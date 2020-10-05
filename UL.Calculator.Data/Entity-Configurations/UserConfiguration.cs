using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UL.Calculator.Entities;

namespace UL.Calculator.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            //builder.Property(x => x.Id).ValueGeneratedOnAdd();
            //builder.HasAlternateKey(x => x.Email); Left for reference

            builder.HasOne(x => x.LoginInfo)
                        .WithOne(y => y.User)
                        .HasForeignKey<UserLogin>(fk => fk.Username)
                        .HasPrincipalKey<User>(pk => pk.Email);
        }
    }
}