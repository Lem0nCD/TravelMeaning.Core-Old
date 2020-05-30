using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMeaning.Models.Model
{
    public class GuideRoute : BaseEntity
    {
        public Guid TravelGuideId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string MapLocation { get; set; }
        public string Waypoints1 { get; set; }
        public string Waypoints2 { get; set; }
        public string Waypoints3 { get; set; }
        public string Waypoints4 { get; set; }
        public string Waypoints5 { get; set; }
        public TravelGuide TravelGuide { get; set; }
    }
}
