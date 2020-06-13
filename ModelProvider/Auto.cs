using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider
{
    public class Auto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicencePlate { get; set; }

        [Required]
        public virtual Client Client { get; set; }

        public override string ToString()
        {
            return Id + " " + Brand + " " + Model + " " + LicencePlate;
        }
    }
}
