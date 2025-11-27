using FinaControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaControl.Data.Mappings;

public class CategoryMap :  IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        
        builder.HasIndex(u => u.Name,"IX_Category_Name")
            .IsUnique();

        builder.HasKey(c => c.Id)
            .HasName("PK_Category");

        builder.Property(c => c.Id)
            .UseIdentityColumn();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar");
    }
}