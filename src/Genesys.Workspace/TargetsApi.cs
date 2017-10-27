using System;
using System.Collections.Generic;
using Genesys.Workspace.Client;
using Genesys.Workspace.Model;

namespace Genesys.Workspace
{
    public class TargetsApi
    {
        private Genesys.Workspace.Api.TargetsApi targetsApi;

        public TargetsApi()
        {
        }

        void initialize(ApiClient client)
        {
            this.targetsApi = new Api.TargetsApi(client.Configuration);
        }

        void setTargetsApi(Api.TargetsApi targetsApi)
        {
            this.targetsApi = targetsApi;
        }

        public TargetSearchResult search(String searchTerm)
        {
            return search(searchTerm, new TargetsSearchOptions());
        }

        public TargetSearchResult search(String searchTerm, TargetsSearchOptions options)
        {
            try 
            {
                TargetsResponse response = this.targetsApi.Get(searchTerm);

                TargetsResponseData data = response.Data;

                List<Target> targets = new List<Target>();
                if (data.Targets != null)
                {
                    foreach (Genesys.Workspace.Model.Target t in data.Targets)
                    {
                        targets.Add(new Target(t));
                    }
                }
                return new TargetSearchResult((long)data.TotalMatches, targets);

            } 
            catch (ApiException e) 
            {
                throw new WorkspaceApiException("searchTargets failed.", e);
            }
        }
    }
}
