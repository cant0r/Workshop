using ModelProvider;
using ModelProvider.ViewModels;
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
            baseUri = "http://localhost:5000/api/workshop/technician";
            URIparts = new Dictionary<Type, string>()
            {
                { typeof(RepairView), "/repair" },
                { typeof(RepairLogView), "/logs" },
                { typeof(UserView), "/users" },
                { typeof(TechnicianView), "/technicians" }
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
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
            return JsonConvert.DeserializeObject<List<TEntity>>(rawContent, parserSettings);
        }
       
        public List<TechnicianView> GetTechniciansByRepairId(RepairView r)
        {
            string URIPart;
            try
            {
                URIPart = URIparts[typeof(TechnicianView)];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "NULL vagy kulcs nem létezik hiba", MessageBoxButton.OK);
                return null;
            }
            var result = httpClient.GetAsync(baseUri + URIPart + "/" + r.Id).Result;
            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.Content.ToString(), result.ReasonPhrase);
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = null };
            return JsonConvert.DeserializeObject<List<TechnicianView>>(rawContent, parserSettings);
        }
        public List<RepairView> GetRepairsByTechnicianId(TechnicianView r)
        {
            string URIPart;
            try
            {
                URIPart = URIparts[typeof(RepairView)];
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
            var obj = JsonConvert.DeserializeObject<List<RepairView>>(rawContent, parserSettings);
            return obj;
        }       
        public void UpdateRepair(RepairView repair)
        {
            using  HttpClient client = new HttpClient();
            string URIPart = URIparts[typeof(RepairView)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(repair, options);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
          
            var result = client.PutAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
        }
        public void UploadRepairLog(RepairLogView repair)
        {
            string URIPart = URIparts[typeof(RepairLogView)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(repair, options);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");

            var result = httpClient.PutAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
        }


        public bool ValidateUser(UserView u)
        {
            string URIPart = URIparts[typeof(UserView)];
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
