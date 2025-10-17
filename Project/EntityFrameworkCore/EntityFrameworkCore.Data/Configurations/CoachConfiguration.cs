using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configurations
{
    internal class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasData(
                new Coach
                {
                    Id = 8,
                    Name = "Eight"
                },
                new Coach
                {
                    Id = 9,
                    Name = "Nine"
                },
                new Coach
                {
                    Id = 10,
                    Name = "Ten"
                }
            );
        }
    }
}