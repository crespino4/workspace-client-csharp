using System;
using System.Collections.Generic;
using Genesys.Workspace.Model;

namespace Genesys.Workspace
{
    public enum TargetType
    {
        AGENT,
        AGENT_GROUP,
        ACD_QUEUE,
        ROUTE_POINT,
        SKILL,
        CUSTOM_CONTACT
    }

    public class Target
    {
        public string Name { get; protected set; }
        public string Number { get; protected set; }
        public TargetType Type { get; protected set; }
        public object availability { get; protected set; }

        public Target(Model.Target target)
        {
            this.Name = target.Name;
            this.Number = target.Number;
            switch (target.Type)
            {
                case Model.Target.TypeEnum.Agent:
                    this.Type = TargetType.AGENT;
                    this.extractAgentAvailability(target);
                    break;

                case Model.Target.TypeEnum.Acdqueue:
                    this.Type = TargetType.ACD_QUEUE;
                    this.extractDNAvailability(target);
                    break;

                case Model.Target.TypeEnum.Agentgroup:
                    this.Type = TargetType.AGENT_GROUP;
                    this.extractAgentGroupAvailability(target);
                    break;

                case Model.Target.TypeEnum.Routepoint:
                    this.Type = TargetType.ROUTE_POINT;
                    this.extractDNAvailability(target);
                    break;

                case Model.Target.TypeEnum.Skill:
                    this.Type = TargetType.SKILL;
                    break;

                case Model.Target.TypeEnum.Customcontact:
                    this.Type = TargetType.CUSTOM_CONTACT;
                    break;
            }
        }

        private void extractAgentAvailability(Genesys.Workspace.Model.Target target)
        {
            var availabilityData = target.Availability;
            if (availabilityData == null)
            {
                return;
            }

            //List channelsData = (List)availabilityData.get("channels");
            //List<ChannelAvailability> channels = new ArrayList<>();

            //if (channelsData != null && !channelsData.isEmpty())
            //{
            //    channelsData.forEach(o-> {
            //        LinkedTreeMap channelData = (LinkedTreeMap)o;

            //        String channelName = (String)channelData.get("name");
            //        boolean available = (Boolean)channelData.get("available");
            //        LinkedTreeMap userStateData = (LinkedTreeMap)channelData.get("userState");

            //        AgentState agentState = Util.parseAgentState((String)userStateData.get("state"));
            //        AgentWorkMode workMode = Util.parseAgentWorkMode((String)userStateData.get("workMode"));
            //        String reason = (String)userStateData.get("reason");

            //        String phoneNumber = (String)channelData.get("phoneNumber");
            //        String switchName = (String)channelData.get("switchName");
            //        AgentActivity activity = Util.parseAgentActivity((String)channelData.get("activity"));

            //        channels.add(new ChannelAvailability(channelName, available, agentState, workMode, reason, phoneNumber, switchName, activity));
            //    });
            //}

            //this.availability = new AgentAvailability(channels);
        }

        private void extractAgentGroupAvailability(Genesys.Workspace.Model.Target target)
        {
            //LinkedTreeMap availabilityData = (LinkedTreeMap)target.getAvailability();
            //if (availabilityData == null)
            //{
            //    return;
            //}

            //Integer readyAgents = ((Double)availabilityData.get("readyAgents")).intValue();
            //this.availability = new AgentGroupAvailability(readyAgents);
        }

        private void extractDNAvailability(Genesys.Workspace.Model.Target target)
        {
            //LinkedTreeMap availabilityData = (LinkedTreeMap)target.getAvailability();
            //if (availabilityData == null)
            //{
            //    return;
            //}

            //Integer waitingCalls = ((Double)availabilityData.get("waitingCalls")).intValue();
            //if (this.type == TargetType.ACD_QUEUE)
            //{
            //    this.availability = new ACDQueueAvailability(waitingCalls);
            //}
            //else
            //{
            //    this.availability = new RoutePointAvailability(waitingCalls);
            //}
        }

        //public AgentAvailability getAgentAvailability()
        //{
        //    //if (this.type != TargetType.AGENT || this.availability == null)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    return (AgentAvailability)this.availability;
        //    //}
        //}

        //public AgentGroupAvailability getAgentGroupAvailability()
        //{
        //    //if (this.type != TargetType.AGENT_GROUP || this.availability == null)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    return (AgentGroupAvailability)this.availability;
        //    //}
        //}

        //public RoutePointAvailability getRoutePointAvailability()
        //{
        //    //if (this.type != TargetType.ROUTE_POINT || this.availability == null)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    return (RoutePointAvailability)this.availability;
        //    //}
        //}

        //public ACDQueueAvailability getACDQueueAvailability()
        //{
        //    //if (this.type != TargetType.ACD_QUEUE || this.availability == null)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    return (ACDQueueAvailability)this.availability;
        //    //}
        //}

        public override string ToString()
        {
            return string.Format("[Target: Name={0}, Number={1}, Type={2}]", Name, Number, Type);
        }
    }

    public class TargetsSearchOptions
    {
        public string FilterName { get; set; }
        public TargetType[] Types { get; set; }
        public bool Desc { get; set; }
        public int? Limit { get; set; }
        public bool Exact { get; set; }
    }

    public class TargetSearchResult
    {
        public long TotalMatches { get; protected set; }
        public List<Target> Targets { get; protected set; }

        public TargetSearchResult(long totalMatches, List<Target> targets)
        {
            this.TotalMatches = totalMatches;
            this.Targets = targets;
        }
    }
}
