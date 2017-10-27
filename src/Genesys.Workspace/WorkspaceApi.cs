using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RestSharp;
using System.Windows;
using Newtonsoft.Json.Linq;
using CometD.Client;
using System.Collections;
using Genesys.Workspace.Api;
using Genesys.Workspace.Client;
using Genesys.Workspace.Model;

namespace Genesys.Workspace
{
    public class WorkspaceApi
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static String SESSION_COOKIE = "WORKSPACE_SESSIONID";

        private ApiClient apiClient;
        private VoiceApi voiceApi;
        private TargetsApi targetsApi;
        private SessionApi sessionApi;
        private Notifications notifications;

        private CookieContainer cookieContainer;
        private string workspaceSessionId;
        private bool workspaceInitialized = false;

        public User User { get; protected set; }
        public KeyValueCollection Settings { get; protected set; }
        public List<ActionCode> ActionCodes { get; protected set; }
        public List<BusinessAttribute> BusinessAttributes { get; protected set; }
        public List<Transaction> Transactions { get; protected set; }
        public List<AgentGroup> AgentGroups { get; protected set; }

        public bool DebugEnabled { get; set; }

        public WorkspaceApi(String apiKey, String baseUrl) : this(apiKey, baseUrl, new VoiceApi(), new TargetsApi(), new SessionApi(), new Notifications())
        {
        }

        private WorkspaceApi(String apiKey, String baseUrl, VoiceApi voiceApi, TargetsApi targetsApi, SessionApi sessionApi, Notifications notifications) {
            cookieContainer = new CookieContainer();

            apiClient = new ApiClient(baseUrl + "/workspace/v3");
            apiClient.Configuration.ApiClient = apiClient;  // circular reference?!?
            apiClient.Configuration.AddApiKey("x-api-key", apiKey);
            apiClient.Configuration.DefaultHeader.Add("x-api-key", apiKey);
            apiClient.RestClient.CookieContainer = cookieContainer;
            apiClient.RestClient.AddDefaultHeader("x-api-key", apiKey);

            //this.apiKey = apiKey;
            //this.baseUrl = baseUrl;
            //this.workspaceUrl = this.baseUrl + "/workspace/v3";
            
            this.voiceApi = voiceApi;
            this.targetsApi = targetsApi;
            this.sessionApi = sessionApi;
            this.notifications = notifications;

            this.sessionApi.Initialize(apiClient);
            this.voiceApi.Initialize(apiClient);
        } 

        private String extractSessionCookie(ApiResponse<ApiSuccessResponse> response)
        {
            log.Debug("Extracting session cookie...");
            string sessionId = null;

            foreach (string key in response.Headers.Keys)
            {
                if (key.Equals("set-cookie"))
                {
                    string cookie = response.Headers[key];
                    sessionId = cookie.Split(';')[0].Split('=')[1];
                    apiClient.Configuration.DefaultHeader.Add("Cookie", String.Format("{0}={1}", SESSION_COOKIE, sessionId));
                    apiClient.RestClient.AddDefaultHeader("Cookie", String.Format("{0}={1}", SESSION_COOKIE, sessionId));
                    break;
                }
            }

            log.Debug("WORKSPACE_SESSIONID is " + sessionId);

            return sessionId;
        }

        void OnInitMessage(CometD.Bayeux.Client.IClientSessionChannel channel, CometD.Bayeux.IMessage message, BayeuxClient client)
        {
            try
            {
                IDictionary<string, object> data = message.DataAsDictionary;

                string messageType = (string)data.GetValue("messageType");

                if ("WorkspaceInitializationComplete".Equals(messageType))
                {
                    string state = (string)data.GetValue("state");
                    if ("Complete".Equals(state))
                    {
                        IDictionary<String, Object> initData = (IDictionary<String, Object>)data.GetValue("data");
                        IDictionary<String, Object> userData = (IDictionary<String, Object>)initData.GetValue("user");

                        ArrayList annexData = (ArrayList)userData.GetValue("userProperties");
                        KeyValueCollection userProperties = new KeyValueCollection();
                        Util.extractKeyValueData(userProperties, annexData);

                        if (User == null)
                        {
                            User = new User()
                            {
                                dbid = (int)userData.GetValue("dbid"),
                                firstName = (string)userData.GetValue("firstName"),
                                lastName = (string)userData.GetValue("lastName"),
                                userName = (string)userData.GetValue("userName"),
                                employeeId = (string)userData.GetValue("employeeId"),
                                agentId = (string)userData.GetValue("agentLogin"),
                                defaultPlace = (string)userData.GetValue("defaultPlace"),
                                tenantDBID = (int)userData.GetValue("tenantDBID"),
                                userProperties = userProperties
                            };
                        }

                        ExtractConfiguration((IDictionary<String, Object>)initData.GetValue("configuration"));

                        this.workspaceInitialized = true;
                        log.Debug("Initialization complete");
                        log.Debug(User.ToString());
                    }
                    else if ("Failed".Equals(state))
                    {
                        log.Debug("Workspace initialization failed!");
                    }
                }
            }
            catch(Exception exc)
            {
                log.Error("Error handling OnInitMessage", exc);
            }
        }

