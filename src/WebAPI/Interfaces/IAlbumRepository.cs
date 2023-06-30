using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface IAlbumRepository
{
    List<Album> GetAll();
    List<Album> GetAll(int? bandId = null, int? typeId = null, DateTime? releaseFrom = null, DateTime? releaseTo = null);
    Album GetById(int id);
    bool IsExists(int id);
    int Add(int bandId, int typeId, string name, DateTime releaseDate);
    void Update(int id, int bandId, int typeId, string name, DateTime releaseDate);
    void Delete(int id);
}
