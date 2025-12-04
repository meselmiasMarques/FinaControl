using System.Text.Json.Serialization;

namespace FinaControl.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        [JsonIgnore]
        public string PasswordHash { get; set; }  = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<Role> Roles { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}