using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PRNFinalProject.Models
{
    public partial class Rate
    {
        [Key]
        [Column("MovieID")]
        public int MovieId { get; set; }
        [Key]
        [Column("PersonID")]
        public int PersonId { get; set; }
        [Column(TypeName = "ntext")]
        public string Comment { get; set; }
        public double? NumericRating { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Time { get; set; }

        [ForeignKey(nameof(MovieId))]
        [InverseProperty("Rates")]
        public virtual Movie Movie { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Rates")]
        public virtual Person Person { get; set; }
    }
}
