using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public interface IRepository<T>
{
    void Add(T item);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Func<T, bool> predicate);
}

public class Book
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    
    public Book() { }
    
    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
    
    public override string ToString() => $"{Title} by {Author}";
}

public sealed class JsonRepository<T> : IRepository<T>
{
    private readonly string _filePath;
    
    public JsonRepository(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Add(T item)
    {
        var items = GetAll().ToList();
        items.Add(item);
        
        var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
    
    public IEnumerable<T> GetAll()
    {
        if (!File.Exists(_filePath))
            return Enumerable.Empty<T>();
            
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
    
    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return GetAll().Where(predicate);
    }
}

public class BookService
{
    private readonly IRepository<Book> _repository;
    
    public BookService(IRepository<Book> repository)
    {
        _repository = repository;
    }
    
    public void AddBook(string title, string author)
    {
        _repository.Add(new Book(title, author));
    }
    
    public void ListAllBooks()
    {
        var books = _repository.GetAll();
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 6 - REPOSITORY JSON ===");
        
        string jsonFile = "books.json";
        
        if (File.Exists(jsonFile))
            File.Delete(jsonFile);
        
        IRepository<Book> repository = new JsonRepository<Book>(jsonFile);
        var bookService = new BookService(repository);
        
        bookService.AddBook("Domain-Driven Design", "Eric Evans");
        bookService.AddBook("Clean Code", "Robert Martin");
        bookService.AddBook("Design Patterns", "Erich Gamma");
        
        Console.WriteLine("Livros salvos no JSON:");
        bookService.ListAllBooks();
        
        Console.WriteLine($"\nConteúdo do arquivo {jsonFile}:");
        if (File.Exists(jsonFile))
        {
            Console.WriteLine(File.ReadAllText(jsonFile));
        }
    }
}
