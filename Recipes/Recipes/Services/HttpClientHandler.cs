using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Recipes.Utility;

namespace Recipes.Services
{
    public abstract class HttpClientHandler : IHttpClientHandler
    {
        string errorMessage = string.Empty;

        public HttpClientHandler()
        {
            
        }

        public async Task<string> GetDataFromServer(string strRequestUri)
        {
            string strServiceResult = string.Empty;
            try
            {
                //Check network connection is available or not
                var NetworkAvailable = Connectivity.NetworkAccess;
                if (NetworkAvailable == NetworkAccess.Internet)
                {

                    Uri uri = new Uri(strRequestUri);
                    var httpClientHandler = new System.Net.Http.HttpClientHandler();

                    using (var client = new HttpClient(httpClientHandler))
                    {
                        var cancelSource = new CancellationTokenSource();
                        var content = client.GetAsync(uri, cancelSource.Token);

                        if (Task.WaitAny(new Task[] { content }, TimeSpan.FromSeconds(Constant.TimeOut_30Sec)) < 0)
                        {
                            cancelSource.Cancel(); // attempt to cancel the HTTP request
                            strServiceResult = Constant.Timeout;
                        }
                        else
                        {
                            using (HttpResponseMessage response = await content)
                            {

                                if (response.IsSuccessStatusCode)
                                {
                                    strServiceResult = await response.Content.ReadAsStringAsync();
                                }
                                else
                                {
                                    strServiceResult = Constant.Failure;
                                }

                            }
                        }

                    }
                }
                else
                {
                    strServiceResult = Constant.NoInternet;
                }
            }
            catch (HttpRequestException rex)
            {
                if (rex.Message == Constant.NoInternet)
                {
                    strServiceResult = Constant.NoInternet;

                }
                else
                {
                    strServiceResult = Constant.Failure;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Constant.NoInternet)
                {
                    strServiceResult = Constant.NoInternet;
                }
                else
                {
                    strServiceResult = Constant.Failure;
                }
            }
            return strServiceResult;
        }

    }
}
