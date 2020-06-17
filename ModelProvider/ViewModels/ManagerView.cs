using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelProvider.ViewModels
{
    public class ManagerView : IViewModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public UserView User { get; set; }

        public long UserId { get; set; }
    }
}
