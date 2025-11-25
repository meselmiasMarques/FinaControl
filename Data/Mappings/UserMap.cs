using FinaControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaControl.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(u => u.Name)
            .IsRequired(true)
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(u => u.Email)
            .IsRequired(true)
            .HasMaxLength(250) 
            .HasColumnType("varchar(250)");
        
    }
}