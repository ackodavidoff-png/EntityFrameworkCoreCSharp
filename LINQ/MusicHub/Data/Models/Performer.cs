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
    public class Performer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(EntityValidation.PerformerFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(EntityValidation.PerformerLastNameMaxLength)]
        public string LastName { get; set; } = null!;
        [Required]
        public int Age { get; set; }
        [Required]
        [Column(TypeName = EntityValidation.DecimalPriceFormat)]
        public decimal NetWorth { get; set; }
        public virtual ICollection<SongPerformer> PerformerSongs { get; set; } = new HashSet<SongPerformer>();
    }
}
