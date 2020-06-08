using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class RepairLog
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual Repair Repair { get; set; }

    }
}
