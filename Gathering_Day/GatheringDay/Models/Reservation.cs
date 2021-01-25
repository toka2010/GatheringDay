using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GatheringDay.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime  EventDate { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public int numberOfPeople { get; set; }
        
        public string EventName { get; set; }

        [NotMapped]
        public string ReservationrMsg { get; set; }



    }   
}