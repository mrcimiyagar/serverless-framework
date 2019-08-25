namespace X1BackendProject.Models
{
    public class DatabaseConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public Databases DatabaseProvider { get; set; }
    }
}