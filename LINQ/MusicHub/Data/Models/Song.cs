using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MusicHub.Data.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(EntityValidation.SongNameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [ForeignKey(nameof(Album))]
        public int? AlbumId { get; set; }
        public Album? Album { get; set; }
        [Required]
        [ForeignKey(nameof(Writer))]
        public int WriterId { get; set; }
        [Required]
        public Writer Writer { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual ICollection<SongPerformer> SongPerformers { get; set; } = new HashSet<SongPerformer>();
    }
}
