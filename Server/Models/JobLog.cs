using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class JobLog
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual Job Job { get; set; }

    }
}
