using Flurl.Http;
using Pokedex.Configuration;
using Pokedex.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Service
{
    public class BaseService : IBaseService
    {
        public string Controller { get; set; }
        public BaseService(string _controller)
        {
            Controller = _controller;
        }

        public async Task<T> GetFromApi<T>(string url) where T : class
        {
            try
            {
                string requesturl = $"{UrlConfiguration.BaseUrl()}/{Controller}/{url}";
                return await requesturl.GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                Debug.WriteLine(ex.GetResponseJsonAsync());
                return null;
            }
        }
    }
}
