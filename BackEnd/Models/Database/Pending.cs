using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Database
{
    public class Pending
    {
        [Key]
        public long PendingId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}