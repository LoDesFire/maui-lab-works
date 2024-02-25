using SQLite;

namespace LabsApp.Entities;

[Table("Books")]
public class Book
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }	
    
    [Indexed]
    [Column("author_id")]
    public int AuthorId { get; set; }
    
    [Column("pages_count")]
    public int PagesCount { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
}