using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface IBandRepository
{
    List<Band> GetAll();
    Band GetById(int id);
    bool IsBandExist(int id);
    bool IsBandHasMember(int id, int memberId);
    int Add(string name, string genre, string countryOfOrigin, int formedIn);
    void AddMemberToBand(int id, int memberId);
    void Update(int id, string name, string genre, string countryOfOrigin, int formedIn);
    void Delete(int id);
    void DeleteMemberFromBand(int id, int memberId);
}
