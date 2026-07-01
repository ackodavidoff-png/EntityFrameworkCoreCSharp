using MusicHub.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(EntityValidation.AlbumNameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime ReleaseDate { get; set; }
        [ForeignKey(nameof(Producer))]
        public int? ProducerId { get; set; }
        public Producer? Producer { get; set; }
        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();
        [Column(TypeName = EntityValidation.DecimalPriceFormat)]
        public decimal Price => Songs.Sum(s => s.Price);
    }
}
