using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelProvider.Models
{
    public class Bonus : IModell
    {    
        [Key]
        public string Name { get; set; }

        [Required]
        public long Price { get; set; }

        public virtual IList<BonusRepair> BonusRepairs { get; set; }
    }
}
