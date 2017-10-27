using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using CometD.Bayeux;
using CometD.Bayeux.Client;
using CometD.Client;
using Genesys.Workspace.Client;
using RestSharp;

namespace Genesys.Workspace
{
    public class Notifications
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //public event CometDEventHandler CometDEventReceived;
        public delegate void CometDEventHandler(IClientSessionChannel channel, IMessage message, BayeuxClient client);

        private BayeuxClient bayeuxClient;
        private Dictionary<string, CometDEventHandler> subscriptions;
        //private CookieContainer cookieContainer;

        public Notifications()
        {
            //cookieContainer = new CookieContainer();
            subscriptions = new Dictionary<string, CometDEventHandler>();
        }

        public void Initialize(ApiClient apiClient)
        {
            var options = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            foreach(string key in apiClient.Configuration.DefaultHeader.Keys)
            {
                switch(key)
                {
                    case "x-api-key":
                    case "Cookie":
                        options.Add(key, apiClient.Configuration.DefaultHeader[key]);
                        break;
                }
            }

            //this.cookieContainer = apiClient.RestClient.CookieContainer;

            /**
             * GWS currently only supports LongPolling as a method to receive events.
             * So tell the CometD library to negotiate a handshake with GWS and setup a LongPolling session.
             */
            ClientTransport transport = new ClientTransport(options, apiClient.RestClient.CookieContainer.GetCookies(new Uri(apiClient.RestClient.BaseUrl.ToString() + "/notifications")));

            bayeuxClient = new BayeuxClient(apiClient.RestClient.BaseUrl.ToString() + "/notifications", transport);

            if (bayeuxClient.Handshake(null, 30000))
            {
                foreach(string channelName in subscriptions.Keys )
                {
                    IClientSessionChannel channel = bayeuxClient.GetChannel(channelName);
                    channel.Subscribe(new CallbackMessageListener<BayeuxClient>(OnMessageReceived, bayeuxClient));
                }
            }
            else
            {
                throw new Exception("CometD handshake failed");
            }
        }

        public void subscribe(String channelName, CometDEventHandler eventHandler)
        {
            subscriptions.Add(channelName, eventHandler);
        }

        public void Disconnect()
        {
            if (bayeuxClient != null && bayeuxClient.IsConnected)
            {
                bayeuxClient.Disconnect();
            }
        }

        public void OnMessageReceived(IClientSessionChannel channel, IMessage message, BayeuxClient client)
        {
            //if (CometDEventReceived != null)
            //{
            //    CometDEventReceived(channel, message, client);
            //}

            try
            {
                subscriptions[message.Channel](channel, message, client);
            }
            catch(Exception exc)
            {
                log.Error("Execption handling OnMessageReceived for " + message.Channel, exc);
            }
        }

    }
}
