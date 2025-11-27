using FinaControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaControl.Data.Mappings;

public class TransactionMap :  IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasIndex(u => u.Type, "IX_Transaction_Type");

        builder.HasKey(c => c.Id)
            .HasName("PK_Transaction");

        builder.Property(c => c.Id)
            .UseIdentityColumn();

        builder.Property(c => c.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(c => c.Type)
            .IsRequired()
            .HasColumnType("INT");
        
        //Relacionamentos
        
        builder.HasOne(c => c.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(c => c.CategoryId)
            .HasConstraintName("FK_Transaction_Category")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Transactions)
            .HasConstraintName("FK_Transaction_User")
            .OnDelete(DeleteBehavior.Cascade);
        


    }
}