using System.Collections.Generic;
using BackEnd.Models.Database;

namespace BackEnd.Models
{
    public enum ResponseStatus
    {
        Success,
        Failure
    }
    
    public class Packet
    {
        public ResponseStatus Status { get; set; }
        public int ErrorCode { get; set; }
        public Pending Pending { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
        public List<Project> Projects { get; set; }
    }
}