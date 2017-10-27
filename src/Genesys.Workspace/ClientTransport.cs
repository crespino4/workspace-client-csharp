using System;
using System.Collections.Generic;
using System.Net;
using CometD.Client.Transport;

namespace Genesys.Workspace
{
    public class ClientTransport : LongPollingTransport
    {
        private IDictionary<string, object> options;
        private CookieCollection cookieCollection = new CookieCollection();

        public ClientTransport(IDictionary<string, object> options, CookieCollection cookies)
            : base(options)
        {
            this.options = options;

            foreach (Cookie cookie in cookies)
            {
                this.cookieCollection.Add(cookie);
            }
        }

        /// <summary>
        /// Returns the <see cref="Cookie"/> with a specific name from this HTTP transport.
        /// </summary>
        override public Cookie GetCookie(string name)
        {
            Cookie cookie = base.GetCookie(name);
            return cookie;
        }

        /// <summary>
        /// Adds a <see cref="Cookie"/> to this HTTP transport.
        /// </summary>
        override public void SetCookie(Cookie cookie)
        {
            this.cookieCollection.Add(cookie);

            base.SetCookie(cookie);
        }

        /// <summary>
        /// Setups HTTP request headers.
        /// </summary>
        override protected void ApplyRequestHeaders(HttpWebRequest request)
        {
            if (null == request)
                throw new ArgumentNullException("request");

            foreach (String key in this.options.Keys)
            {
                request.Headers[key] = (string)this.options[key];
            }
        }

        /// <summary>
        /// Setups HTTP request cookies.
        /// </summary>
        override protected void ApplyRequestCookies(HttpWebRequest request)
        {
            if (null == request)
                throw new ArgumentNullException("request");

            request.CookieContainer = new CookieContainer();

            foreach (Cookie c in this.cookieCollection)
            {
                request.CookieContainer.Add(new Cookie(c.Name, c.Value, "/", "api-usw1.genhtcc.com"));
            }
        }
    }
}

