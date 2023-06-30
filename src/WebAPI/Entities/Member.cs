namespace WebAPI.Entities;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<Band> Bands { get; set; } = new List<Band>();
}
