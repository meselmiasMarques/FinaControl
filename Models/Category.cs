namespace FinaControl.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }  = string.Empty;
        
        //Relacionamentos
        public List<Transaction> Transactions { get; set; } = new();
    }
}