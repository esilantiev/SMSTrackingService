using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFeature.Models
{
    public class TrackingItem
    {
        public DateTime Date { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string ItemEvent { get; set; }
        public string Description { get; set; }
    }
}
