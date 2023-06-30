using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Persistance;

public class DiscographySeeder
{
    private readonly DiscographyDbContext _dbContext;

    public DiscographySeeder(DiscographyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        _dbContext.Database.EnsureCreated();

        // Clear all data
        _dbContext.Database.ExecuteSql($"DELETE FROM [Albums]");
        _dbContext.Database.ExecuteSql($"DELETE FROM [AlbumTypes]");
        _dbContext.Database.ExecuteSql($"DELETE FROM [Bands]");
        _dbContext.Database.ExecuteSql($"DELETE FROM [Members]");

        // Album types
        var albumType1 = new AlbumType() { Id = 1, Name = "Demo" };
        var albumType2 = new AlbumType() { Id = 2, Name = "Single" };
        var albumType3 = new AlbumType() { Id = 3, Name = "EP" };
        var albumType4 = new AlbumType() { Id = 4, Name = "Full-length" };

        var albumTypes = new List<AlbumType>() { albumType1, albumType2, albumType3, albumType4 };

        _dbContext.AlbumTypes.AddRange(albumTypes);
        _dbContext.SaveChanges();

        // Bands
        var band1 = new Band() { Id = 1, Name = "The Algoritmics", Genre = "Pop", FormedIn = 2020, CountryOfOrigin = "Canada" };
        var band2 = new Band() { Id = 2, Name = "Augmented Falseness", Genre = "Progressive metal", FormedIn = 2019, CountryOfOrigin = "United Kingdom" };
        var band3 = new Band() { Id = 3, Name = "The Khaki Case", Genre = "Jazz", FormedIn = 2015, CountryOfOrigin = "USA" };

        _dbContext.Bands.AddRange(new List<Band>() { band1, band2, band3 });
        _dbContext.SaveChanges();

        // Members
        var member1 = new Member() { Id = 1, Name = "Robert (fast fingers) Bell", Bands = new List<Band>() { band1, band3 } };
        var member2 = new Member() { Id = 2, Name = "Gavin (the throat) Page", Bands = new List<Band>() { band1, band3 } };
        var member3 = new Member() { Id = 3, Name = "Harry (guitar god) Miller", Bands = new List<Band>() { band1, band2 } };
        var member4 = new Member() { Id = 4, Name = "Ellen (funky) Trivett", Bands = new List<Band>() { band2 } };
        var member5 = new Member() { Id = 5, Name = "Molly (jazzy) Padilla", Bands = new List<Band>() { band2 } };
        var member6 = new Member() { Id = 6, Name = "Ward (the guard) Lucas", Bands = new List<Band>() { band2, band3 } };
        var member7 = new Member() { Id = 7, Name = "Marc (mars) Ramsey", Bands = new List<Band>() { band1, band2 } };
        var member8 = new Member() { Id = 8, Name = "Joy (delight) Little", Bands = new List<Band>() { band3 } };
        var member9 = new Member() { Id = 9, Name = "Elena (brighty) Abbott", Bands = new List<Band>() { band3 } };
        var member10 = new Member() { Id = 10, Name = "Judy (praised) Moreno", Bands = new List<Band>() { band3 } };

        _dbContext.Members.AddRange(new List<Member>() { member1, member2, member3, member4, member5, member6, member7, member8, member9, member10 });
        _dbContext.SaveChanges();

        // Albums
        var album1 = new Album() { Id = 1, AlbumTypeId = 1, BandId = 1, Name = "Exploring binaries", ReleaseDate = new DateTime(2020, 3, 12) };
        var album2 = new Album() { Id = 2, AlbumTypeId = 3, BandId = 1, Name = "Operator madness", ReleaseDate = new DateTime(2021, 5, 23) };
        var album3 = new Album() { Id = 3, AlbumTypeId = 4, BandId = 1, Name = "A false statement", ReleaseDate = new DateTime(2022, 9, 27) };
        var album4 = new Album() { Id = 4, AlbumTypeId = 2, BandId = 2, Name = "Tutorials from scratch", ReleaseDate = new DateTime(2020, 1, 8) };
        var album5 = new Album() { Id = 5, AlbumTypeId = 2, BandId = 2, Name = "Terminal termination", ReleaseDate = new DateTime(2020, 2, 8) };
        var album6 = new Album() { Id = 6, AlbumTypeId = 4, BandId = 2, Name = "Forgotten variables", ReleaseDate = new DateTime(2020, 4, 8) };
        var album7 = new Album() { Id = 7, AlbumTypeId = 1, BandId = 3, Name = "A camel named Bill", ReleaseDate = new DateTime(2015, 2, 10) };
        var album8 = new Album() { Id = 8, AlbumTypeId = 1, BandId = 3, Name = "Midnight at 7th street", ReleaseDate = new DateTime(2015, 4, 1) };
        var album9 = new Album() { Id = 9, AlbumTypeId = 2, BandId = 3, Name = "Incrementing the blues", ReleaseDate = new DateTime(2016, 10, 27) };
        var album10 = new Album() { Id = 10, AlbumTypeId = 2, BandId = 3, Name = "Beige lessons", ReleaseDate = new DateTime(2017, 11, 25) };

        _dbContext.Albums.AddRange(album1, album2, album3, album4, album5, album6, album7, album8, album9, album10);
        _dbContext.SaveChanges();
    }
}
