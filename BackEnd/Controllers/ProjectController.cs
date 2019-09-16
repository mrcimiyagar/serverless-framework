using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BackEnd.DbContexts;
using BackEnd.Models;
using BackEnd.Models.Database;
using BackEnd.Tools;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpPost("~/api/project/create")]
        public ActionResult<Packet> Create([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                if (!Security.AuthenticateUser(Request.Headers[Security.Authorization], dbContext, out var session))
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 100
                    };
                
                dbContext.Entry(session).Reference(s => s.User).Load();
                var user = session.User;
                dbContext.Entry(user).Collection(u => u.Projects).Load();
                var project = user.Projects.FirstOrDefault(p => p.Name == packet.Project.Name);
                if (project != null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 1
                    };
                }

                project = new Project()
                {
                    Name = packet.Project.Name,
                    Status = 0,
                    DateCreated = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                    Creator = user
                };

                dbContext.Projects.Add(project);
                dbContext.SaveChanges();

                return new Packet()
                {
                    Status = ResponseStatus.Success,
                    Project = project
                };

            }
        }

        [HttpPost("~/api/project/read")]
        public ActionResult<Packet> Verify([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                if (!Security.AuthenticateUser(Request.Headers[Security.Authorization], dbContext, out var session))
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 100
                    };
                
                dbContext.Entry(session).Reference(s => s.User).Load();
                var user = session.User;
                dbContext.Entry(user).Collection(u => u.Projects).Load();
                var project = user.Projects.Find(p => p.ProjectId == packet.Project.ProjectId);

                return new Packet()
                {
                    Status = ResponseStatus.Success,
                    Project = project
                };
            }
        }
        
        [HttpPost("~/api/project/read_all")]
        public ActionResult<Packet> ReadAll([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                if (!Security.AuthenticateUser(Request.Headers[Security.Authorization], dbContext, out var session))
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 100
                    };
                
                dbContext.Entry(session).Reference(s => s.User).Load();
                var user = session.User;
                dbContext.Entry(user).Collection(u => u.Projects).Load();
                var projects = user.Projects.ToList();

                return new Packet()
                {
                    Status = ResponseStatus.Success,
                    Projects = projects
                };
            }
        }
        
        [HttpPost("~/api/project/update")]
        public ActionResult<Packet> Update([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                if (!Security.AuthenticateUser(Request.Headers[Security.Authorization], dbContext, out var session))
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 100
                    };
                
                dbContext.Entry(session).Reference(s => s.User).Load();
                var user = session.User;
                dbContext.Entry(user).Collection(u => u.Projects).Load();
                var similarName = user.Projects.FirstOrDefault(p => p.Name == packet.Project.Name);
                if (similarName != null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 1
                    };
                }
                var project = user.Projects.Find(p => p.ProjectId == packet.Project.ProjectId);
                if (project == null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 2
                    };
                }
                project.Name = packet.Project.Name;
                dbContext.SaveChanges();

                return new Packet()
                {
                    Status = ResponseStatus.Success
                };
            }
        }
        
        [HttpPost("~/api/project/delete")]
        public ActionResult<Packet> Delete([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                if (!Security.AuthenticateUser(Request.Headers[Security.Authorization], dbContext, out var session))
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 100
                    };
                
                dbContext.Entry(session).Reference(s => s.User).Load();
                var user = session.User;
                dbContext.Entry(user).Collection(u => u.Projects).Load();
                var project = user.Projects.Find(p => p.ProjectId == packet.Project.ProjectId);
                if (project == null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 1
                    };
                }

                dbContext.Projects.Remove(project);

                return new Packet()
                {
                    Status = ResponseStatus.Success,
                    Project = project
                };
            }
        }
    }
}