        private void ExtractConfiguration(IDictionary<string, object> configData)
        {
            try
            {
                //log.Debug("Processing Action Codes");

                ArrayList actionCodesData = (ArrayList)configData.GetValue("actionCodes");
                this.ActionCodes = new List<ActionCode>();
                if (actionCodesData != null)
                {
                    //log.Debug(String.Format("Processing {0} Action Codes", actionCodesData.Count));  

                    foreach (Dictionary<string, object> actionCode in actionCodesData)
                    {
                        //log.Debug("Action Code: " + actionCode.ToDebugString());

                        ArrayList userPropertyData = (ArrayList)actionCode.GetValue("userProperties");
                        KeyValueCollection userProperties = new KeyValueCollection();
                        Util.extractKeyValueData(userProperties, userPropertyData);

                        ArrayList subCodesData = (ArrayList)actionCode.GetValue("subCodes");
                        List<SubCode> subCodes = new List<SubCode>();
                        if (subCodesData != null)
                        {
                            foreach (Dictionary<string, object> subCode in subCodesData)
                            {
                                //log.Debug("Action SubCode: " + subCode.ToDebugString());

                                subCodes.Add(new SubCode()
                                {
                                    name = (string)subCode.GetValue("name"),
                                    code = (String)subCode.GetValue("code")
                                });
                            }
                        }

                        this.ActionCodes.Add(new ActionCode()
                        {
                            name = (string)actionCode.GetValue("name"),
                            code = (string)actionCode.GetValue("code"),
                            type = Util.parseActionCodeType((String)actionCode.GetValue("type")),
                            subCodes = subCodes
                        });
                    }
                }
                else
                {
                    //log.Debug("No Action Codes to process");     
                }
            }
            catch(Exception exc) 
            {
                //log.Error("Exception parsing Action Codes", exc);
            }

            try
            {
                //log.Debug("Processing Settings");

                ArrayList settingsData = (ArrayList)configData.GetValue("settings");
                this.Settings = new KeyValueCollection();
                Util.extractKeyValueData(this.Settings, settingsData);
            }
            catch(Exception exc) 
            {
                //log.Error("Exception parsing Settings", exc);    
            }

            try
            {
                //log.Debug("Processing Business Attributes");

                ArrayList businessAttributesData = (ArrayList)configData.GetValue("businessAttributes");
                if (businessAttributesData != null)
                {
                    //log.Debug(String.Format("Processing {0} Business Attributes", businessAttributesData.Count));    

                    this.BusinessAttributes = new List<BusinessAttribute>();

                    foreach (Dictionary<string, object> businessAttribute in businessAttributesData)
                    {
                        //log.Debug("Business Attribute: " + businessAttribute.ToDebugString());

                        ArrayList businessAttributeValues = (ArrayList)businessAttribute.GetValue("values");

                        List<BusinessAttributeValue> values = new List<BusinessAttributeValue>();

                        if (businessAttributeValues != null)
                        {
                            foreach (Dictionary<string, object> businessAttributeValue in businessAttributeValues)
                            {
                                //log.Debug("Business Attribute Value: " + businessAttributeValue.ToDebugString());

                                values.Add(new BusinessAttributeValue()
                                {
                                    dbid = (int)businessAttributeValue.GetValue("id"),
                                    name = (string)businessAttributeValue.GetValue("name"),
                                    displayName = (string)businessAttributeValue.GetValue("displayName"),
                                    description = (string)businessAttributeValue.GetValue("description"),
                                    defaultValue = businessAttributeValue.GetValue("default")
                                });
                            }
                        }

                        this.BusinessAttributes.Add(new BusinessAttribute()
                        {
                            dbid = (int)businessAttribute.GetValue("id"),
                            name = (string)businessAttribute.GetValue("name"),
                            displayName = (string)businessAttribute.GetValue("displayName"),
                            description = (string)businessAttribute.GetValue("description"),
                            values = values
                        });
                    }
                }
                else
                {
                    //log.Debug("No Business Attributes to process");    
                }
            }
            catch(Exception exc)
            {
                //log.Error("Exception parsing Business Attributes", exc);
            }

            try
            {
                //log.Debug("Processing Transactions");

                ArrayList transactionsData = (ArrayList)configData.GetValue("transactions");
                if (transactionsData != null)
                {
                    //log.Debug(String.Format("Processing {0} Transactions", transactionsData.Count)); 

                    this.Transactions = new List<Transaction>();
                    foreach (Dictionary<string, object> transaction in transactionsData)
                    {
                        //log.Debug("Transaction: " + transaction.ToDebugString());

                        ArrayList userPropertyData = (ArrayList)transaction.GetValue("userProperties");
                        KeyValueCollection userProperties = new KeyValueCollection();
                        Util.extractKeyValueData(userProperties, userPropertyData);

                        this.Transactions.Add(new Transaction()
                        {
                            name = (string)transaction.GetValue("name"),
                            alias = (string)transaction.GetValue("alias"),
                            userProperties = userProperties
                        });
                    }
                }
                else
                {
                    //log.Debug("No Transactions to process");    
                }
            }
            catch(Exception exc)
            {
                //log.Error("Exception parsing Transactions", exc);
            }

            try
            {
                //log.Debug("Processing Agent Groups");

                ArrayList agentGroupsData = (ArrayList)configData.GetValue("agentGroups");
                if (agentGroupsData != null)
                {
                    //log.Debug(String.Format("Processing {0} Agent Groups", agentGroupsData.Count));

                    this.AgentGroups = new List<AgentGroup>();
                    foreach (Dictionary<string, object> agentGroup in agentGroupsData)
                    {
                        //log.Debug("Agent Group: " + agentGroup.ToDebugString());

                        KeyValueCollection userProperties = new KeyValueCollection();
                        Dictionary<String, Object> agentGroupSettingsData = (Dictionary<String, Object>)agentGroup.GetValue("settings");
                        if (agentGroupSettingsData != null && !(agentGroupSettingsData.Count == 0))
                        {
                            // Top level will be sections
                            foreach (string sectionName in agentGroupSettingsData.Keys)
                            {
                                Dictionary<String, Object> sectionData = (Dictionary<String, Object>)agentGroupSettingsData[sectionName];
                                KeyValueCollection section = new KeyValueCollection();
                                if (sectionData != null && !(sectionData.Count == 0))
                                {
                                    foreach (string optionName in sectionData.Keys)
                                    {
                                        section.addString(optionName, (String)sectionData[optionName]);
                                    }
                                }

                                userProperties.addList(sectionName, section);
                            }
                        }

                        this.AgentGroups.Add(new AgentGroup()
                        {
                            dbid = (int)agentGroup.GetValue("DBID"),
                            name = (string)agentGroup.GetValue("name"),
                            userProperties = userProperties
                        });
                    }
                }
                else
                {
                    //log.Debug("No Agent Groups to process");
                }
            }
            catch(Exception exc)
            {
                //log.Error("Exception parsing Agent Groups", exc);
            }
        }

