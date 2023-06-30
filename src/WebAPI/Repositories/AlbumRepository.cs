using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Persistance;

namespace WebAPI.Repositories;

public class AlbumRepository : IAlbumRepository
{
    private readonly DiscographyDbContext _dbContext;

    public AlbumRepository(DiscographyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Album> GetAll() =>
        _dbContext.Albums.Include(a => a.AlbumType).Include(a => a.Band).ToList();

    public List<Album> GetAll(int? bandId = null, int? typeId = null, DateTime? releaseFrom = null, DateTime? releaseTo = null)
    {
        var albumQuery = _dbContext.Albums.Include(a => a.AlbumType).Include(a => a.Band).AsQueryable();

        if (bandId.HasValue)
        {
            albumQuery = albumQuery.Where(a => a.BandId == bandId.Value);
        }

        if (typeId.HasValue)
        {
            albumQuery = albumQuery.Where(a => a.AlbumTypeId == typeId.Value);
        }

        if (releaseFrom.HasValue)
        {
            albumQuery = albumQuery.Where(a => a.ReleaseDate >= releaseFrom.Value.Date);
        }

        if (releaseTo.HasValue)
        {
            albumQuery = albumQuery.Where(a => a.ReleaseDate <= releaseTo.Value.Date);
        }

        return albumQuery.AsNoTracking().ToList();
    }


    public Album GetById(int id) =>
        _dbContext.Albums.Include(a => a.AlbumType).Include(a => a.Band).AsNoTracking().FirstOrDefault(a => a.Id == id);

    public bool IsExists(int id) =>
        _dbContext.Albums.Any(a => a.Id == id);

    public int Add(int bandId, int typeId, string name, DateTime releaseDate)
    {
        var newAlbum = new Album()
        {
            AlbumTypeId = typeId,
            BandId = bandId,
            Name = name,
            ReleaseDate = releaseDate
        };

        _dbContext.Albums.Add(newAlbum);
        _dbContext.SaveChanges();

        return newAlbum.Id;
    }

    public void Update(int id, int bandId, int typeId, string name, DateTime releaseDate)
    {
        var albumToUpdate = new Album()
        {
            Id = id,
            BandId = bandId,
            AlbumTypeId = typeId,
            Name = name,
            ReleaseDate = releaseDate
        };

        _dbContext.Update(albumToUpdate);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var albumToDelete = new Album() { Id = id };
        _dbContext.Entry<Album>(albumToDelete).State = EntityState.Deleted; 
        
        _dbContext.SaveChanges();
    }
}
