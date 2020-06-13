using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class Manager
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual IEnumerable<Repair> Repair { get; set; }
    }
}
