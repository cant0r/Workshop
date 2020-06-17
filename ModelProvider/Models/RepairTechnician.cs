using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider.Models
{
    public class RepairTechnician : IModell
    {
        public long RepairID { get; set; }
        public virtual Repair Repair { get; set; }

        public long TechnicianId { get; set; }
      
        public virtual Technician Technician { get; set; }
    }
}
