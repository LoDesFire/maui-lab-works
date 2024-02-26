using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using LabsApp.Entities;
using LabsApp.Services;

namespace LabsApp.Pages;

public partial class SqLiteDemoViewModel
{
    private readonly IDbService _dbService;

    public SqLiteDemoViewModel(IDbService dbService)
    {
        _dbService = dbService;
        Authors = new ObservableCollection<Author>();
        Books = new ObservableCollection<Book>();
    }

    public ObservableCollection<Author> Authors { get; }
    public ObservableCollection<Book> Books { get; }
    

    [RelayCommand]
    private async Task LoadPicker()
    {
        var authors = await _dbService.GetAllAuthors();
        Authors.Clear();
        foreach (var author in authors)
        {
            Authors.Add(author);
        }
    }
    
    [RelayCommand]
    private async Task AuthorsBooks(object? selectedItem)
    {
        if (selectedItem is Author author)
        {
            var id = author.Id;
            var books = await _dbService.GetAuthorBooks(id);
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
    }
}