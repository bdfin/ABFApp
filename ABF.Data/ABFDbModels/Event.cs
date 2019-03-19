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
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
      
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Display(Name = "Member Only")]
        public bool IsMemberOnly { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Display(Name = "Event Image")]
        public int ImageId { get; set; }

        public virtual Location Location { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
