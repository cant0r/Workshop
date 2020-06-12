using ModelProvider;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Windows.Automation;

namespace Client_Manager.Models
{
    public sealed class Manager
    {
        public List<Auto> Autos { get; private set; }
        public List<Client> Clients { get; private set; }
        public List<Repair> Repairs { get; private set; }
        public List<RepairLog> RepairLogs { get; private set; }
        public List<Technician> Technicians { get; private set; }

        private WorkshopClient workshopClient;

        private static readonly Manager instance;


        static Manager()
        {
            instance = new Manager(); 
        }
        private Manager() 
        {
            workshopClient = WorkshopClient.GetInstance();
        }
        public static Manager GetInstance()
        {
            return instance;
        }

        public void ParseDatabase()
        {
            Autos = workshopClient.RetrieveEntities<Auto>();
            Clients = workshopClient.RetrieveEntities<Client>();
            Repairs = workshopClient.RetrieveEntities<Repair>();
            RepairLogs = workshopClient.RetrieveEntities<RepairLog>();
            Technicians = workshopClient.RetrieveEntities<Technician>();
        }

    }
}
