using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class StockQuantities
    {
        public Dictionary<Event, int> events { get; set; }
        public Dictionary<AddOn, int> addons { get; set; }
    }
}