namespace EntityFrameworkCore.Domain
{
    public class League : BaseDomainModel
    {
        public string? Name { get; set; }

        // Add Query Filter Flag
        public bool IsDeleted { get; set; }

        // Adding reference to Teams Table 
        // League:Team --> 1:M relation
        // 1 League many teams
        public List<Team> Teams { get; set; } = new List<Team>(); // navigation property
    }

}

