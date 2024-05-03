using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Backend.DataAccess;

namespace Backend.Configurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        #region Methods
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email)
                   .HasMaxLength(60)
                   .IsRequired();

            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.Property(x => x.Name)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Lastname)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(120);
        }
        #endregion
    }
}
