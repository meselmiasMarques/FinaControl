namespace FinaControl.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }  = string.Empty;

        public long UserId { get; set; }
        public User? User { get; set; }
        
        //Relacionamentos
        public List<Transaction> Transactions { get; set; } = new();
    }
}