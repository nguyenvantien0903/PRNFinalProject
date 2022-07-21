using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PRNFinalProject.Models
{
    public partial class Person
    {
        public Person()
        {
            Rates = new HashSet<Rate>();
        }

        [Key]
        [Column("PersonID")]
        public int PersonId { get; set; }
        [StringLength(200)]
        public string Fullname { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public int? Type { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty(nameof(Rate.Person))]
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
