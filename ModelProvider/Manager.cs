using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        [Required]
        public User User { get; set; }

        public long UserId { get; set; }
        public virtual ICollection<Repair> Repair { get; set; }
    }
}
