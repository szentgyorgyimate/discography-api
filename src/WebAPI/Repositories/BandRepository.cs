using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Persistance;

namespace WebAPI.Repositories;

public class BandRepository : IBandRepository
{
    private readonly DiscographyDbContext _dbContext;

    public BandRepository(DiscographyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Band> GetAll() =>
        _dbContext.Bands.Include(b => b.Members).AsNoTracking().ToList();

    public Band GetById(int id) =>
        _dbContext.Bands.Include(b => b.Members).AsNoTracking().FirstOrDefault(b => b.Id == id);

    public bool IsBandExist(int id) =>
        _dbContext.Bands.Any(b => b.Id == id);

    public bool IsBandHasMember(int id, int memberId)
    {
        var band = _dbContext.Bands.Include(b => b.Members).AsNoTracking().FirstOrDefault(b => b.Id == id);

        return band.Members.Any(m => m.Id == memberId);
    }

    public int Add(string name, string genre, string countryOfOrigin, int formedIn)
    {
        var newBand = new Band()
        {
            Name = name,
            Genre = genre,
            CountryOfOrigin = countryOfOrigin,
            FormedIn = formedIn
        };

        _dbContext.Bands.Add(newBand);
        _dbContext.SaveChanges();

        return newBand.Id;
    }

    public void AddMemberToBand(int id, int memberId)
    {
        var band = _dbContext.Bands.Find(id);
        var memberToAdd = _dbContext.Members.Find(memberId);

        band.Members.Add(memberToAdd);
        
        _dbContext.SaveChanges();
    }

    public void Update(int id, string name, string genre, string countryOfOrigin, int formedIn)
    {
        var bandToUpdate = new Band()
        {
            Id = id,
            Name = name,
            Genre = genre,
            CountryOfOrigin = countryOfOrigin,
            FormedIn = formedIn
        };

        _dbContext.Bands.Update(bandToUpdate);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var bandToDelete = new Band() { Id = id };

        _dbContext.Entry<Band>(bandToDelete).State = EntityState.Deleted;
        _dbContext.SaveChanges();
    }

    public void DeleteMemberFromBand(int id, int memberId)
    {
        var band = _dbContext.Bands.Include(b => b.Members).FirstOrDefault(b => b.Id == id);
        band.Members.Remove(band.Members.First(m => m.Id == memberId));

        _dbContext.SaveChanges();
    }
}
