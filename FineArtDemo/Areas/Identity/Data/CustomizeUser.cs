using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FineArtDemo.Models;
using Microsoft.AspNetCore.Identity;

namespace FineArtDemo.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CustomizeUser class
    public class CustomizeUser : IdentityUser
    {
        public List<Competition> Competitions { get; set; }
        public List<Post> Posts { get; set; }
    }
}
