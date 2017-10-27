using System;
using System.Collections;
using System.Collections.Generic;
using CometD.Bayeux;
using CometD.Bayeux.Client;
using CometD.Client;
using Genesys.Workspace.Client;
using Genesys.Workspace.Model;
using RestSharp;

namespace Genesys.Workspace
{
    public class VoiceApi
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void DnStateChangedEventHandler(Dn dn, IMessage message);
        public event DnStateChangedEventHandler DnStateChanged;

        public delegate void CallStateChangedEventHandler(Call call, IMessage message);
        public event CallStateChangedEventHandler CallStateChanged;

        public delegate void VoiceErrorEventHandler(string msg, string code, IMessage message);
        public event VoiceErrorEventHandler VoiceErrorReceived;

        private Genesys.Workspace.Api.VoiceApi voiceApi;
        public Dn Dn { get; protected set; }
        public Dictionary<string, Call> Calls { get; protected set; }

        public VoiceApi()
        {
            Calls = new Dictionary<string, Call>();
        }

        public void Initialize(ApiClient apiClient)
        {
            this.voiceApi = new Api.VoiceApi(apiClient.RestClient.BaseUrl.ToString());
            voiceApi.Configuration.ApiClient = apiClient;
        }

        public void OnVoiceMessage(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            try
            {
                IDictionary<string, object> data = message.DataAsDictionary;

                string messageType = (string)data.GetValue("messageType");

                switch (messageType)
                {
                    case "DnStateChanged":
                        this.onDnStateChanged(channel, message, client);
                        break;

                    case "CallStateChanged":
                        this.onCallStateChanged(channel, message, client);
                        break;

                    case "EventError":
                        this.onEventError(channel, message, client);
                        break;

                    default:
                        log.Debug("Unexpected messageType: " + messageType);
                        break;
                }
            }
            catch(Exception exc)
            {
                log.Error("Error handling OnVoiceMessage", exc);
            }
        }

        public void onDnStateChanged(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            if (this.Dn == null)
            {
                this.Dn = new Dn();
            }

            IDictionary<string, object> data = message.DataAsDictionary;

            Dictionary<string, object> dnData = (Dictionary<string, object>)data.GetValue("dn");

            this.Dn.number = (string)dnData.GetValue("number");
            this.Dn.switchName = (string)dnData.GetValue("switchName");
            this.Dn.agentId = (string)dnData.GetValue("agentId");

            AgentState agentState;
            agentState = (Enum.TryParse<AgentState>((string)dnData.GetValue("agentState"), out agentState) == true) ? agentState : AgentState.Unknown;
            this.Dn.agentState = agentState;

            AgentWorkMode workMode;
            workMode = (Enum.TryParse<AgentWorkMode>((string)dnData.GetValue("agentWorkMode"), out workMode) == true) ? workMode: AgentWorkMode.Unknown;
            this.Dn.workMode = workMode;

            this.Dn.telephonyNetwork = (string)dnData.GetValue("telephonyNetwork");

            this.Dn.forwardTo = (string)dnData.GetValue("forwardTo");
            this.Dn.dnd = "on".Equals((string)dnData.GetValue("dnd"));


            var capTemp = dnData.GetValue("capabilities");

            // .NET Framework returns an object[]
            // Mono on Mac returns an ArrayList
            ArrayList capabilities = null;
            if (capTemp is ArrayList)
                capabilities = (ArrayList)capTemp;
            else if (capTemp is object[])
                capabilities = new ArrayList((object[])capTemp);
            else
                capabilities = new ArrayList();
            
            List<DnCapability> caps = new List<DnCapability>();

            foreach (string c in capabilities)
            {
                DnCapability cap;

                if ( Enum.TryParse<DnCapability>(c.Replace('-', '_'), out cap) )
                {
                    caps.Add(cap);
                }
            }
            this.Dn.capabilities = caps.ToArray();

            log.Debug("Dn updated: " + Dn.ToString());

            DnStateChanged?.Invoke(this.Dn, message);
        }

