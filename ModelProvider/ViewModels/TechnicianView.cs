using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider.ViewModels
{
    public class TechnicianView : IViewModell
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public UserView User { get; set; }

        public long UserId { get; set; }
        public virtual IList<RepairTechnicianView> RepairTechnicians { get; set; }

        public override string ToString()
        {
            return Id + " " + " " + Name + " "  + PhoneNumber;
        }

    }
}
