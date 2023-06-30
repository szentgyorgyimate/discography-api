using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Persistance;

public class DiscographyDbContext : DbContext
{
    public DiscographyDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumType> AlbumTypes { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<Member> Members { get; set; }

    // This code is not necessary. It's here only for demonstration purposes.
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Band>()
    //        .HasMany(b => b.Members)
    //        .WithMany(m => m.Bands) 
    //        .UsingEntity(          
    //            "BandMember",
    //            b => b.HasOne(typeof(Band)).WithMany().HasForeignKey("BandsId").HasPrincipalKey(nameof(Band.Id)),
    //            m => m.HasOne(typeof(Member)).WithMany().HasForeignKey("MembersId").HasPrincipalKey(nameof(Member.Id)),
    //            bm => bm.HasKey("BandsId", "MembersId"));
        
    //    base.OnModelCreating(modelBuilder);
    //}
}
