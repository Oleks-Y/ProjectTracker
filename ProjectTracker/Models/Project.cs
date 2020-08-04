using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateStart { get; set; }
        
        public DateTime DateEnd { get; set; }
        
        public Activity Activity { get; set; }
        
        public int ActivityForeignKey  { get; set; }
        
        

    }
}