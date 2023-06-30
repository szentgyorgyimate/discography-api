using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Persistance;

namespace WebAPI.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly DiscographyDbContext _dbContext;

    public MemberRepository(DiscographyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Member> GetAll() =>
        _dbContext.Members.Include(m => m.Bands).AsNoTracking().ToList();
    

    public Member GetById(int id) =>
        _dbContext.Members.Include(m => m.Bands).AsNoTracking().FirstOrDefault(m => m.Id == id);

    public bool IsMemberExist(int id) =>
        _dbContext.Members.Any(m => m.Id == id);

    public int Add(string name)
    {
        var newMember = new Member() { Name = name };
        
        _dbContext.Members.Add(newMember);
        _dbContext.SaveChanges();

        return newMember.Id;
    }

    public void Update(int id, string name)
    {
        var memberToUpdate = new Member()
        {
            Id = id,
            Name = name
        };

        _dbContext.Members.Update(memberToUpdate);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var memberToDelete = new Member() { Id = id };

        _dbContext.Entry<Member>(memberToDelete).State = EntityState.Deleted;
        _dbContext.SaveChanges();
    }
}
