using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.DataAccess;

namespace ProjectTracker.Controllers
{
    public class TimeTrackingController : Controller
    {
        public EmployeeContext _employeeContext;

        public TimeTrackingController(EmployeeContext context)
        {
            _employeeContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        /// <summary>
        /// Get time tracking by person id and date
        /// </summary>
        [HttpGet("{employeeId:int}/{date:DateTime}")]
        public IActionResult TimeTracking(int employeeId, DateTime date)
        {
            // Get all Projects of this day
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }

            
            
            return Json(_employeeContext.Activities
                .Where(a =>
                    a.Employee.Id == employeeId && a.Project.DateStart.Date == date.Date)
                .Select(a => new {a.Id, a.Project.Name, a.Role, a.ActivityType, 
                    Duration = (a.Project.DateEnd - a.Project.DateStart).Hours })
            );
        }
        
        /// <summary>
        /// Get time tracking by person by week number (in scope of the year)
        /// </summary>
        [HttpGet("{employeeId:int}/{week:int}")]
        public IActionResult WeekTracking(int employeeId, int week)
        {
            // Get all Projects of this day
            // Check if week exists in calendar
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }

            
            return Json(_employeeContext.Activities
                .Where(a =>
                    a.Employee.Id == employeeId && a.Project.DateStart.DayOfYear/7 == week)
                .Select(a => new {a.Id, a.Project.Name, a.Role, a.ActivityType, 
                    Duration = (a.Project.DateEnd - a.Project.DateStart).Hours })
            );

        }
    }
}