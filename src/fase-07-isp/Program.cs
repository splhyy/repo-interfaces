using System;
using System.Collections.Generic;
using System.Linq;

// ANTES: Interface que viola ISP
public interface IRepositoryViolacaoISP<T>
{
    void Add(T item);
    void Update(T item);
    void Delete(T item);
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Func<T, bool> predicate);
    int Count();
    bool Exists(Func<T, bool> predicate);
}

// DEPOIS: Interfaces segregadas por responsabilidade
public interface IReadRepository<T>
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Func<T, bool> predicate);
    int Count();
    bool Exists(Func<T, bool> predicate);
}

public interface IWriteRepository<T>
{
    void Add(T item);
    void Update(T item);
    void Delete(T item);
}

// Implementações
public class BookReadRepository : IReadRepository<Book>
{
    private readonly List<Book> _books = new();
    
    public BookReadRepository()
    {
        _books.Add(new Book(1, "Domain-Driven Design", "Eric Evans"));
        _books.Add(new Book(2, "Clean Code", "Robert Martin"));
        _books.Add(new Book(3, "Design Patterns", "Erich Gamma"));
    }
    
    public Book GetById(int id) => _books.First(b => b.Id == id);
    public IEnumerable<Book> GetAll() => _books;
    public IEnumerable<Book> Find(Func<Book, bool> predicate) => _books.Where(predicate);
    public int Count() => _books.Count;
    public bool Exists(Func<Book, bool> predicate) => _books.Any(predicate);
}

public class BookWriteRepository : IWriteRepository<Book>
{
    private readonly List<Book> _books = new();
    
    public void Add(Book item) => _books.Add(item);
    public void Update(Book item) 
    {
        var existing = _books.FirstOrDefault(b => b.Id == item.Id);
        if (existing != null)
        {
            _books.Remove(existing);
            _books.Add(item);
        }
    }
    public void Delete(Book item) => _books.Remove(item);
}

// Cliente que só precisa LER
public class RelatorioService
{
    private readonly IReadRepository<Book> _repository;
    
    public RelatorioService(IReadRepository<Book> repository)
    {
        _repository = repository;
    }
    
    public void GerarRelatorio()
    {
        Console.WriteLine($"=== RELATÓRIO DE LIVROS ===");
        Console.WriteLine($"Total de livros: {_repository.Count()}");
        Console.WriteLine("\nLista de livros:");
        foreach (var book in _repository.GetAll())
        {
            Console.WriteLine($" - {book}");
        }
    }
}

// Cliente que só precisa ESCREVER
public class ImportadorService
{
    private readonly IWriteRepository<Book> _repository;
    
    public ImportadorService(IWriteRepository<Book> repository)
    {
        _repository = repository;
    }
    
    public void ImportarLivro(string titulo, string autor)
    {
        var novoId = new Random().Next(100, 999);
        var livro = new Book(novoId, titulo, autor);
        _repository.Add(livro);
        Console.WriteLine($"✅ Livro '{titulo}' importado com sucesso! (ID: {novoId})");
    }
}

public class Book
{
    public int Id { get; }
    public string Title { get; }
    public string Author { get; }
    
    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }
    
    public override string ToString() => $"{Title} by {Author} (ID: {Id})";
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 7 - ISP (Interface Segregation Principle) ===");
        
        // Cliente de LEITURA - só depende do que usa
        var readRepo = new BookReadRepository();
        var relatorioService = new RelatorioService(readRepo);
        relatorioService.GerarRelatorio();
        
        Console.WriteLine("\n---");
        
        // Cliente de ESCRITA - só depende do que usa
        var writeRepo = new BookWriteRepository();
        var importadorService = new ImportadorService(writeRepo);
        importadorService.ImportarLivro("Novo Livro ISP", "Autor Demonstração");
        
        Console.WriteLine("\n🎯 BENEFÍCIOS DO ISP:");
        Console.WriteLine("- Clientes não forçados a depender de métodos não usados");
        Console.WriteLine("- Interfaces coesas e específicas");
        Console.WriteLine("- Maior flexibilidade e manutenibilidade");
        Console.WriteLine("- Testabilidade aprimorada");
    }
}
