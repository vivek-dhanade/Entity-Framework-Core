
namespace EntityFrameworkCore.Domain
{
    public class Team : BaseDomainModel
    {
        // public int TeamId { get; set; }
        public string? Name { get; set; }

        // One-to-many reln
        // Adding reference to League Table Id
        // EF Core automatically understands the reference due to EF Core's convention based mapping
        // (to manually add reference, follow fluentAPI in Seeding (i.e. Configurations file) of the Team Table as given in next example of Matches )
        public League? League { get; set; } // navigation property
        public int? LeagueId { get; set; } // foreign key


        // Many-to-many reln 
        // (Home Team -- Away Team, with Joining Table as Match Table, )
        public List<Match> HomeMatches { get; set; }// navigation property
        public  List<Match> AwayMatches { get; set; } // navigation property


        //One-to-one reln
        public Coach Coach { get; set; } // navigation property
        public int CoachId { get; set; } // foreign key
        
    }
}








