using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PRNFinalProject.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Rates = new HashSet<Rate>();
        }

        [Key]
        [Column("MovieID")]
        public int MovieId { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        public int? Year { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Column("GenreID")]
        public int? GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        [InverseProperty("Movies")]
        public virtual Genre Genre { get; set; }
        [InverseProperty(nameof(Rate.Movie))]
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
