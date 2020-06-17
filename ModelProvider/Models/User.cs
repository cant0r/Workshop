using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ModelProvider.Models
{
    public class User : IModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool isManager { get; set; }

    }
}
