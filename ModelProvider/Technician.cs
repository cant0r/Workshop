using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class Technician
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<RepairTechnician> RepairTechnician { get; set; }

        public override string ToString()
        {
            return Id + " " + " " + Name + " " + Email + " " + PhoneNumber;
        }

    }
}
