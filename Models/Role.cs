namespace FinaControl.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }

        public List<User?> Users { get; set;}= new();
    }    
}