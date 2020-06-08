using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class RepairTechnician
    {
        public long RepairID { get; set; }
        [Required]
        public virtual Repair Repair { get; set; }

        public long TechnicianId { get; set; }
        [Required]
        public virtual Technician Technician { get; set; }
    }
}
