using System;
using System.Collections.Generic;

namespace Genesys.Workspace
{
    public class User
    {
        public int dbid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string employeeId { get; set; }
        public string agentId { get; set; }
        public string defaultPlace { get; set; }
        public int tenantDBID { get; set; }
        public KeyValueCollection userProperties { get; set; }

        public override string ToString()
        {
            return string.Format("[User: dbid={0}, firstName={1}, lastName={2}, userName={3}, employeeId={4}, agentId={5}, defaultPlace={6}, tenantDBID={7}, userProperties={8}]", dbid, firstName, lastName, userName, employeeId, agentId, defaultPlace, tenantDBID, userProperties);
        }
    }

    public class ActionCode
    {
        public string name { get; set; }
        public string code { get; set; }
        public ActionCodeType type { get; set; }
        public List<SubCode> subCodes { get; set; }
        public KeyValueCollection userProperties { get; set; }

        public ActionCode() {}

        public override string ToString()
        {
            return string.Format("[ActionCode: name={0}, code={1}, type={2}, subCodes={3}, userProperties={4}]", name, code, type, subCodes, userProperties);
        }
    }

    public class SubCode
    {
        public string name { get; set; }
        public string code { get; set; }

        public override string ToString()
        {
            return string.Format("[SubCode: name={0}, code={1}]", name, code);
        }
    }

    public class AgentGroup
    {
        public int dbid { get; set; }
        public string name { get; set; }
        public KeyValueCollection userProperties { get; set; }

        public override string ToString()
        {
            return string.Format("[AgentGroup: name={0}, dbid={1}, userProperties={2}]", name, dbid, userProperties);
        }
    }

    public class Transaction
    {
        public string name { get; set; }
        public string alias { get; set; }
        public KeyValueCollection userProperties { get; set; }

        public override string ToString()
        {
            return string.Format("[Transaction: name={0}, alias={1}, userProperties={2}]", name, alias, userProperties);
        }
    }

    public class BusinessAttribute
    {
        public int dbid { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public List<BusinessAttributeValue> values { get; set; }

        public override string ToString()
        {
            return string.Format("[BusinessAttribute: dbid={0}, name={1}, displayName={2}, description={3}, values={4}]", dbid, name, displayName, description, values);
        }
    }


    public class BusinessAttributeValue
    {
        public int dbid { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public object defaultValue { get; set; }

        public override string ToString()
        {
            return string.Format("[BusinessAttributeValue: dbid={0}, name={1}, displayName={2}, description={3}, defaultValue={4}]", dbid, name, displayName, description, defaultValue);
        }
    }

    public class Dn
    {
        public string number { get; set; }
        public string switchName { get; set; }
        public string agentId { get; set; }
        public string telephonyNetwork { get; set; }
        public AgentState agentState { get; set; }
        public AgentWorkMode workMode { get; set; }
        public string forwardTo { get; set; }
        public bool dnd { get; set; }
        public DnCapability[] capabilities { get; set; }

        public override string ToString()
        {
            return string.Format("[Dn: number={0}, switchName={1}, agentId={2}, telephonyNetwork={3}, agentState={4}, workMode={5}, forwardTo={6}, dnd={7}, capabilities={8}]", number, switchName, agentId, telephonyNetwork, agentState, workMode, forwardTo, dnd, CapabilitiesAsString(capabilities));
        }

        public string CapabilitiesAsString(DnCapability[] caps)
        {
            List<string> capabilities = new List<string>();

            if (caps != null)
            {
                foreach (DnCapability c in caps)
                {
                    capabilities.Add(c.ToString());
                }
            }

            return "[" + String.Join(",", capabilities) + "]";
        }
    }

    public class Call
    {
        public string id { get; set; }
        public string callUuid { get; set; }
        public CallState state { get; set; }
        public string parentConnId { get; set; }
        public string previousConnId { get; set; }
        public string callType { get; set; }
        public string ani { get; set; }
        public string dnis { get; set; }
        public string recordingState { get; set; }
        public string[] participants { get; set; }
        public KeyValueCollection userData { get; set; }
        public CallCapability[] capabilities { get; set; }

        public override string ToString()
        {
            return string.Format("[Call: id={0}, callUuid={1}, state={2}, parentConnId={3}, previousConnId={4}, callType={5}, ani={6}, dnis={7}, recordingState={8}, participants={9}, userData={10}, capabilities={11}]", id, callUuid, state, parentConnId, previousConnId, callType, ani, dnis, recordingState, this.ParticipantsAsString(participants), userData, this.CapabilitiesAsString(capabilities));
        }

        public string ParticipantsAsString(string[] participants)
        {
            String temp = "";

            if ( participants != null )
            {
                temp = String.Join(",", participants);
            }

            return "[" + temp + "]";
        }

        public string CapabilitiesAsString(CallCapability[] caps)
        {
            List<string> capabilities = new List<string>();

            if (caps != null)
            {
                foreach (CallCapability c in caps)
                {
                    capabilities.Add(c.ToString());
                }
            }

            return "[" + String.Join(",", capabilities) + "]";
        }
    }
}
