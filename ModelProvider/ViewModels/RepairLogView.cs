﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelProvider.ViewModels
{
    public class RepairLogView : IViewModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long TechnicianId { get; set; }

        [Required]
        public string Description { get; set; }  

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public virtual RepairView Repair { get; set; }

    }
}
