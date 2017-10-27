using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Genesys.Workspace.Model;

namespace Genesys.Workspace
{
    public enum StatusCode : int
    {
        OK = 0,
        ASYNC_OK = 1
    }

    public enum AgentState
    {
        Unknown,
        LoggedOut,
        LoggedIn,
        Ready,
        NotReady,
        OutOfService
    }

    public enum AgentWorkMode
    {
        Unknown,
        AuxWork,
        AfterCallWork,
        AutoIn,
        ManualIn
    }

    public enum ActionCodeType
    {
        UNKNOWN,
        LOGIN,
        LOGOUT,
        READY,
        NOT_READY,
        BUSY_ON,
        BUSY_OFF,
        FORWARD_ON,
        FORWARD_OFF,
        INTERNAL_CALL,
        INBOUND_CALL,
        OUTBOUND_CALL,
        CONFERENCE,
        TRANSFER      
    }

    public enum CallState
    {
        UNKNOWN,
        RINGING,
        DIALING,
        ESTABLISHED,
        HELD,
        RELEASED,
        COMPLETED   
    }

    public enum NotificationType
    {
        UNKNOWN,
        STATE_CHANGE,
        PARTICIPANTS_UPDATED,
        ATTACHED_DATA_CHANGED,
        CALL_RECOVERED
    }

    public enum AgentActivity
    {
        UNKNOWN,
        IDLE,
        HANDLING_INBOUND_CALL,
        HANDLING_INTERNAL_CALL,
        HANDLING_OUTBOUND_CALL,
        HANDLING_CONSULT_CALL,
        INITIATING_CALL,
        RECEIVING_CALL,
        CALL_ON_HOLD,
        HANDLING_INBOUND_INTERACTION,
        HANDLING_INTERNAL_INTERACTION,
        HANDLING_OUTBOUND_INTERACTION,
        DELIVERING_INTERACTION
    }

    public enum DnCapability
    {
        ready,
        not_ready,
        dnd_on,
        dnd_off,
        set_forward,
        cancel_forward,
        login,
        logoff
    }

    public enum CallCapability
    {
        answer,
        release,
        hold,
        retrieve,
        send_dtmf,
        attach_user_data,
        delete_user_data_pair,
        update_user_data,
        initiate_conference,
        initiate_transfer,
        complete_conference,
        complete_transfer,
        single_step_conference,
        single_step_transfer,
        delete_from_conference,
        start_recording,
        stop_recording,
        pause_recording,
        resume_recording,
        switch_to_listen_in,
        switch_to_coaching,
        switch_to_barge_in,
        alternate,
        clear,
        reconnect,
        redirect,
        complete,
        merge
    }

    public static class Util
    {
        public static ActionCodeType parseActionCodeType(String input)
        {

            ActionCodeType type = ActionCodeType.UNKNOWN;
            if (input == null)
            {
                return type;
            }

            switch (input)
            {
                case "Login":
                    type = ActionCodeType.LOGIN;
                    break;

                case "Logout":
                    type = ActionCodeType.LOGOUT;
                    break;

                case "Ready":
                    type = ActionCodeType.READY;
                    break;

                case "NotReady":
                    type = ActionCodeType.NOT_READY;
                    break;

                case "BusyOn":
                    type = ActionCodeType.BUSY_ON;
                    break;

                case "BusyOff":
                    type = ActionCodeType.BUSY_OFF;
                    break;

                case "ForwardOn":
                    type = ActionCodeType.FORWARD_ON;
                    break;

                case "Forwardoff":
                    type = ActionCodeType.FORWARD_OFF;
                    break;

                case "InternalCall":
                    type = ActionCodeType.INTERNAL_CALL;
                    break;

                case "InboundCall":
                    type = ActionCodeType.INBOUND_CALL;
                    break;

                case "OutboundCall":
                    type = ActionCodeType.OUTBOUND_CALL;
                    break;

                case "Conference":
                    type = ActionCodeType.CONFERENCE;
                    break;

                case "Transfer":
                    type = ActionCodeType.TRANSFER;
                    break;
            }

            return type;
        }


