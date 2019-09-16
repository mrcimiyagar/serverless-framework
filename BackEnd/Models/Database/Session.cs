using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Database
{
    public class Session
    {
        [Key]
        public long SessionId { get; set; }
        public string Token { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}