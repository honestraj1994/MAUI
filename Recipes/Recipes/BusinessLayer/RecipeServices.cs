using Recipes.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.BusinessLayer
{
    public class RecipeServices : Services.HttpClientHandler
    {

        public async Task<string> GetRecipeListData()
        {
            string serviceResult = string.Empty;

            try
            {

                string requestUri = string.Format(EndPoints.RecipeList);
                serviceResult = await GetDataFromServer(requestUri);

            }
            catch (System.Net.WebException e)
            {
                if (e.Message == "Error: ConnectFailure (Network is unreachable)" || e.Message == "Error: NameResolutionFailure")
                {
                    serviceResult = "Network"; ;
                }
                else
                {
                    serviceResult = "false";
                }
            }
            catch (Exception ex)
            {
                serviceResult = "false";
                if (ex.Message.Equals("NO_INTERNET"))
                {
                    throw ex;
                }
                throw ex;
            }
            return serviceResult;
        }

    }
}
