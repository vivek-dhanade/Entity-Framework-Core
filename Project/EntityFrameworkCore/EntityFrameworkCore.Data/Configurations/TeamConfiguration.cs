using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configurations
{
    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {

            // Key Config
            // builder.HasIndex(q => q.Name).IsUnique();

            // Composite Key Config
            // builder.HasIndex(q => new { q.CoachId, q.LeagueId }).IsUnique();    

            // Data Validation using FluentAPI
            builder.Property(q => q.Name).HasMaxLength(100).IsRequired();

            

            // Example of how 1:M relation of League:Team could be defined manually

            builder.HasOne(t => t.League)          // <-- Each Team has ONE League (navigation property)
                .WithMany(l => l.Teams)            // <-- Each League has MANY Teams (collection navigation)
                .HasForeignKey(t => t.LeagueId);   // <-- The FK is Team.LeagueId



            // Manually add reference for M:N, Many to Many Relationship between Team: Team (HomeTeam:AwayTeam), where Match Table acts as Joining Table

            //For HomeTeam
            builder.HasMany(team => team.HomeMatches)           // Each AwayTeam has MANY HomeMatches
                .WithOne(match => match.HomeTeam)               // Each Match has ONE HomeTeam
                .HasForeignKey(match => match.HomeTeamId)       // The FK is Match.HomeTeamId
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //For AwayTeam
            builder.HasMany(team => team.AwayMatches)           // Each HomeTeam has MANY AwayMatches
                .WithOne(match => match.AwayTeam)               // Each Match has ONE AwayTeam
                .HasForeignKey(match => match.AwayTeamId)       // The FK is Match.AwayTeamId
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Team
                {
                    Id = 1,
                    Name = "Tivoli Gardens FC",
                    CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CoachId = 8
                },
                new Team
                {
                    Id = 2,
                    Name = "Waterhouse FC",
                    CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CoachId = 9
                },
                new Team
                {
                    Id = 3,
                    Name = "Humble Lions FC",
                    CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CoachId = 10
                }
            );
        }
    }
}