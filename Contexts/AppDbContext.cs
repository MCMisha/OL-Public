using Microsoft.EntityFrameworkCore;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Contexts;

public partial class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Performance> Performances { get; set; }
    public virtual DbSet<Implementer> Implementers { get; set; }
    public virtual DbSet<PerformanceImplementer> PerformanceImplementers { get; set; }
    public virtual DbSet<Place> Places { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<News> News { get; set; }
    public virtual DbSet<Artist> Artists { get; set; }
    public virtual DbSet<Section> Sections { get; set; }
    public virtual DbSet<TicketPriceGroup> TicketPriceGroups { get; set; }
    public virtual DbSet<TicketPrice> TicketPrices { get; set; }
    public virtual DbSet<EventArtist> EventArtists { get; set; }
    public virtual DbSet<PerformanceEvent> PerformanceEvents { get; set; }
    public virtual DbSet<PublicComment> PublicComments { get; set; }
    public virtual DbSet<MainPageBackground> MainPageBackgrounds { get; set; }
    public virtual DbSet<ArtistPhoto> ArtistPhotos { get; set; }
    public virtual DbSet<PerformancePhoto> PerformancePhotos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("AppDB"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genre_pk");

            entity.ToTable("genre");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("perfomances_id_pk");

            entity.ToTable("performance");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.BreaksCount)
                .HasDefaultValue(0)
                .HasColumnName("breaks_count");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.MainImage).HasColumnName("main_image");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Poster).HasColumnName("poster");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.PremiereDate).HasColumnName("premiere_date");

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Performances)
                .HasForeignKey(d => d.Genre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("perfomance_genre_id_fk");

            entity.HasOne(d => d.PlaceNavigation).WithMany(p => p.Performances)
                .HasForeignKey(d => d.Place)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("perfomance_place_id_fk");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("place_id_pk");

            entity.ToTable("place");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });
        modelBuilder.HasSequence<int>("performance_id_seq");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_id_pk");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .HasColumnName("password_hash");
            
            modelBuilder.Entity<User>()
                .Property(u => u.RefreshTokenHash)
                .HasMaxLength(64)
                .HasColumnName("refresh_token_hash");

            modelBuilder.Entity<User>()
                .Property(u => u.RefreshTokenExpiresAt)
                .HasColumnName("refresh_token_expires_at");
        });
        modelBuilder.HasSequence<int>("user_id_seq");

        modelBuilder.Entity<PerformanceImplementer>()
            .HasKey(pr => pr.Id);

        modelBuilder.Entity<PerformanceImplementer>()
            .HasOne(pr => pr.Performance)
            .WithMany(p => p.PerformanceImplementers)
            .HasForeignKey(pr => pr.PerformanceId);

        modelBuilder.Entity<PerformanceImplementer>()
            .HasOne(pr => pr.Implementer)
            .WithMany(r => r.PerformanceImplementers)
            .HasForeignKey(pr => pr.ImplementerId);

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("news_id_pk");

            entity.ToTable("news");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.Property(e => e.Subtitle)
                .HasMaxLength(100)
                .HasColumnName("subtitle");

            entity.Property(e => e.MainImage).HasColumnName("main_image");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");

            entity.Property(e => e.Content)
                .HasMaxLength(100000)
                .HasColumnName("content");
        });

        modelBuilder.HasSequence<int>("news_id_seq");

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("artist_id_pk");

            entity.ToTable("artist");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");

            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .HasColumnName("last_name");

            entity.Property(e => e.Photo).HasColumnName("photo");

            entity.Property(e => e.Description)
                .HasMaxLength(6000)
                .HasColumnName("description");
        });

        modelBuilder.HasSequence<int>("artist_id_seq");

        modelBuilder.Entity<Section>(b =>
        {
            b.HasKey(x => x.Id).HasName("section_id_pk");
            b.ToTable("section");
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(200);
            b.Property(x => x.Order).IsRequired();
            b.HasIndex(x => x.Slug).IsUnique();
            b.Property(x => x.Type).HasColumnName("type").IsRequired();
        });

        modelBuilder.HasSequence<int>("section_id_seq");

        modelBuilder.Entity<TicketPriceGroup>()
            .ToTable("ticket_price_groups")
            .HasMany(g => g.Prices)
            .WithOne(p => p.Group)
            .HasForeignKey(p => p.TicketPriceGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TicketPriceGroup>()
            .HasIndex(g => new { g.PerformanceId, g.SortOrder });

        modelBuilder.HasSequence<int>("ticket_price_group_id_seq");

        modelBuilder.Entity<TicketPrice>()
            .ToTable("ticket_prices")
            .HasIndex(p => new { p.TicketPriceGroupId, p.Type })
            .IsUnique();

        modelBuilder.Entity<TicketPrice>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(10,2)");

        modelBuilder.HasSequence<int>("ticket_price_id_seq");

        modelBuilder.Entity<EventArtist>(entity =>
        {
            entity.ToTable("event_artist");

            entity.Property(ea => ea.Role)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(ea => ea.PerformanceEventNavigation)
                .WithMany(e => e.EventArtists)
                .HasForeignKey(ea => ea.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ea => ea.Artist)
                .WithMany(a => a.EventArtists)
                .HasForeignKey(ea => ea.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.HasSequence<int>("event_artist_id_seq");
        
        modelBuilder.Entity<PerformanceEvent>(e =>
        {
            e.ToTable("performance_event");
            e.HasKey(x => x.Id);

            e.HasIndex(x => new { x.PerformanceId, x.StartAt }).IsUnique();
            e.HasIndex(x => x.StartAt);
            e.Property(x => x.BuyLink).HasMaxLength(500);

            e.HasOne(x => x.Performance)
                .WithMany(x => x.PerformanceEvents)
                .HasForeignKey(x => x.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.HasSequence<int>("performance_event_id_seq");

        modelBuilder.Entity<PublicComment>(e =>
        {
            e.ToTable("public_comment");

            e.HasKey(x => x.Id);

            e.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(x => x.Comment)
                .IsRequired()
                .HasMaxLength(1000);

            e.Property(x => x.Stars)
                .IsRequired();

            e.Property(x => x.DatePublished)
                .IsRequired();

            e.Property(x => x.Photo)
                .HasColumnType("bytea");

            e.HasOne(pc => pc.Performance)
                .WithMany(p => p.PublicComments)
                .HasForeignKey(pc => pc.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.HasSequence<int>("public_comment_id_seq");

        modelBuilder.Entity<MainPageBackground>(entity =>
        {
            entity.ToTable("main_page_background");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.PerformanceId)
                .IsRequired();

            entity.Property(e => e.MainImage)
                .IsRequired();

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0);

            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.DisplayOrder);
            entity.HasOne(e => e.PerformanceNavigation)
                .WithOne(performance => performance.MainPageBackground)
                .HasForeignKey<MainPageBackground>(e => e.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.HasSequence<int>("main_page_background_id_seq");

        modelBuilder.Entity<ArtistPhoto>(entity =>
        {
            entity.ToTable("artist_photo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.ArtistNavigation)
                .WithMany(a => a.Photos)
                .HasForeignKey(aph => aph.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PerformancePhoto>(entity =>
        {
            entity.ToTable("performance_photo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.PerformanceNavigation)
                .WithMany(p => p.PerformancePhotos)
                .HasForeignKey(pph => pph.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
