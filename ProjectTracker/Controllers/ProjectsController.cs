using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using ProjectTracker.Controllers.JsonModels;
using ProjectTracker.DataAccess;
using ProjectTracker.Models;

namespace ProjectTracker.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProjectsController : Controller
    {
        public EmployeeContext _employeeContext;
        
        public ProjectsController(EmployeeContext context)
        {
            _employeeContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        /// <summary>
        /// Add new user
        /// </summary>
        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeePost emp)
        {
            
            if (emp.Name == null || emp.Sex == null || emp.Birthday == null)
            {
                return NotFound("Fields can`t be empty");
            }

            var user = _employeeContext.Employees.Add(new Employee()
            {
                Name = emp.Name,
                Sex = emp.Sex,
                Birthday = emp.Birthday
            });

            _employeeContext.SaveChanges();
            // return Json
            return Json(user.Entity.Id);

        }
        /// <summary>
        /// Get all user activities
        /// </summary>
        [HttpGet("{employeeId:int}")]
        public IActionResult AllProjects(int employeeId)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }
            
            return Json(_employeeContext.Activities.Where( a=> a.Employee == emp )
                    .Select(a => new {a.Id, a.Project.Name, a.Role, a.ActivityType,
                        Employee = new { a.Employee.Id , a.Employee.Name},
                    Duration = (a.Project.DateEnd - a.Project.DateStart).Hours }));
        
        }
        /// <summary>
        /// Get acviti by id
        /// </summary>
        [HttpGet("{employeeId:int}/{activityId:int}")]
        public IActionResult AllProjects(int employeeId, int activityId)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }
            
            // To do in get not return employee
            return Json(_employeeContext.Activities.Where( a=> a.Employee == emp && a.Id == activityId ));
        
        }
        /// <summary>
        /// Delete activity
        /// </summary>
        [HttpDelete("{employeeId:int}/{activityId:int}")]
        public IActionResult DeleteActivity(int employeeId, int activityId)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }

            var activity = _employeeContext.Activities.FirstOrDefault(a => a.Id == activityId);
            
            if (activity == null)
            {
                return NotFound("No activity");
            }


            _employeeContext.Activities.Remove(activity);
            _employeeContext.SaveChanges();
            return Ok();

        }
        /// <summary>
        /// Add activity to user
        /// </summary>
        [HttpPost("{employeeId:int}")]
        public IActionResult AddActivity(int employeeId, [FromBody] ProjectPost project)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (emp == null)
            {
                return NotFound();
            }

            emp.Activities ??= new List<Activity>();
            if (project.DateStart.Date != project.DateEnd.Date)
            {
                return NotFound("Date start and date end should be in one day");
            }

            if (project.DateEnd < project.DateStart)
            {
                return NotFound("Start date cannot be greater than end date");
            }
            
            // check project.end > projects.start
            emp.Activities.Add(new Activity()
            {
                Project = new Project()
                {
                    Name = project.Project,
                    DateStart = project.DateStart,
                    DateEnd = project.DateEnd
                },
                ActivityType = project.ActivityType,
                Role = project.Role
                
            });

            _employeeContext.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Update activity
        /// </summary>
        [HttpPut("{employeeId:int}/{activityId:int}")]
        public IActionResult PutActivity(int employeeId, int activityId, [FromBody] ProjectPost project)
        {
            
            
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (emp == null)
            {
                return NotFound();
            }

            var activity = _employeeContext.Activities.FirstOrDefault(a => a.Id == activityId && a.Employee.Id == employeeId);

            if (activity == null)
            {
                return NotFound("No activity");
            }

            activity = new Activity()
            {
                Project = new Project()
                {
                    Name = project.Project,
                    DateStart = project.DateStart,
                    DateEnd = project.DateEnd
                },
                ActivityType = project.ActivityType,
                Role = project.Role
                
            };
            _employeeContext.SaveChanges();

            return Ok();
        }

       

      


        
    }
}