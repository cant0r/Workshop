using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider.ViewModels
{
    public class RepairTechnicianView : IViewModell
    {
        public long RepairID { get; set; }

        public long TechnicianId { get; set; }
      
    }
}
