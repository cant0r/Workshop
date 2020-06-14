using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelProvider
{
    public class Bonus
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public long Price { get; set; }

    }
}
