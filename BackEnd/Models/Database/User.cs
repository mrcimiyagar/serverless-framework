using System.Collections.Generic;

namespace BackEnd.Models.Database
{
    public class User
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Project> Projects { get; set; }
    }
}