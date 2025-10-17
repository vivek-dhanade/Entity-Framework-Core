namespace EntityFrameworkCore.Domain
{
    public class Match : BaseDomainModel
    {
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime Date { get; set; }


        // M:N , Team:Team (HomeTeam:AwayTeam), relation wherein Match Table acts as joining table
        public  Team HomeTeam { get; set; }// navigation property
        public int HomeTeamId { get; set; } // foreign key

        public  Team AwayTeam { get; set; } // navigation property
        public int AwayTeamId { get; set; } // foreign key

    }
}
