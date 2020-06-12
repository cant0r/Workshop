using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelProvider
{
    public class Discount
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public int Percentage { get; set; }
    }
}
