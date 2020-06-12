﻿using ModelProvider;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Windows.Automation;

namespace Client_Manager.Models
{
    public sealed class ManagerService
    {
        public List<Auto> Autos { get; private set; }
        public List<Client> Clients { get; private set; }
        public List<Repair> Repairs { get; private set; }
        public List<RepairLog> RepairLogs { get; private set; }
        public List<Technician> Technicians { get; private set; }

        private WorkshopClient workshopClient;

        private static readonly ManagerService instance;


        static ManagerService()
        {
            instance = new ManagerService(); 
        }
        private ManagerService() 
        {
            workshopClient = WorkshopClient.GetInstance();
            
        }
        public static ManagerService GetInstance()
        {
            instance?.ParseDatabase();
            return instance;
        }

        public void ParseDatabase()
        {
            Autos = workshopClient.RetrieveEntities<Auto>() ?? new List<Auto>();
            Clients = workshopClient.RetrieveEntities<Client>() ?? new List<Client>();
            Repairs = workshopClient.RetrieveEntities<Repair>() ?? new List<Repair>();
            RepairLogs = workshopClient.RetrieveEntities<RepairLog>() ?? new List<RepairLog>();
            Technicians = workshopClient.RetrieveEntities<Technician>() ?? new List<Technician>();
        }

    }
}