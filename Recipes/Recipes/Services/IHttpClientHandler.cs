using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public interface IHttpClientHandler
    {
        Task<string> GetDataFromServer(string strRequestUri);
    }
}
