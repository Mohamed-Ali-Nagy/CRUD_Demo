using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }
        [StringLength(50)]
        public string? CountryName { get; set; }
        public virtual ICollection<Country>? Countries { get; set; }=new List<Country>();
    }
}
