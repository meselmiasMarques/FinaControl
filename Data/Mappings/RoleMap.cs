using FinaControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaControl.Data.Mappings;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        
        builder.HasIndex(u => u.Name,"IX_Role_Name")
            .IsUnique();

        builder.HasKey(c => c.Id)
            .HasName("PK_Role");

        builder.Property(c => c.Id)
            .UseIdentityColumn();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar");

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");


    }
}

