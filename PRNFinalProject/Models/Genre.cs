using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PRNFinalProject.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        [Key]
        [Column("GenreID")]
        public int GenreId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        [InverseProperty(nameof(Movie.Genre))]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
