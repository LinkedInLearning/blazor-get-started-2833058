using System;
using System.Collections.Generic;

namespace Beam.Shared
{
    public class Ray
    {
        public int RayId { get; set; }
        public int FrequencyId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int PrismCount { get; set; }
        public List<string> UsersPrismed { get; set; }
        public DateTime RayCastDate { get; set; }
    }
}
