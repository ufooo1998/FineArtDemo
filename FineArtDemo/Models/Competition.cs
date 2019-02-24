using FineArtDemo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineArtDemo.Models
{
    public class Competition
    {
        public int ID { get; set; }
        public string CompetitionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserID { get; set; }
        public CustomizeUser User { get; set; }
        public List<CompetitionPost> CompetitionPosts { get; set; }
    }
}
