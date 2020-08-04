using System;

namespace ProjectTracker.Controllers.JsonModels
{
    public class ProjectPost
    {
        public DateTime DateStart { get; set; }
        
        public DateTime DateEnd { get; set; }
        //public int Hours { get; set; }

        public string Role { get; set; }

        public string Project { get; set; } 
        
        public string ActivityType { get; set; }
    }
}