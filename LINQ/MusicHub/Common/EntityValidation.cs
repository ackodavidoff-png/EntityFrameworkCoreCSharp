using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Common
{
    public static class EntityValidation
    {
        //song class
        public const int SongNameMaxLength = 20;
        //album class
        public const int AlbumNameMaxLength = 40;
        //Performer
        public const int PerformerFirstNameMaxLength = 20;
        public const int PerformerLastNameMaxLength = 20;
        //producer
        public const int ProducerNameMaxLength = 30;
        //writer
        public const int WriterNameMaxLength = 20;
        //general
        public const string DecimalPriceFormat = "DECIMAL(12, 3)";
    }
}
