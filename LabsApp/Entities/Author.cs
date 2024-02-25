using System.Data.SqlTypes;
using System.Runtime.InteropServices.JavaScript;
using SQLite;

namespace LabsApp.Entities;

[Table("Authors")]
public class Author
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }	
    
    [Column("full_name")]				
    public string? FullName { get; set; }	

    [Column("birth_date")]			
    public DateTime BirthDate { get; set; }
    
    [Column("email")]
    public string? Email { get; set; }
}