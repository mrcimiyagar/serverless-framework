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
    public class AuthController : ControllerBase
    {
        [HttpPost("~/api/auth/register")]
        public ActionResult<Packet> Register([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Email == packet.User.Email);

                if (user != null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 1
                    };
                }

                user = dbContext.Users.FirstOrDefault(u => u.Username == packet.User.Username);

                if (user != null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 2
                    };
                }

                var pending = new Pending()
                {
                    Email = packet.User.Email,
                    Code = Security.MakeKey8()
                };

                dbContext.Pendings.Add(pending);
                dbContext.SaveChanges();

                Support.SendEmail(packet.User.Email, "Verification", $"Your verification code is : {pending}");

                pending.Code = null;
                
                return new Packet()
                {
                    Status = ResponseStatus.Success,
                    Pending = pending
                };
            }
        }

        [HttpPost("~/api/auth/verify")]
        public ActionResult<Packet> Verify([FromBody] Packet packet)
        {
            using (var dbContext = new DatabaseContext())
            {
                var pending = dbContext.Pendings.Find(packet.Pending.PendingId);

                if (pending == null)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 1
                    };
                }

                if (pending.Code != packet.Pending.Code)
                {
                    return new Packet()
                    {
                        Status = ResponseStatus.Failure,
                        ErrorCode = 2
                    };
                }
                
                var user = new User()
                {
                    Email = packet.User.Email,
                    Password = packet.User.Password,
                    FirstName = packet.User.FirstName,
                    LastName = packet.User.LastName,
                    Username = packet.User.Username,
                    Sessions = new List<Session>()
                    {
                        new Session()
                        {
                            Token = Security.MakeKey64()
                        }
                    }
                };

                user.Sessions[0].User = user;

                dbContext.AddRange(user);
                dbContext.SaveChanges();
                
                return new Packet()
                {
                    Status = ResponseStatus.Success,
                    User = user
                };
            }
        }
    }
}