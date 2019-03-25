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
        public Event thisevent { get; set; }

        public Location thislocation { get; set; }

    }
}