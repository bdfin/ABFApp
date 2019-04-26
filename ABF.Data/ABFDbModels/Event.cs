using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Event
    {
        [Display(Name = "Event ID")]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
      
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Event")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Brief Details")]
        public string Details { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Full Details")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }

        [Display(Name = "Member Only")]
        public bool IsMemberOnly { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Display(Name = "Event Image")]
        public int ImageId { get; set; }

        [Display(Name = "Book URL")]
        public string BookUrl { get; set; }

        [Display(Name = "Author's Website")]
        public string AuthorUrl { get; set; }

        public virtual Location Location { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
        public virtual Event Events { get; set; }
    }
}
