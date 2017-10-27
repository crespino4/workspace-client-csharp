using System;
using Genesys.Workspace.Client;
using Genesys.Workspace.Model;

namespace Genesys.Workspace
{
    public class SessionApi
    {
        private Genesys.Workspace.Api.SessionApi sessionApi;

        public SessionApi()
        {
        }

        public void Initialize(ApiClient apiClient)
        {
            //this.sessionApi = new Api.SessionApi(apiClient.RestClient.BaseUrl.ToString());
            //sessionApi.Configuration.ApiClient = apiClient;
            this.sessionApi = new Api.SessionApi(apiClient.Configuration);
        }

        public ApiResponse<ApiSuccessResponse> InitializeWorkspaceWithHttpInfo(string authCode, string redirectUri, string authorization)
        {
            return this.sessionApi.InitializeWorkspaceWithHttpInfo(authCode, redirectUri, authorization);
        }

        public ApiSuccessResponse ActivateChannels(ChannelsData channelsData)
        {
            return sessionApi.ActivateChannels(channelsData);
        }

        public ApiSuccessResponse Logout()
        {
            return sessionApi.Logout();            
        }
    }
}
