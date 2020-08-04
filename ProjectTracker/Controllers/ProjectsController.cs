using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        //Todo Add method to get by id
        // [HttpGet]
        // public IActionResult Index()
        // {
        //     return Json(_employeeContext.Projects.ToArray());
        // }
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
        [HttpGet("{employeeId:int}")]
        public IActionResult AllProjects(int employeeId)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }
            
            return Json(_employeeContext.Activities.Where( a=> a.Employee == emp ));
        
        }
        [HttpGet("{employeeId:int}/{activityId:int}")]
        public IActionResult AllProjects(int employeeId, int activityId)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
            {
                return NotFound("No user");
            }
            
            return Json(_employeeContext.Activities.Where( a=> a.Employee == emp && a.Id == activityId ));
        
        }

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
        [HttpPost("{employeeId:int}")]
        public IActionResult AddActivity(int employeeId, [FromBody] ProjectPost project)
        {
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (emp == null)
            {
                return NotFound();
            }

            emp.Activities ??= new List<Activity>();
            
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
        [HttpPut("{employeeId:int}/{activityId:int}")]
        public IActionResult AddActivity(int employeeId, int activityId, [FromBody] ProjectPost project)
        {
            // ToDo FIX IT !!!!
            var emp = _employeeContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (emp == null)
            {
                return NotFound();
            }

            var activity = _employeeContext.Activities.FirstOrDefault(a => a.Id == activityId);

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