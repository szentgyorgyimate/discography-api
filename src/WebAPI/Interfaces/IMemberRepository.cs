using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface IMemberRepository
{
    List<Member> GetAll();
    Member GetById(int id);
    bool IsMemberExist(int id);
    int Add(string name);
    void Update(int id, string name);
    void Delete(int id);
}
