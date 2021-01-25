﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GatheringDay.Models
{
    public class ContactUs
    {      [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        [NotMapped]
        public string ContacterrorMsg { get; set; }
    }
}