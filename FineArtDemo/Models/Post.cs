using FineArtDemo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FineArtDemo.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string PostName { get; set; }
        public string UserID { get; set; }
        public CustomizeUser User { get; set; }
        public List<CompetitionPost> CompetitionPosts { get; set; }

    }
}