        private void onCallStateChanged(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            IDictionary<string, object> data = message.DataAsDictionary;

            Dictionary<string, object> callData = (Dictionary<string, object>)data.GetValue("call");
            NotificationType notificationType = Util.parseNotificationType((string)data.GetValue("notificationType"));

            string id = (string)callData.GetValue("id");
            string callUuid = (string)callData.GetValue("callUuid");
            CallState state = Util.parseCallState((string)callData.GetValue("state"));
            string parentConnId = (string)callData.GetValue("parentConnId");
            string previousConnId = (string)callData.GetValue("previousConnId");
            string ani = (string)callData.GetValue("ani");
            string dnis = (string)callData.GetValue("dnis");


            var userdataTemp = callData.GetValue("userdata");

            // .NET Framework returns an object[]
            // Mono on Mac returns an ArrayList
            ArrayList userdataData = null;
            if (userdataTemp is ArrayList)
                userdataData = (ArrayList)userdataTemp;
            else if (userdataTemp is object[])
                userdataData = new ArrayList((object[])userdataTemp);
            else
                userdataData = new ArrayList();
            
            KeyValueCollection userData = new KeyValueCollection();
            Util.extractKeyValueData(userData, userdataData);


            var participantsTemp = callData.GetValue("participants");

            // .NET Framework returns an object[]
            // Mono on Mac returns an ArrayList
            ArrayList participantData = null;
            if (participantsTemp is ArrayList)
                participantData = (ArrayList)participantsTemp;
            else if (participantsTemp is object[])
                participantData = new ArrayList((object[])participantsTemp);
            else
                participantData = new ArrayList();
            
            String[] participants = Util.extractParticipants(participantData);

            bool connIdChanged = false;
            String callType = (string)callData.GetValue("callType");

            Call call;
            if (this.Calls.ContainsKey(id))
            {
                call = this.Calls[id];
            }
            else
            {
                call = new Call()
                {
                    id = id,
                    callType = callType,
                    parentConnId = parentConnId
                };

                this.Calls.Add(id, call);
                log.Debug("Added call:  " + call.ToString());
            }

            if (previousConnId != null && this.Calls.ContainsKey(previousConnId))
            {
                call = this.Calls[previousConnId];
                this.Calls.Remove(previousConnId);
                call.id = id;
                call.previousConnId = previousConnId;
                this.Calls.Add(id, call);
                connIdChanged = true;
            }
            else if (state == CallState.RELEASED)
            {
                this.Calls.Remove(id);
                log.Debug("Removed call " + id + "(" + state + ")");
            }

            call.state = state;
            call.ani = ani;
            call.dnis = dnis;
            call.callUuid = callUuid;
            call.participants = participants;
            call.userData = userData;

            var capTemp = callData.GetValue("capabilities");

            // .NET Framework returns an object[]
            // Mono on Mac returns an ArrayList
            ArrayList capabilities = null;
            if (capTemp is ArrayList)
                capabilities = (ArrayList)capTemp;
            else if (capTemp is object[])
                capabilities = new ArrayList((object[])capTemp);
            else
                capabilities = new ArrayList();

            List<CallCapability> caps = new List<CallCapability>();

            foreach (string c in capabilities)
            {
                CallCapability cap;

                if (Enum.TryParse<CallCapability>(c.Replace('-', '_'), out cap))
                {
                    caps.Add(cap);
                }
            }
            call.capabilities = caps.ToArray();

            CallStateChanged?.Invoke(call, message);
        }

        private void onEventError(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            IDictionary<string, object> data = message.DataAsDictionary;

            Dictionary<string, object> errorDetails = (Dictionary<string, object>)data.GetValue("error");
            string msg = (string)errorDetails.GetValue("errorMessage");
            string code = errorDetails.GetValue("errorCode") == null ? "" : errorDetails.GetValue("errorCode").ToString();

            VoiceErrorReceived?.Invoke(msg, code, message);
        }

        /**
         * Set the agent state to ready.
         */
        public void SetAgentReady()
        {
            this.SetAgentReady(null, null);
        }

        /**
         * Set the agent state to ready.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void SetAgentReady(
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )
        {
            try 
            {
                VoicereadyData readyData = new VoicereadyData(
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                ReadyData data = new ReadyData
                {
                    Data = readyData
                };

                ApiSuccessResponse response = this.voiceApi.SetAgentStateReady(data);
                Util.ThrowIfNotOk("SetAgentReady", response);
            } catch (ApiException e) {
                throw new WorkspaceApiException("SetAgentReady failed.", e);
            }
        }

        /**
         * Set the agent state to not ready.
         */
        public void SetAgentNotReady()
        {
            this.SetAgentNotReady(null, null, null, null);
        }

