using ModelProvider;
using ModelProvider.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

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
                { typeof(AutoView), "/autos" },
                { typeof(RepairView), "/repair" },
                { typeof(RepairLogView), "/logs" },
                { typeof(TechnicianView), "/technicians" },
                { typeof(ClientView), "/clients" },
                { typeof(BonusView), "/bonus" },
                { typeof(ManagerView), "/managers" },
                { typeof(UserView), "/users" },
                { typeof(BonusRepairView), "/br" }
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
                MessageBox.Show(result.Content.ToString(), result.ReasonPhrase);
            }
            var rawContent = result.Content.ReadAsStringAsync().Result;
            var parserSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.DeserializeObject<List<TEntity>>(rawContent, parserSettings);
        }
        public bool UploadRepair(RepairView repair)
        {
            string URIPart = URIparts[typeof(RepairView)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(repair, options);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
    
            var result = httpClient.PostAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
            return result.IsSuccessStatusCode;
        }
        public bool UploadUpdatedRepair(RepairView repair)
        {         
            string URIPart = URIparts[typeof(RepairView)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(repair, options);
            var rawData = new StringContent(json, Encoding.UTF8, "application/json");
          
            var result = httpClient.PutAsync(baseUri + URIPart, rawData).Result;

            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
            return result.IsSuccessStatusCode;
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
        public bool ValidateLicencePlate(string u)
        {
            string URIPart = URIparts[typeof(AutoView)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
           
            var result = httpClient.GetAsync(baseUri + URIPart + "/plate"+ "/"+u).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());                
            }
            var valid =  JsonConvert.DeserializeObject(response, options).ToString();
            return Regex.IsMatch(valid, @"[Tt]rue");
        }
        public bool ValidateClientEmail(string u)
        {
            string URIPart = URIparts[typeof(ClientView)];
            var options = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };      
            var result = httpClient.GetAsync(baseUri + URIPart + "/email/"+u).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            if (!result.IsSuccessStatusCode)
            {
                MessageBox.Show(result.ReasonPhrase, result.StatusCode.ToString());
            }
            var valid = JsonConvert.DeserializeObject(response, options).ToString();
            return Regex.IsMatch(valid, @"[Tt]rue");
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
