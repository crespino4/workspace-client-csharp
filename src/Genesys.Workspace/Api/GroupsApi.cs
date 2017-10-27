/* 
 * Workspace API
 *
 * Agent API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using Genesys.Workspace.Client;
using Genesys.Workspace.Model;

namespace Genesys.Workspace.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IGroupsApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Search for users by specific group ID
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>ApiSuccessResponse</returns>
        ApiSuccessResponse GetGroupUsers (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null);

        /// <summary>
        /// Search for users by specific group ID
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>ApiResponse of ApiSuccessResponse</returns>
        ApiResponse<ApiSuccessResponse> GetGroupUsersWithHttpInfo (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Search for users by specific group ID
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>Task of ApiSuccessResponse</returns>
        System.Threading.Tasks.Task<ApiSuccessResponse> GetGroupUsersAsync (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null);

        /// <summary>
        /// Search for users by specific group ID
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>Task of ApiResponse (ApiSuccessResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<ApiSuccessResponse>> GetGroupUsersAsyncWithHttpInfo (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class GroupsApi : IGroupsApi
    {
        private Genesys.Workspace.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public GroupsApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            ExceptionFactory = Genesys.Workspace.Client.Configuration.DefaultExceptionFactory;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public GroupsApi(Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = Genesys.Workspace.Client.Configuration.DefaultExceptionFactory;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Genesys.Workspace.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public Dictionary<String, String> DefaultHeader()
        {
            return this.Configuration.DefaultHeader;
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Search for users by specific group ID 
        /// </summary>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>ApiSuccessResponse</returns>
        public ApiSuccessResponse GetGroupUsers (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null)
        {
             ApiResponse<ApiSuccessResponse> localVarResponse = GetGroupUsersWithHttpInfo(groupId, searchTerm, sort, limit, offset);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Search for users by specific group ID 
        /// </summary>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>ApiResponse of ApiSuccessResponse</returns>
        public ApiResponse< ApiSuccessResponse > GetGroupUsersWithHttpInfo (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null)
        {
            // verify the required parameter 'groupId' is set
            if (groupId == null)
                throw new ApiException(400, "Missing required parameter 'groupId' when calling GroupsApi->GetGroupUsers");

            var localVarPath = "/groups/{groupId}/users";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (groupId != null) localVarPathParams.Add("groupId", Configuration.ApiClient.ParameterToString(groupId)); // path parameter
            if (searchTerm != null) localVarQueryParams.Add("searchTerm", Configuration.ApiClient.ParameterToString(searchTerm)); // query parameter
            if (sort != null) localVarQueryParams.Add("sort", Configuration.ApiClient.ParameterToString(sort)); // query parameter
            if (limit != null) localVarQueryParams.Add("limit", Configuration.ApiClient.ParameterToString(limit)); // query parameter
            if (offset != null) localVarQueryParams.Add("offset", Configuration.ApiClient.ParameterToString(offset)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetGroupUsers", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ApiSuccessResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiSuccessResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiSuccessResponse)));
        }

        /// <summary>
        /// Search for users by specific group ID 
        /// </summary>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>Task of ApiSuccessResponse</returns>
        public async System.Threading.Tasks.Task<ApiSuccessResponse> GetGroupUsersAsync (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null)
        {
             ApiResponse<ApiSuccessResponse> localVarResponse = await GetGroupUsersAsyncWithHttpInfo(groupId, searchTerm, sort, limit, offset);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Search for users by specific group ID 
        /// </summary>
        /// <exception cref="Genesys.Workspace.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="groupId">The id of the group to get users for</param>
        /// <param name="searchTerm">The text to search for (optional)</param>
        /// <param name="sort">Desired sort order (asc or desc). asc if not specified (optional)</param>
        /// <param name="limit">Number of results. 100 if not specified. (optional)</param>
        /// <param name="offset">Offset of page to start from. 0 if not specified. (optional)</param>
        /// <returns>Task of ApiResponse (ApiSuccessResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ApiSuccessResponse>> GetGroupUsersAsyncWithHttpInfo (decimal? groupId, string searchTerm = null, string sort = null, decimal? limit = null, decimal? offset = null)
        {
            // verify the required parameter 'groupId' is set
            if (groupId == null)
                throw new ApiException(400, "Missing required parameter 'groupId' when calling GroupsApi->GetGroupUsers");

            var localVarPath = "/groups/{groupId}/users";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (groupId != null) localVarPathParams.Add("groupId", Configuration.ApiClient.ParameterToString(groupId)); // path parameter
            if (searchTerm != null) localVarQueryParams.Add("searchTerm", Configuration.ApiClient.ParameterToString(searchTerm)); // query parameter
            if (sort != null) localVarQueryParams.Add("sort", Configuration.ApiClient.ParameterToString(sort)); // query parameter
            if (limit != null) localVarQueryParams.Add("limit", Configuration.ApiClient.ParameterToString(limit)); // query parameter
            if (offset != null) localVarQueryParams.Add("offset", Configuration.ApiClient.ParameterToString(offset)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetGroupUsers", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ApiSuccessResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (ApiSuccessResponse) Configuration.ApiClient.Deserialize(localVarResponse, typeof(ApiSuccessResponse)));
        }

    }
}
