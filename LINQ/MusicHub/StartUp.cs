namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            string resultForExportAlbumsInfo = ExportAlbumsInfo(context, 9);
            string resultForExportSongsAboveDuration = ExportAlbumsInfo(context, 4);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();
            var albums = context.Albums.Where(x => x.ProducerId == producerId).Select(x => new
            {
                AlbumName = x.Name,
                ReleaseDate = x.ReleaseDate,
                ProducerName = x.Producer.Name,
                AlbumPrice = x.Songs.Sum(s => s.Price),//x.Price,
                Songs = x.Songs.Select(y => new
                {
                    SongName = y.Name,
                    Price = y.Price,
                    WriterName = y.Writer.Name
                }).OrderByDescending(y => y.SongName).ThenBy(y => y.WriterName).ToList()
            }).OrderByDescending(x => x.AlbumPrice).ToList();
            foreach(var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");
                int count = 0;
                foreach(var song in album.Songs)
                {
                    count++;
                    sb.AppendLine($"---#{count}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.Price:F2}");
                    sb.AppendLine($"---Writer: {song.WriterName}");
                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();
            var songs = context.Songs.Where(x => x.Duration.TotalSeconds > duration).AsEnumerable().Select(x => new
            {
                x.Name,
                Writer = x.Writer.Name,
                Producer = x.Album.Producer.Name,
                x.Duration,
                Performers = x.SongPerformers.Select(y => $"{y.Performer.FirstName} {y.Performer.LastName}").OrderBy(y => y).ToList()
            }).OrderBy(x => x.Name).ThenBy(x => x.Writer).ToList();
            int counter = 0;
            foreach(var song in songs)
            {
                counter++;
                sb.AppendLine($"-Song #{counter}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.Writer}");
                foreach(var performer in song.Performers)
                {
                    sb.AppendLine($"---Performer: {performer}");
                }
                sb.AppendLine($"---AlbumProducer: {song.Producer}");
                sb.AppendLine($"---Duration: {song.Duration:c}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}