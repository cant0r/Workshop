using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelProvider.ViewModels
{
    public class UserView : IViewModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        public string Password { get; set; }
     
        public string Email { get; set; }

        [Required]
        public bool isManager { get; set; }

    }
}
