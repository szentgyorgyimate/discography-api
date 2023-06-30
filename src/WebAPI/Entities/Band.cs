namespace WebAPI.Entities;

public class Band
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public int FormedIn { get; set; }
    public string CountryOfOrigin { get; set; }
    
    public List<Album> Albums { get; set; }
    public List<Member> Members { get; set; } = new List<Member>();
}
