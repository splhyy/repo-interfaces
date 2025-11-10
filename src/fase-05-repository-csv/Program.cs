using System;
using System.Collections.Generic;
using System.IO;
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
    
    public Book(string csvLine)
    {
        var parts = csvLine.Split(',');
        Title = parts[0];
        Author = parts[1];
    }
    
    public string ToCsv() => $"{Title},{Author}";
    
    public override string ToString() => $"{Title} by {Author}";
}

public sealed class CsvRepository<T> : IRepository<T>
{
    private readonly string _filePath;
    private readonly Func<T, string> _toCsv;
    private readonly Func<string, T> _fromCsv;
    
    public CsvRepository(string filePath, Func<T, string> toCsv, Func<string, T> fromCsv)
    {
        _filePath = filePath;
        _toCsv = toCsv;
        _fromCsv = fromCsv;
    }
    
    public void Add(T item)
    {
        File.AppendAllLines(_filePath, new[] { _toCsv(item) });
    }
    
    public IEnumerable<T> GetAll()
    {
        if (!File.Exists(_filePath))
            return Enumerable.Empty<T>();
            
        return File.ReadAllLines(_filePath).Select(_fromCsv);
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
        Console.WriteLine("=== FASE 5 - REPOSITORY CSV ===");
        
        string csvFile = "books.csv";
        
        // Limpar arquivo anterior se existir
        if (File.Exists(csvFile))
            File.Delete(csvFile);
        
        IRepository<Book> repository = new CsvRepository<Book>(
            csvFile,
            book => book.ToCsv(),
            line => new Book(line)
        );
        
        var bookService = new BookService(repository);
        
        // Adicionar livros
        bookService.AddBook("Domain-Driven Design", "Eric Evans");
        bookService.AddBook("Clean Code", "Robert Martin");
        
        Console.WriteLine("Livros salvos no CSV:");
        bookService.ListAllBooks();
        
        Console.WriteLine($"\nConteúdo do arquivo {csvFile}:");
        if (File.Exists(csvFile))
        {
            Console.WriteLine(File.ReadAllText(csvFile));
        }
    }
}