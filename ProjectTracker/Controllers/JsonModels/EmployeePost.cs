using System;

namespace ProjectTracker.Controllers.JsonModels
{
    public class EmployeePost
    {
        
        public string Name { get; set; }
        
        public string Sex { get; set; }

        public DateTime Birthday { get; set; }
    }
}