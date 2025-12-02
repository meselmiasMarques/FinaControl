namespace FinaControl.ViewModels.Reports;

public class TransactionsWithUser
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0.0M;
    public DateTime? CreatedAt { get; set; }
    public string Type { get; set; }= string.Empty;
    public string User { get; set; }= string.Empty;
    public string Category { get; set; }= string.Empty;
}