        public static void extractKeyValueData(KeyValueCollection userData, ArrayList data)
        {
            if (data == null)
            {
                return;
            }

            for (int i = 0; i < data.Count; i++)
            {
                IDictionary<String, Object> pair = (IDictionary<String, Object>)data[i];
                string key = (string)pair["key"];
                string type = (string)pair["type"];

                switch (type)
                {
                    case "str":
                        userData.addString(key, (String)pair["value"]);
                        break;
                    
                    case "int":
                        userData.addInt(key, (int)pair["value"]);
                        break;

                    case "kvlist":
                        KeyValueCollection list = new KeyValueCollection();
                        Util.extractKeyValueData(list, (ArrayList)pair["value"]);
                        userData.addList(key, list);
                        break;
                }
            }
        }

        public static String[] extractParticipants(ArrayList data)
        {
            String[] participants = new String[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                IDictionary<String, Object> p = (IDictionary<String, Object>)data[i];
                String number = (String)p["number"];
                participants[i] = number;
            }

            return participants;
        }

        public static CallState parseCallState(String input)
        {
            CallState state = CallState.UNKNOWN;
            if (input != null)
            {
                switch (input)
                {
                    case "Ringing":
                        state = CallState.RINGING;
                        break;

                    case "Dialing":
                        state = CallState.DIALING;
                        break;

                    case "Established":
                        state = CallState.ESTABLISHED;
                        break;

                    case "Held":
                        state = CallState.HELD;
                        break;

                    case "Released":
                        state = CallState.RELEASED;
                        break;

                    case "Completed":
                        state = CallState.COMPLETED;
                        break;
                }
            }

            return state;
        }

        public static NotificationType parseNotificationType(String input)
        {
            NotificationType type = NotificationType.UNKNOWN;
            if (input != null)
            {
                switch (input)
                {
                    case "StateChange":
                        type = NotificationType.STATE_CHANGE;
                        break;

                    case "ParticipantsUpdated":
                        type = NotificationType.PARTICIPANTS_UPDATED;
                        break;

                    case "AttachedDataChanged":
                        type = NotificationType.ATTACHED_DATA_CHANGED;
                        break;

                    case "CallRecovered":
                        type = NotificationType.CALL_RECOVERED;
                        break;
                }
            }

            return type;
        }

        public static AgentActivity parseAgentActivity(String input)
        {
            AgentActivity activity = AgentActivity.UNKNOWN;
            if (input != null)
            {
                switch (input)
                {
                    case "Idle":
                        activity = AgentActivity.IDLE;
                        break;

                    case "HandlingInboundCall":
                        activity = AgentActivity.HANDLING_INBOUND_CALL;
                        break;

                    case "HandlingInternalCall":
                        activity = AgentActivity.HANDLING_INTERNAL_CALL;
                        break;

                    case "HandlingOutboundCall":
                        activity = AgentActivity.HANDLING_OUTBOUND_CALL;
                        break;

                    case "HandlingConsultCall":
                        activity = AgentActivity.HANDLING_CONSULT_CALL;
                        break;

                    case "InitiatingCall":
                        activity = AgentActivity.INITIATING_CALL;
                        break;

                    case "ReceivingCall":
                        activity = AgentActivity.RECEIVING_CALL;
                        break;

                    case "CallOnHold":
                        activity = AgentActivity.CALL_ON_HOLD;
                        break;

                    case "HandlingInboundInteraction":
                        activity = AgentActivity.HANDLING_INBOUND_INTERACTION;
                        break;

                    case "HandlingInternalInteraction":
                        activity = AgentActivity.HANDLING_INTERNAL_INTERACTION;
                        break;

                    case "HandlingOutboundInteraction":
                        activity = AgentActivity.HANDLING_OUTBOUND_INTERACTION;
                        break;

                    case "DeliveringInteraction":
                        activity = AgentActivity.DELIVERING_INTERACTION;
                        break;


                }
            }

            return activity;
        }

        //public static Capability? CapabilityFromString(String s)
        //{
        //    s = s.Replace("-", "");

        //    foreach (Capability v in Enum.GetValues(typeof(Capability)))
        //    {
        //        if (v.ToString().Equals(s, StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            return v;
        //        }
        //    }

        //    return null;
        //}

        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            TV value;
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }

        public static void ThrowIfNotOk(String requestName, ApiSuccessResponse response)
        {
            if ( response.Status == null )
            {
                throw new WorkspaceApiException(requestName + " failed with code: <unknown>");
            }

            if (response.Status.Code != (int)StatusCode.ASYNC_OK)
            {
                throw new WorkspaceApiException(requestName + " failed with code: " + response.Status.Code);
            }
        }
    }
}
