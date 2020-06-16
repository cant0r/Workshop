using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class Repair
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual Auto Auto { get; set; }
        [Required]
        public virtual Manager Manager { get; set; }

        public string Description { get; set; }

        [Required]
        public long Price { get; set; }

        [Required]
        public virtual State State { get; set; }
     
        public virtual IList<BonusRepair> BonusRepairs { get; set; }
        
        public virtual IList<RepairTechnician> RepairTechnicians { get; set; }
    }
}
