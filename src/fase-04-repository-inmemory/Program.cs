using System;
using System.Collections.Generic;
using System.Linq;

public interface IRepository<T>
{
    void Add(T item);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Func<T, bool> predicate);
}

public class Book
{
    public string Title { get; }
    public string Author { get; }
    
    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
    
    public override string ToString() => $"{Title} by {Author}";
}

public sealed class InMemoryRepository<T> : IRepository<T>
{
    private readonly List<T> _items = new List<T>();
    
    public void Add(T item) => _items.Add(item);
    
    public IEnumerable<T> GetAll() => _items;
    
    public IEnumerable<T> Find(Func<T, bool> predicate) => _items.Where(predicate);
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
    
    public void FindBooksByAuthor(string author)
    {
        var books = _repository.Find(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
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
        Console.WriteLine("=== FASE 4 - REPOSITORY INMEMORY ===");
        
        IRepository<Book> repository = new InMemoryRepository<Book>();
        var bookService = new BookService(repository);
        
        // Adicionar alguns livros
        bookService.AddBook("Domain-Driven Design", "Eric Evans");
        bookService.AddBook("Clean Code", "Robert Martin");
        bookService.AddBook("Design Patterns", "Erich Gamma");
        
        Console.WriteLine("\nTodos os livros:");
        bookService.ListAllBooks();
        
        Console.WriteLine("\nLivros do Eric Evans:");
        bookService.FindBooksByAuthor("Eric Evans");
    }
}