        /**
         * Set the agent state to not ready.
         * @param workMode optional workMode to use in the request.
         * @param reasonCode optional reasonCode to use in the request.
         */
        public void SetAgentNotReady(String workMode, String reasonCode)
        {
            this.SetAgentNotReady(workMode, reasonCode, null, null);
        }

        /**
         * Set the agent state to not ready.
         * @param workMode optional workMode to use in the request.
         * @param reasonCode optional reasonCode to use in the request.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void SetAgentNotReady(
                String workMode,
                String reasonCode,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )
        {
            try 
            {
                VoicenotreadyData notReadyData = new VoicenotreadyData(
                    reasonCode,
                    ((workMode != null) ? (VoicenotreadyData.AgentWorkModeEnum)Enum.Parse(typeof(VoicenotreadyData.AgentWorkModeEnum), workMode) : (VoicenotreadyData.AgentWorkModeEnum?)null),
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                NotReadyData data = new NotReadyData
                {
                    Data = notReadyData
                };

                ApiSuccessResponse response = this.voiceApi.SetAgentStateNotReady(data);
                Util.ThrowIfNotOk("SetAgentNotReady", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("SetAgentReady failed.", e);
            }
        }

        /**
         * Set do-not-disturb on for voice.
         */
        public void DndOn()
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.SetDNDOn();
                Util.ThrowIfNotOk("DndOn", response);
            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("DndOn failed.", e);
            }
        }

        /**
         * Set do-not-disturb off for voice.
         */
        public void DndOff()
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.SetDNDOff();
                Util.ThrowIfNotOk("DndOff", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("DndOff failed.", e);
            }
        }

        /**
         * Login the voice channel.
         */
        public void Login()
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.LoginVoice();
                Util.ThrowIfNotOk("VoiceLogin", response);
            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("VoiceLogin failed", e);
            }
        }

        /**
         * Logout the voice channel.
         */
        public void Logout()
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.LogoutVoice();
                Util.ThrowIfNotOk("VoiceLogout", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("VoiceLogout failed", e);
            }
        }

        /**
         * Set call forwarding to the specififed destination.
         * @param destination - destination to forward calls to.
         */
        public void SetForward(String destination)
        {
            try 
            {
                VoicesetforwardData forwardData = new VoicesetforwardData(destination);
                ForwardData data = new ForwardData
                {
                    Data = forwardData
                };

                ApiSuccessResponse response = this.voiceApi.Forward(data);
                Util.ThrowIfNotOk("SetForward", response);
            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("SetForward failed.", e);
            }
        }

        /**
         * Cancel call forwarding.
         */
        public void CancelForward()
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.CancelForward();
                Util.ThrowIfNotOk("CancelForward", response);
            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("CancelForward failed.", e);
            }
        }

        /**
         * Make a new call to the specified destination.
         * @param destination The destination to call
         */
        public void MakeCall(String destination)   
        {
            this.MakeCall(destination, null, null, null, null, null);
        }

        /**
         * Make a new call to the specified destination.
         * @param destination The destination to call
         * @param userData userData to be included with the new call
         */
        public void MakeCall(
                String destination,
                KeyValueCollection userData
        )   
        {
            this.MakeCall(destination, null, userData, null, null, null);
        }

        /**
         * Make a new call to the specified destination.
         * @param destination The destination to call
         * @param userData userData to be included with the new call
         * @param reasons reasons
         * @param extensions extensions
         */
        public void MakeCall(
                string destination,
                string location,
                KeyValueCollection userData,
                KeyValueCollection reasons,
                KeyValueCollection extensions,
                string outboundCallerId
        )   
        {
            try 
            {
                VoicemakecallData data = new VoicemakecallData(
                    destination,
                    location,
                    (userData != null) ? userData.ToListKvpair() : null,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null,
                    outboundCallerId
                );
                MakeCallData makeCallData = new MakeCallData(data);

                ApiSuccessResponse response = this.voiceApi.MakeCall(makeCallData);
                Util.ThrowIfNotOk("MakeCall", response);

            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("MakeCall failed.", e);
            }
        }

        /**
         * Answer call.
         * @param connId The connId of the call to answer.
         */
        public void AnswerCall(String connId)   
        {
            this.AnswerCall(connId, null, null);
        }

        /**
         * Answer call.
         * @param connId The connId of the call to answer.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void AnswerCall(
                String connId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try
            {
                VoicereadyData answerData = new VoicereadyData(
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                AnswerData data = new AnswerData
                {
                    Data = answerData
                };

                ApiSuccessResponse response = this.voiceApi.Answer(connId, data);
                Util.ThrowIfNotOk("AnswerCall", response);

            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("AnswerCall failed.", e);
            }
        }

        /**
         * Place call on hold.
         * @param connId The connId of the call to place on hold.
         */
        public void HoldCall(String connId)   
        {
            this.HoldCall(connId, null, null);
        }

        /**
         * Place call on hold.
         * @param connId The connId of the call to place on hold.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void HoldCall(
                String connId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try {
                VoicereadyData holdData = new VoicereadyData(
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                HoldData data = new HoldData
                {
                    Data = holdData
                };

                ApiSuccessResponse response = this.voiceApi.Hold(connId, data);
                Util.ThrowIfNotOk("HoldCall", response);

            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("HoldCall failed.", e);
            }
        }

        /**
         * Retrieve call from hold.
         * @param connId The connId of the call to retrieve.
         */
        public void RetrieveCall(String connId)   
        {
            this.RetrieveCall(connId, null, null);
        }

        /**
         * Retrieve call from hold.
         * @param connId The connId of the call to retrieve.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void RetrieveCall(
                String connId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicereadyData retrieveData = new VoicereadyData(
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                RetrieveData data = new RetrieveData
                {
                    Data = retrieveData
                };
                ApiSuccessResponse response = this.voiceApi.Retrieve(connId, data);
                Util.ThrowIfNotOk("RetrieveCall", response);

            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("RetrieveCall failed.", e);
            }
        }

        /**
         * Release call.
         * @param connId The connId of the call to release
         */
        public void ReleaseCall(String connId)   
        {
            this.ReleaseCall(connId, null, null);
        }

        /**
         * Release call.
         * @param connId The connId of the call to release
         * @param reasons reasons
         * @param extensions extensions
         */
        public void ReleaseCall(
                String connId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {

            try 
            {
                VoicereadyData releaseData = new VoicereadyData(
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                ReleaseData data = new ReleaseData
                {
                    Data = releaseData
                };

                ApiSuccessResponse response = this.voiceApi.Release(connId, data);
                Util.ThrowIfNotOk("ReleaseCall", response);

            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("ReleaseCall failed.", e);
            }
        }

        /**
         * Initiate a conference to the specified destination.
         * @param connId The connId of the call to start the conference from.
         * @param destination The destination
         */
        public void InitiateConference(String connId, String destination)   
        {
            this.InitiateConference(connId, destination, null, null, null, null, null);
        }

        /**
         * Initiate a conference to the specified destination.
         * @param connId The connId of the call to start the conference from.
         * @param destination The destination
         * @param userData userdata to be used for the new consult call.
         */
        public void InitiateConference(
                String connId,
                String destination,
                KeyValueCollection userData
        )   
        {
            this.InitiateConference(connId, destination, null, null, userData, null, null);
        }

        /**
         * Initiate a conference to the specified destination.
         * @param connId The connId of the call to start the conference from.
         * @param destination The destination
         * @param location
         * @param outboundCallerId
         * @param userData userdata to be used for the new consult call.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void InitiateConference(
                String connId,
                String destination,
                String location,
                String outboundCallerId,
                KeyValueCollection userData,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidinitiateconferenceData initData = new VoicecallsidinitiateconferenceData(
                    destination,
                    location,
                    (userData != null) ? userData.ToListKvpair() : null,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null,
                    outboundCallerId
                );
                InitiateConferenceData data = new InitiateConferenceData
                {
                    Data = initData
                };

                ApiSuccessResponse response = this.voiceApi.InitiateConference(connId, data);
                Util.ThrowIfNotOk("InitiateConference", response);

            }
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("InitiateConference failed.", e);
            }
        }

        /**
         * Complete a previously initiated conference identified by the provided ids.
         * @param connId The id of the consule call (established)
         * @param parentConnId The id of the parent call (held).
         */
        public void CompleteConference(String connId, String parentConnId)   
        {
            this.CompleteConference(connId, parentConnId, null, null);
        }

        /**
         * Complete a previously initiated conference identified by the provided ids.
         * @param connId The id of the consule call (established)
         * @param parentConnId The id of the parent call (held).
         * @param reasons reasons
         * @param extensions extensions
         */
        public void CompleteConference(
                String connId,
                String parentConnId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidcompletetransferData completeData = new VoicecallsidcompletetransferData(
                    parentConnId,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                CompleteConferenceData data = new CompleteConferenceData
                {
                    Data = completeData
                };

                ApiSuccessResponse response = this.voiceApi.CompleteConference(connId, data);
                Util.ThrowIfNotOk("CompleteConference", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("CompleteConference failed.", e);
            }
        }

        /**
         * Initiate a transfer to the specified destination.
         * @param connId The connId of the call to be transferred.
         * @param destination The destination of the transfer.
         */
        public void InitiateTransfer(String connId, String destination)   
        {
            this.InitiateTransfer(connId, destination, null, null, null, null, null);
        }

        /**
         * Initiate a transfer to the specified destination.
         * @param connId The connId of the call to be transferred.
         * @param destination The destination of the transfer.
         * @param userData userdata to be included with the new consult call
         */
        public void InitiateTransfer(
                String connId,
                String destination,
                KeyValueCollection userData
        )   
        {
            this.InitiateTransfer(connId, destination, null, null, userData, null, null);
        }

        /**
         * Initiate a transfer to the specified destination.
         * @param connId  The connId of the call to be transferred.
         * @param destination The destination
         * @param location
         * @param outboundCallerId
         * @param userData userdata to be used for the new consult call.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void InitiateTransfer(
                String connId,
                String destination,
                String location,
                String outboundCallerId,
                KeyValueCollection userData,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidinitiatetransferData data = new VoicecallsidinitiatetransferData(
                    destination,
                    location,
                    (userData != null) ? userData.ToListKvpair() : null,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null,
                    outboundCallerId
                );
                InitiateTransferData initData = new InitiateTransferData
                {
                    Data = data
                };

                ApiSuccessResponse response = this.voiceApi.InitiateTransfer(connId, initData);
                Util.ThrowIfNotOk("InitiateTransfer", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("InitiateTransfer failed.", e);
            }
        }

        /**
         * Complete a previously initiated transfer using the provided ids.
         * @param connId The id of the consult call (established)
         * @param parentConnId The id of the parent call (held)
         */
        public void CompleteTransfer(String connId, String parentConnId)   
        {
            this.CompleteTransfer(connId, parentConnId, null, null);
        }

        /**
         * Complete a previously initiated transfer using the provided ids.
         * @param connId The id of the consult call (established)
         * @param parentConnId The id of the parent call (held)
         * @param reasons reasons
         * @param extensions extensions
         */
        public void CompleteTransfer(
                String connId,
                String parentConnId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidcompletetransferData completeData = new VoicecallsidcompletetransferData(
                    parentConnId,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                CompleteTransferData data = new CompleteTransferData
                {
                    Data = completeData
                };

                ApiSuccessResponse response = this.voiceApi.CompleteTransfer(connId, data);
                Util.ThrowIfNotOk("CompleteTransfer", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("CompleteTransfer failed.", e);
            }
        }

        /**
         * Alternate two calls retrieving the held call and placing the established call on hold. This is a
         * shortcut for doing hold and retrieve separately.
         * @param connId The id of the established call.
         * @param heldConnId The id of the held call.
         */
        public void AlternateCalls(String connId, String heldConnId)   
        {
            this.AlternateCalls(connId, heldConnId, null, null);
        }

        /**
         * Alternate two calls retrieving the held call and placing the established call on hold. This is a
         * shortcut for doing hold and retrieve separately.
         * @param connId The id of the established call.
         * @param heldConnId The id of the held call.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void AlternateCalls(
                String connId,
                String heldConnId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidalternateData alternateData = new VoicecallsidalternateData(
                    heldConnId,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                AlternateData data = new AlternateData
                {
                    Data = alternateData
                };

                ApiSuccessResponse response = this.voiceApi.Alternate(connId, data);
                Util.ThrowIfNotOk("AlternateCalls", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("AlternateCalls failed.", e);
            }
        }

        /**
         * Delete a dn from a conference call
         * @param connId The connId of the conference
         * @param dnToDrop The dn number to drop from the conference.
         */
        public void DeleteFromConference(String connId, String dnToDrop)   
        {
            this.DeleteFromConference(connId, dnToDrop, null, null);
        }

        /**
         * Delete a dn from a conference call
         * @param connId The connId of the conference
         * @param dnToDrop The dn number to drop from the conference.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void DeleteFromConference(
                String connId,
                String dnToDrop,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsiddeletefromconferenceData deleteData = new VoicecallsiddeletefromconferenceData(
                    dnToDrop,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                DeleteFromConferenceData data = new DeleteFromConferenceData
                {
                    Data = deleteData
                };

                ApiSuccessResponse response = this.voiceApi.DeleteFromConference(connId, data);
                Util.ThrowIfNotOk("DeleteFromConference", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("DeleteFromConference failed", e);
            }
        }

        /**
         * Perform a single-step transfer to the specified destination.
         * @param connId The id of the call to transfer.
         * @param destination The destination to transfer the call to.
         */
        public void SingleStepTransfer(String connId, String destination)   
        {
            this.SingleStepTransfer(connId, destination, null, null, null, null);
        }

        /**
         * Perform a single-step transfer to the specified destination.
         * @param connId The id of the call to transfer.
         * @param destination The destination to transfer the call to.
         * @param userData userdata to be included on the transfer
         */
        public void SingleStepTransfer(
                String connId,
                String destination,
                KeyValueCollection userData
        )   
        {
            this.SingleStepTransfer(connId, destination, null, userData, null, null);
        }

        /**
         * Perform a single-step transfer to the specified destination.
         * @param connId The id of the call to transfer.
         * @param destination The destination to transfer the call to.
         * @param location
         * @param userData userdata to be used for the new consult call.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void SingleStepTransfer(
                String connId,
                String destination,
                String location,
                KeyValueCollection userData,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try
            {
                VoicecallsidsinglesteptransferData transferData = new VoicecallsidsinglesteptransferData(
                    destination,
                    location,
                    (userData != null) ? userData.ToListKvpair() : null,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                SingleStepTransferData data = new SingleStepTransferData
                {
                    Data = transferData
                };

                ApiSuccessResponse response = this.voiceApi.SingleStepTransfer(connId, data);
                Util.ThrowIfNotOk("SingleStepTransfer", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("SingleStepTransfer failed", e);
            }
        }

        /**
         * Perform a single-step conference to the specififed destination. This will effectively add the
         * destination to the existing call, creating a conference if necessary.
         * @param connId The id of the call to conference.
         * @param destination The destination to be added to the call.
         */
        public void SingleStepConference(String connId, String destination)   
        {
            this.SingleStepConference(connId, destination, null, null, null, null);

        }

        /**
         * Perform a single-step conference to the specififed destination. This will effectively add the
         * destination to the existing call, creating a conference if necessary.
         * @param connId The id of the call to conference.
         * @param destination The destination to be added to the call.
         * @param userData userdata to be included with the request
         *
         */
        public void SingleStepConference(
                String connId,
                String destination,
                KeyValueCollection userData
        )   
        {
            this.SingleStepConference(connId, destination, null, userData, null, null);
        }

        /**
         * Perform a single-step conference to the specififed destination. This will effectively add the
         * destination to the existing call, creating a conference if necessary.
         * @param connId The id of the call to conference.
         * @param destination The destination to be added to the call.
         * @param location
         * @param userData userdata to be included with the request
         * @param reasons reasons
         * @param extensions extensions
         */
        public void SingleStepConference(
                String connId,
                String destination,
                String location,
                KeyValueCollection userData,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidsinglestepconferenceData confData = new VoicecallsidsinglestepconferenceData(
                    destination,
                    location,
                    (userData != null) ? userData.ToListKvpair() : null,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                SingleStepConferenceData data = new SingleStepConferenceData
                {
                    Data = confData
                };

                ApiSuccessResponse response = this.voiceApi.SingleStepConference(connId, data);
                Util.ThrowIfNotOk("SingleStepConference", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("SingleStepConference failed", e);
            }
        }

        /**
         * Attach the provided data to the call. This adds the data to the call even if data already exists
         * with the provided keys.
         * @param connId The id of the call to attach data to.
         * @param userData The data to attach to the call. This is an array of objects with the properties key, type, and value.
         */
        public void AttachUserData(String connId, KeyValueCollection userData)   
        {
            try 
            {
                VoicecallsidcompleteData completeData = new VoicecallsidcompleteData
                {
                    UserData = userData.ToListKvpair()
                };
                UserData data = new UserData
                {
                    Data = completeData
                };

                ApiSuccessResponse response = this.voiceApi.AttachUserData(connId, data);
                Util.ThrowIfNotOk("AttachUserData", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("AttachUserData failed.", e);
            }
        }

        /**
         * Update call data with the provided key/value pairs. This will replace any existing kvpairs with the same keys.
         * @param connId The id of the call to update data for.
         * @param userData The data to update. This is an array of objecvts with the properties key, type, and value.
         */
        public void UpdateUserData(String connId, KeyValueCollection userData)   
        {
            try 
            {
                VoicecallsidcompleteData completeData = new VoicecallsidcompleteData
                {
                    UserData = userData.ToListKvpair()
                };
                UserData data = new UserData
                {
                    Data = completeData
                };

                ApiSuccessResponse response = this.voiceApi.UpdateUserData(connId, data);
                Util.ThrowIfNotOk("UpdateUserData", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("UpdateUserData failed.", e);
            }
        }

        /**
         * Delete data with the specified key from the call.
         * @param connId The call to remove data from.
         * @param key The key to remove.
         */
        public void DeleteUserDataPair(String connId, String key)   
        {
            try 
            {
                VoicecallsiddeleteuserdatapairData deletePairData = new VoicecallsiddeleteuserdatapairData
                {
                    Key = key
                };
                KeyData data = new KeyData
                {
                    Data = deletePairData
                };

                ApiSuccessResponse response = this.voiceApi.DeleteUserDataPair(connId, data);
                Util.ThrowIfNotOk("DeleteUserDataPair", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("DeleteUserDataPair failed.", e);
            }
        }

        /**
         * Send DTMF digits to the specififed call.
         * @param connId The call to send DTMF digits to.
         * @param digits The DTMF digits to send.
         */
        public void SendDTMF(String connId, String digits)   
        {
            this.SendDTMF(connId, digits, null, null);
        }

        /**
         * Send DTMF digits to the specififed call.
         * @param connId The call to send DTMF digits to.
         * @param digits The DTMF digits to send.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void SendDTMF(
                String connId,
                String digits,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidsenddtmfData dtmfData = new VoicecallsidsenddtmfData(
                    digits,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                SendDTMFData data = new SendDTMFData
                {
                    Data = dtmfData
                };

                ApiSuccessResponse response = this.voiceApi.SendDTMF(connId, data);
                Util.ThrowIfNotOk("SendDTMF", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("SendDTMF failed", e);
            }
        }

        /**
         * Send EventUserEvent with the provided data.
         * @param userData The data to be sent. This is an array of objects with the properties key, type, and value.
         */
        public void SendUserEvent(KeyValueCollection userData)   
        {
            this.SendUserEvent(userData, null);
        }

        /**
         * Send EventUserEvent with the provided data.
         * @param userData The data to be sent. This is an array of objects with the properties key, type, and value.
         * @param callUuid The callUuid that the event will be associated with.
         */
        public void SendUserEvent(KeyValueCollection userData, String callUuid)   
        {
            try 
            {
                SendUserEventDataData sendUserEventData = new SendUserEventDataData(
                    (userData != null) ? userData.ToListKvpair() : null,
                    callUuid
                );
                SendUserEventData data = new SendUserEventData
                {
                    Data = sendUserEventData
                };

                ApiSuccessResponse response = this.voiceApi.SendUserEvent(data);
                Util.ThrowIfNotOk("SendUserEvent", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("SendUserEvent failed.", e);
            }
        }

        /**
         * Redirect call to the specified destination
         * @param connId The connId of the call to redirect.
         * @param destination The destination to redirect the call to.
         */
        public void RedirectCall(String connId, String destination)   
        {
            this.RedirectCall(connId, destination, null, null);
        }

        /**
         * Redirect call to the specified destination
         * @param connId The connId of the call to redirect.
         * @param destination The destination to redirect the call to.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void RedirectCall(
                String connId,
                String destination,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidredirectData redirectData = new VoicecallsidredirectData(
                    destination,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                RedirectData data = new RedirectData
                {
                    Data = redirectData
                };

                ApiSuccessResponse response = this.voiceApi.Redirect(connId, data);
                Util.ThrowIfNotOk("RedirectCall", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("RedirectCall failed.", e);
            }
        }

        /**
         * Merge the two specified calls.
         * @param connId The id of the first call to be merged.
         * @param otherConnId The id of the second call to be merged.
         */
        public void MergeCalls(String connId, String otherConnId)   
        {
            this.MergeCalls(connId, otherConnId, null, null);
        }

        /**
         * Merge the two specified calls.
         * @param connId The id of the first call to be merged.
         * @param otherConnId The id of the second call to be merged.
         * @param reasons reasons
         * @param extensions extensions
         */
        public void MergeCalls(
                String connId,
                String otherConnId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidmergeData mergeData = new VoicecallsidmergeData(
                    otherConnId,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                MergeData data = new MergeData
                {
                    Data = mergeData
                };

                ApiSuccessResponse response = this.voiceApi.Merge(connId, data);
                Util.ThrowIfNotOk("MergeCalls", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("MergeCalls failed.", e);
            }
        }

        /**
         * Reconnect the specified call. Reconnect releases the established call and retrieves the held call
         * in one step.
         * @param connId The id of the established call (will be released)
         * @param heldConnId The id of the held call (will be retrieved)
         */
        public void ReconnectCall(String connId, String heldConnId)   
        {
            this.ReconnectCall(connId, heldConnId, null, null);
        }

        /**
         * Reconnect the specified call. Reconnect releases the established call and retrieves the held call
         * in one step.
         * @param connId The id of the established call (will be released)
         * @param heldConnId The id of the held call (will be retrieved)
         * @param reasons reasons
         * @param extensions extensions
         */
        public void ReconnectCall(
                String connId,
                String heldConnId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicecallsidreconnectData reconnectData = new VoicecallsidreconnectData(
                    heldConnId,
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                ReconnectData data = new ReconnectData
                {
                    Data = reconnectData
                };

                ApiSuccessResponse response = this.voiceApi.Reconnect(connId, data);
                Util.ThrowIfNotOk("ReconnectCall", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("ReconnectCall failed.", e);
            }
        }

        /**
         * Clear call.
         * @param connId The connId of the call to clear
         */
        public void ClearCall(String connId)   
        {
            this.ClearCall(connId, null, null);
        }

        /**
         * Clear call.
         * @param connId The connId of the call to clear
         * @param reasons reasons
         * @param extensions extensions
         */
        public void ClearCall(
                String connId,
                KeyValueCollection reasons,
                KeyValueCollection extensions
        )   
        {
            try 
            {
                VoicereadyData clearData = new VoicereadyData(
                    (reasons != null) ? reasons.ToListKvpair() : null,
                    (extensions != null) ? extensions.ToListKvpair() : null
                );
                ClearData data = new ClearData
                {
                    Data = clearData
                };

                ApiSuccessResponse response = this.voiceApi.Clear(connId, data);
                Util.ThrowIfNotOk("ClearCall", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("ClearCall failed.", e);
            }
        }

        /**
         * Start call recording
         * @param connId The id of the call to start recording.
         */
        public void StartRecording(String connId)   
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.StartRecording(connId);
                Util.ThrowIfNotOk("StartRecording", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("StartRecording failed.", e);
            }
        }

        /**
         * Pause call recording.
         * @param connId The id of the call to pause recording on.
         */
        public void PauseRecording(String connId)   
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.PauseRecording(connId);
                Util.ThrowIfNotOk("PauseRecording", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("PauseRecording failed.", e);
            }
        }

        /**
         * Resume call recording.
         * @param connId The id of the call to resume recording.
         */
        public void ResumeRecording(String connId)   
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.ResumeRecording(connId);
                Util.ThrowIfNotOk("ResumeRecording", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("ResumeRecording failed.", e);
            }
        }

        /**
         * Stop call recording
         * @param connId The id of the call to stop recording.
         */
        public void StopRecording(String connId)   
        {
            try 
            {
                ApiSuccessResponse response = this.voiceApi.StopRecording(connId);
                Util.ThrowIfNotOk("StopRecording", response);
            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("StopRecording failed.", e);
            }
        }

        void setVoiceApi(Genesys.Workspace.Api.VoiceApi api)
        {
            voiceApi = api;
        }
    }
}
