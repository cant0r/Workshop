using ModelProvider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider.ViewModels
{
    public class RepairView : IViewModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual AutoView Auto { get; set; }
        [Required]
        public virtual ManagerView Manager { get; set; }

        public string Description { get; set; }

        [Required]
        public long Price { get; set; }

        [Required]
        public virtual State State { get; set; }
     
        public virtual IList<BonusRepairView> BonusRepairs { get; set; }
        
        public virtual IList<RepairTechnicianView> RepairTechnicians { get; set; }
    }
}
