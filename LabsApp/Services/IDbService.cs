namespace LabsApp.Services;
using Entities;

public interface IDbService
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<IEnumerable<Book>> GetAuthorBooks(int id);
    Task Init();
}