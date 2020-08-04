using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ProjectTracker.Models
{
    
    // One project can have many Employees !!!
    public class Activity
    {
        
        public int Id { get; set; }
        
        public Project Project { get; set; }
        
        public string ActivityType { get; set; }
        
        public string Role { get; set; }
        
       
        
        public Employee Employee { get; set; }
        
        // [NotMapped]
        // public double Duration
        // {
        //     get => (this.Project.DateEnd - this.Project.DateStart).TotalHours;
        // }
        //
        // [NotMapped]
        // public DateTime Date
        // {
        //     get => this.Project.DateStart.Date;
        // }
    }
}