using ModelProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Navigation;

namespace Client_Technician.Models
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
            baseUri = "https://localhost:5001/api/workshop/technician";
            URIparts = new Dictionary<Type, string>()
            {
                { typeof(Repair), "/repair" },
                { typeof(RepairLog), "/logs" },
                { typeof(User), "/users" },
                { typeof(Technician), "/technicians" }
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
            string URIPart;
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
                MessageBox.Show(result.Content.ToString(), result.ReasonPhrase);
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = null };
            return JsonConvert.DeserializeObject<List<TEntity>>(rawContent, parserSettings);
        }
        public List<Technician> GetTechniciansByRepairId(Repair r)
        {
            string URIPart;
            try
            {
                URIPart = URIparts[typeof(Technician)];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "NULL vagy kulcs nem létezik hiba", MessageBoxButton.OK);
                return null;
            }
            var result = httpClient.GetAsync(baseUri + URIPart + "/" + r.Id.ToString()).Result;
            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.Content.ToString(), result.ReasonPhrase);
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = null };
            return JsonConvert.DeserializeObject<List<Technician>>(rawContent, parserSettings);
        }
        public List<Repair> GetRepairsByTechnicianId(Technician r)
        {
            string URIPart;
            try
            {
                URIPart = URIparts[typeof(Repair)];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "NULL vagy kulcs nem létezik hiba", MessageBoxButton.OK);
                return null;
            }
            var result = httpClient.GetAsync(baseUri + URIPart + "/" + r.Id.ToString()).Result;
            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.Content.ToString(), result.ReasonPhrase);
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = null };
            return JsonConvert.DeserializeObject<List<Repair>>(rawContent, parserSettings);
        }
        public void UpdateRepair(Repair repair)
        {
            string URIPart = URIparts[typeof(Repair)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(repair, options);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
          
            var result = httpClient.PutAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
        }

        public bool ValidateUser(User u)
        {
            string URIPart = URIparts[typeof(User)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(u, options);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
      
            var result = httpClient.PostAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
                return false;
            }
            return true;
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
