using FinaControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaControl.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasIndex(u => u.Email,"IX_USER_EMAIL")
            .IsUnique();
        
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
        
        builder.Property(u => u.PasswordHash)
            .IsRequired(true)
            .HasMaxLength(250) 
            .HasColumnType("varchar(250)");
        
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");
        
        //Relacionamentos
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Restrict),
                
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                .OnDelete(DeleteBehavior.Restrict),

                userRole =>
                {
                    userRole.HasKey("RoleId", "UserId");
                    userRole.ToTable("UserRole");
                }
            );
    }
}