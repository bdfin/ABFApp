using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Image
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public virtual Event Event { get; set; }
    }
}
