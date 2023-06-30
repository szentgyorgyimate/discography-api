using WebAPI.Interfaces;
using WebAPI.Persistance;

namespace WebAPI.Repositories;

public class AlbumTypeRepository : IAlbumTypeRepository
{
    private readonly DiscographyDbContext _dbContext;

    public AlbumTypeRepository(DiscographyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsAlbumTypeExist(int id) =>
        _dbContext.AlbumTypes.Any(a => a.Id == id);
}
