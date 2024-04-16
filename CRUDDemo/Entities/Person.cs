using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }
        [StringLength(50)]
        public string? PersonName { get; set; }
        [StringLength(50)]

        public string? Email { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string? Gender { get; set; }
        [ForeignKey("Country")]
        public Guid? CountryID { get; set; }
        public Country? Country { get; set; }
        [StringLength(50)]

        public string? Address { get; set; }
        [StringLength(200)]

        public bool ReceiveNewsLetters { get; set; }
    }
}
