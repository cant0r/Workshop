﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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

        [Required]
        public User User { get; set; }

        public long UserId { get; set; }
        public virtual IList<RepairTechnician> RepairTechnicians { get; set; }

        public override string ToString()
        {
            return Id + " " + " " + Name + " " + Email + " " + PhoneNumber;
        }

    }
}
