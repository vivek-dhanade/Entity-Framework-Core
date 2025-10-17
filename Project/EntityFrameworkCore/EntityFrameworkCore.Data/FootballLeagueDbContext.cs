using Microsoft.EntityFrameworkCore;

using EntityFrameworkCore.Domain;

using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkCore.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        // public FootballLeagueDbContext()
        // {
        //     var folder = Environment.SpecialFolder.LocalApplicationData;
        //     var path = Environment.GetFolderPath(folder);
        //     DbPath = Path.Combine(path, "FootballLeague_EfCore.db");
        // }

        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options) : base(options)
        {

        }
        public DbSet<Team> Team { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<League> League { get; set; }
        public DbSet<Match> Match { get; set; }

        public DbSet<TeamsAndLeaguesView> TeamsAndLeaguesView { get; set; }

        public string? DbPath { get; private set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     // // Option 1: Connecting to SQL Server Database
        //     // // optionsBuilder.UseSqlServer("DataSource=localhost\\SQLEXPRESS; InitialCatalog = FootballLeague_EfCore; Encrypt=false");

        //     // // Option 2: Connecting to SqLite Database
        //     // optionsBuilder.UseSqlite($"Data Source = {DbPath}")
        //     // // .UseLazyLoadingProxies()
        //     // //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // used to block tracking of user queries -- improves performance
        //     // .LogTo(Console.WriteLine, LogLevel.Information)
        //     // .EnableSensitiveDataLogging() //dont enable for production
        //     // .EnableDetailedErrors(); //dont enable for production 
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                // modelBuilder.Entity<Team>().HasData(
                //     new Team
                //     {
                //         Id = 1,
                //         Name = "Tivoli Gardens FC",
                //         CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //     },
                //     new Team
                //     {
                //         Id = 2,
                //         Name = "Waterhouse FC",
                //         CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //     },
                //     new Team
                //     {
                //         Id = 3,
                //         Name = "Humble Lions FC",
                //         CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                //     }
                // );
            }
            //OR

            // modelBuilder.ApplyConfiguration(new TeamConfiguration());
            // modelBuilder.ApplyConfiguration(new LeagueConfiguration());

            //OR

            // Applies migrations from all IEntityTypeConfiguration<TEntity> instances that are provided in given assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());




            // attaching View to Entity
            modelBuilder.Entity<TeamsAndLeaguesView>().HasNoKey().ToView("vw_TeamsAndLeagues");


            // User-defined functions
            // attaching user defined function "GetEarliestMatch" present in database to DbContext function "GetEarliestTeamMatch"
            // modelBuilder.HasDbFunction(typeof(FootballLeagueDbContext).GetMethod(nameof(GetEarliestTeamMatch), new[] { typeof(int) })).HasName("GetEarliestMatch");

        }


        /// Overriding Save Changes 
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseDomainModel>().Where(m => m.State == EntityState.Added || m.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.ModifiedBy = "Sample User 1";
                entry.Entity.ModifiedDate = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Sample User";
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                entry.Entity.Version = Guid.NewGuid();
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        // public DateTime GetEarliestTeamMatch(int teamId) => throw new NotImplementedException();

        /// Adding Configure Convention i.e. Data Validation globally for certain data types
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(100);
            configurationBuilder.Properties<decimal>().HavePrecision(16, 2);
        }

    }

    public class FootballLeagueDbContextFactory : IDesignTimeDbContextFactory<FootballLeagueDbContext>
    {
        public FootballLeagueDbContext CreateDbContext(string[] args)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbPath = Path.Combine(path, configuration.GetConnectionString("SqliteDatabaseConnectionString"));

            var connectionString = $"Data Source={dbPath}";

            var optionsBuilder = new DbContextOptionsBuilder<FootballLeagueDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new FootballLeagueDbContext(optionsBuilder.Options);
        }
    }


}