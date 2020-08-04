using System;
using System.Collections.Generic;

namespace ProjectTracker.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Sex { get; set; }

        public DateTime Birthday { get; set; }
        
        public List<Activity> Activities{ get; set; }
        
        
        
    }
}