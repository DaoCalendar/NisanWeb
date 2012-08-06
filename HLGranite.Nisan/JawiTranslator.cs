using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace HLGranite.Nisan
{
    /// <summary>
    /// Jawi Translator class.
    /// </summary>
    public class JawiTranslator
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public JawiTranslator()
        {
        }
        /// <summary>
        /// Translate rumi to jawi.
        /// </summary>
        /// <remarks>
        /// If the dependency website down this will null result.
        /// </remarks>
        /// <param name="rumi"></param>
        /// <returns></returns>
        /// <seealso>http://www.ejawi.net/v3/index?e=converter</seealso>
        public string Translate(string rumi)
        {
            string jawi = string.Empty;
            string html = string.Empty;
            string queryString = "http://www.ejawi.net/v3/getTranslationRumiJawi.php?rumi={0}&jenis=RJ&teknik=DK";

            try
            {
                WebRequest req = WebRequest.Create(string.Format(queryString, rumi));
                //req.Proxy = GetProxy();
                if (req.Proxy != null) req.Credentials = req.Proxy.Credentials;
                using (WebResponse res = req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                        html = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return jawi;
            }

            //WebClient web = new WebClient();
            //web.Proxy = GetProxy();
            //if (web.Proxy != null) web.Credentials = web.Proxy.Credentials;
            //return web.DownloadString(string.Format(queryString, rumi));

            //Sample html response
            /*
             * <div id="content" class="translation">
             * <span style="cursor: help" id="pagi" trans="ڤاݢي<"ڤاݢي</span>
             * </div>
             */
            Match match = Regex.Match(html, "trans=\".+\"");
            if (match.Success)
            {
                //trans="ڤاݢي"
                string hold = match.Groups[0].Value;
                string[] segments = hold.Split(new char[] { ' ' });
                foreach (string segment in segments)
                {
                    if (segment.Contains("trans"))
                    {
                        int first = segment.IndexOf("\"", 0);
                        int second = segment.IndexOf("\"", first + 1);
                        if ((first > 0) && (second > 0))//means segment contains 2 double quotes
                            jawi += segment.Substring(first + 1, second - first - 1) + " ";
                    }
                }
                jawi = jawi.Trim();
            }


            return jawi;
        }
        /// <summary>
        /// Get proxy from configuration.
        /// </summary>
        /// <returns></returns>
        private WebProxy GetProxy()
        {
            //TODO: move to app.setting
            string proxyName = "co-proxy-003";
            string userName = "yeang-shing.then";
            string password = "Q1w2e3r4d";

            WebProxy proxy = new WebProxy(proxyName, 80);
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                if (!userName.Contains(@"\"))
                    proxy.Credentials = new NetworkCredential(userName, password);
                else
                {
                    string[] userInfo = userName.Split('\\');
                    proxy.Credentials = new NetworkCredential(userInfo[1], password, userInfo[0]);
                }
            }

            return proxy;
        }
    }
}