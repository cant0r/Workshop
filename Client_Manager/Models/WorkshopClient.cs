using ModelProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Navigation;

namespace Client_Manager.Models
{
    class WorkshopClient
    {
        private static readonly WorkshopClient instance;
        private readonly HttpClient httpClient;
        private readonly Dictionary<Type, string> URIparts;
        private readonly string baseUri;
        private static bool disposed = false;
        private WorkshopClient() 
        {
            httpClient = new HttpClient();
            baseUri = "https://localhost:5000/api/workshop/manager";
            URIparts = new Dictionary<Type, string>() 
            {
                { typeof(Auto), "/autos" },
                { typeof(Repair), "/repair" },
                { typeof(RepairLog), "/logs" },
                { typeof(Technician), "/technicians" },
                { typeof(Client), "/clients" },
                { typeof(Bonus), "/bonus" }
            };
          
        }
        static WorkshopClient()
        {
            instance = new WorkshopClient();
            
        }

        public static WorkshopClient GetInstance()
        {
            return instance;
        }
        public List<TEntity> RetrieveEntities<TEntity>()
        {
            string URIPart = "";
            try
            {
                URIPart = URIparts[typeof(TEntity)];
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "NULL vagy kulcs nem létezik hiba", MessageBoxButton.OK);
                return null;
            }
            var result = httpClient.GetAsync(baseUri + URIPart).Result;

            if(!result.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(result.StatusCode + '\n' + result.ReasonPhrase);
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<TEntity>>(rawContent);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                httpClient.Dispose();              
            }

            disposed = true;
        }
    }
}