        public VoiceApi Voice()
        {
            return this.voiceApi;
        }

        public TargetsApi Targets()
        {
            return this.targetsApi;
        }

        public void Initialize(string token)
        {
            this.Initialize(null, null, token);
        }

        public void Initialize(string authCode, string redirectUri, string token)
        {
            try
            {
                //apiClient.Configuration.DefaultHeader.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + token);
                //apiClient.RestClient.AddDefaultHeader(HttpRequestHeader.Authorization.ToString(), "bearer " + token);

                ApiResponse<ApiSuccessResponse> response = sessionApi.InitializeWorkspaceWithHttpInfo(authCode, redirectUri, "bearer " + token);

                log.Debug("SessionApi.InitializeWorkspace Response: " + response.Data.ToJson());
                workspaceSessionId = extractSessionCookie(response);

                notifications.subscribe("/workspace/v3/initialization", OnInitMessage);
                notifications.subscribe("/workspace/v3/voice", this.voiceApi.OnVoiceMessage);
                notifications.Initialize(apiClient);
            }
            catch(Exception exc)
            {
                log.Error("Failed to initialize workspace", exc);
            }
        }

        public void Destroy()
        {
            try {
                if (this.workspaceInitialized)
                {
                    notifications.Disconnect();
                    sessionApi.Logout();
                }
            } catch (Exception e) {
                throw new WorkspaceApiException("destroy failed.", e);
            } finally {
                this.workspaceInitialized = false;
            }
        }

        /**
         * Initializes the voice channel using the specified resources.
         * @param agentId agentId to be used for login
         * @param placeName name of the place to use for login
         */
        public void ActivateChannels(
                string agentId, 
                string placeName)
        {
            this.ActivateChannels(agentId, null, placeName, null);
        }

        /**
         * Initializes the voice channel using the specified resources.
         * @param agentId agentId to be used for login
         * @param dn DN to be used for login. Provide only one of dn or placeName
         * @param placeName name of the place to use for login. Provide only one of placeName or dn
         * @param queueName name of the queue to be used for login. (optional)
         */
        public void ActivateChannels(
                string agentId,
                string dn,
                string placeName,
                string queueName
        )
        {
            try {
                string msg = "Activating channels with agentId [" + agentId + "] ";
                ActivatechannelsData data = new ActivatechannelsData();
                data.AgentId = agentId;

                if (placeName != null)
                {
                    data.PlaceName = placeName;
                    msg += "place [" + placeName + "]";
                }
                else
                {
                    data.Dn = dn;
                    msg += "dn [" + dn + "]";
                }

                if (queueName != null)
                {
                    data.QueueName = queueName;
                    msg += " queueName [" + queueName + "]";
                }

                ChannelsData channelsData = new ChannelsData();
                channelsData.Data = data;

                log.Debug(msg + "...");
                ApiSuccessResponse response = this.sessionApi.ActivateChannels(channelsData);
                if (response.Status.Code != 0)
                {
                    throw new WorkspaceApiException( "activateChannels failed with code: " + response.Status.Code );
                }
            } catch (ApiException e) {
                throw new WorkspaceApiException("activateChannels failed.", e);
            }
        }
    }
}
