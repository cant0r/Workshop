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
            baseUri = "https://localhost:5001/api/workshop/manager";
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "NULL vagy kulcs nem létezik hiba", MessageBoxButton.OK);
                return null;
            }
            var result = httpClient.GetAsync(baseUri + URIPart).Result;
            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show("There's no data available!");
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = null };
            return JsonConvert.DeserializeObject<List<TEntity>>(rawContent, parserSettings);
        }
        public void UploadRepair(Repair repair)
        {
            string URIPart = URIparts[typeof(Repair)];
            var parserSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = null
            };
            var json = JsonConvert.SerializeObject(repair, parserSettings);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
            MessageBox.Show(json);
            var result = httpClient.PostAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
        }
        public void UploadUpdatedRepair(Repair repair)
        {
            string URIPart = URIparts[typeof(Repair)];
            var parserSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = null
            };
            var json = JsonConvert.SerializeObject(repair, parserSettings);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
            MessageBox.Show(json);
            var result = httpClient.PutAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
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
