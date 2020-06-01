using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class Job
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual Auto Auto { get; set; }

        public string Description { get; set; }

        [Required]
        public virtual Manager JobManager { get; set; }

        [Required]
        public virtual State WorkState { get; set; }

        [Required]
        public virtual ICollection<JobTechnician> JobTechnicians { get; set; }
    }
}
