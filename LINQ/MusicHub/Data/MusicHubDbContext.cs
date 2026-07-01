namespace MusicHub.Data
{
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }
        public virtual DbSet<Album> Albums { get; set; } = null!;
        public virtual DbSet<Performer> Performers { get; set; } = null!;
        public virtual DbSet<Producer> Producers { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<SongPerformer> SongsPerformers { get; set; } = null!;
        public virtual DbSet<Writer> Writers { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SongPerformer>().HasKey(x => new { x.SongId, x.PerformerId});
        }
    }
}
