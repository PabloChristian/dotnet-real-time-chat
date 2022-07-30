using Real.Time.Chat.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Real.Time.Chat.Infra.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(e => e.Password)
                .HasMaxLength(15)
                .IsRequired();
        }
    }
}
