using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Database
{
    public class Project
    {
        [Key]
        public long ProjectId { get; set; }
        public string Name { get; set; }       
        public short Status { get; set; }
        public long DateCreated { get; set; }
        public long ZipFileSize { get; set; }
        public long CreatorId { get; set; }
        public User Creator { get; set; }
    }
}