using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelProvider.ViewModels
{
    public class BonusView : IViewModell
    {    
        [Key]
        public string Name { get; set; }

        [Required]
        public long Price { get; set; }

        public virtual IList<BonusRepairView> BonusRepairs { get; set; }
    }
}
