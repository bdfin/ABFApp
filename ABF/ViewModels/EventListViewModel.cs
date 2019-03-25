using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class EventListViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:ddd dd MMMM}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Display(Name = "Member Only")]
        public bool IsMemberOnly { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}