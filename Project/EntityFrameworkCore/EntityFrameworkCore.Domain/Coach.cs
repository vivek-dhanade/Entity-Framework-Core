using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Domain
{
    public class Coach : BaseDomainModel
    {
        // [MaxLength(100)]
        // [Required]
        // public int Id { get; set; }
        public string Name { get; set; } = "";


        // public int? TeamId { get; set; }

        public Team? Team { get; set; } // navigation property

    }

}


























