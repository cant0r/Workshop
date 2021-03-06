﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider.Models
{
    public class Client : IModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required]       
        public string Email { get; set; }     
        [Required]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return Id + " " + " " + Name + " " + Email + " " + PhoneNumber;
        }
    }
}
