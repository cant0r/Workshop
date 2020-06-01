using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class JobTechnician
    {
        public long JobId { get; set; }
        public virtual Job Job { get; set; }

        public long TechnicianId { get; set; }
        public virtual Technician Technician { get; set; }
    }
}
