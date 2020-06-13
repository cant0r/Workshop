using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelProvider
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
