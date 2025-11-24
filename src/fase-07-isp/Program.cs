using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

public interface IRepository<T, TId>
{
    T Add(T entity);
    T? GetById(TId id);
    IReadOnlyList<T> ListAll();
    bool Update(T entity);
    bool Remove(TId id);
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int? Year { get; set; }

    public Book() { }

    public Book(int id, string title, string author, int? year = null)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString() => $"#{Id}: {Title} by {Author}" + (Year.HasValue ? $" ({Year})" : "");
}

public sealed class JsonBookRepository : IRepository<Book, int>
{
    private readonly string _path;
    
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    public JsonBookRepository(string path) => _path = path;

    public Book Add(Book entity)
    {
        var list = Load();
        list.RemoveAll(b => b.Id == entity.Id);
        list.Add(entity);
        Save(list);
        return entity;
    }

    public Book? GetById(int id) => Load().FirstOrDefault(b => b.Id == id);

    public IReadOnlyList<Book> ListAll() => Load();

    public bool Update(Book entity)
    {
        var list = Load();
        var index = list.FindIndex(b => b.Id == entity.Id);
        if (index < 0) return false;
        
        list[index] = entity;
        Save(list);
        return true;
    }

    public bool Remove(int id)
    {
        var list = Load();
        var removed = list.RemoveAll(b => b.Id == id) > 0;
        if (removed) Save(list);
        return removed;
    }

    private List<Book> Load()
    {
        if (!File.Exists(_path)) return new List<Book>();
        
        var json = File.ReadAllText(_path);
        if (string.IsNullOrWhiteSpace(json)) return new List<Book>();
        
        return JsonSerializer.Deserialize<List<Book>>(json, _options) ?? new List<Book>();
    }

    private void Save(List<Book> list)
    {
        var json = JsonSerializer.Serialize(list, _options);
        File.WriteAllText(_path, json);
    }
}

// Serviço de domínio (regras de negócio)
public class LibraryService
{
    private readonly IRepository<Book, int> _repository;

    public LibraryService(IRepository<Book, int> repository)
    {
        _repository = repository;
    }

    public void AddBook(string title, string author, int? year = null)
    {
        // Regra de negócio: ID automático
        var books = _repository.ListAll();
        var newId = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
        
        var book = new Book(newId, title, author, year);
        _repository.Add(book);
        Console.WriteLine($"✅ Livro adicionado: {book}");
    }

    public void ListAllBooks()
    {
        var books = _repository.ListAll();
        Console.WriteLine($"\n📚 Biblioteca ({books.Count} livros):");
        foreach (var book in books)
        {
            Console.WriteLine($"   {book}");
        }
    }

    public void FindBook(int id)
    {
        var book = _repository.GetById(id);
        if (book != null)
            Console.WriteLine($"🔍 Encontrado: {book}");
        else
            Console.WriteLine($"❌ Livro #{id} não encontrado");
    }

    public void UpdateBookTitle(int id, string newTitle)
    {
        var book = _repository.GetById(id);
        if (book != null)
        {
            book.Title = newTitle;
            if (_repository.Update(book))
                Console.WriteLine($"✏️ Livro atualizado: {book}");
            else
                Console.WriteLine($"❌ Falha ao atualizar livro #{id}");
        }
        else
        {
            Console.WriteLine($"❌ Livro #{id} não encontrado para atualização");
        }
    }

    public void RemoveBook(int id)
    {
        if (_repository.Remove(id))
            Console.WriteLine($"🗑️ Livro #{id} removido com sucesso");
        else
            Console.WriteLine($"❌ Livro #{id} não encontrado para remoção");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 7 — REPOSITORY JSON (System.Text.Json) ===");
        
        string jsonFile = "library.json";
        
        // Limpar arquivo anterior para demonstração
        if (File.Exists(jsonFile))
            File.Delete(jsonFile);
        
        IRepository<Book, int> repository = new JsonBookRepository(jsonFile);
        var libraryService = new LibraryService(repository);
        
        // Demonstração das operações
        Console.WriteLine("\n1. 📝 ADICIONANDO LIVROS:");
        libraryService.AddBook("Domain-Driven Design", "Eric Evans", 2003);
        libraryService.AddBook("Clean Code", "Robert Martin", 2008);
        libraryService.AddBook("Design Patterns", "Erich Gamma", 1994);
        
        Console.WriteLine("\n2. 📚 LISTANDO TODOS OS LIVROS:");
        libraryService.ListAllBooks();
        
        Console.WriteLine("\n3. 🔍 BUSCANDO LIVRO POR ID:");
        libraryService.FindBook(2);
        libraryService.FindBook(99); // Não existe
        
        Console.WriteLine("\n4. ✏️ ATUALIZANDO LIVRO:");
        libraryService.UpdateBookTitle(1, "Domain-Driven Design - Edição Revisada");
        
        Console.WriteLine("\n5. 🗑️ REMOVENDO LIVRO:");
        libraryService.RemoveBook(3);
        
        Console.WriteLine("\n6. 📊 ESTADO FINAL:");
        libraryService.ListAllBooks();
        
        Console.WriteLine($"\n7. 💾 CONTEÚDO DO ARQUIVO JSON:");
        if (File.Exists(jsonFile))
        {
            Console.WriteLine(File.ReadAllText(jsonFile));
        }
        
        Console.WriteLine("\n🎯 DECISÕES DE DESIGN:");
        Console.WriteLine("- ✅ System.Text.Json com opções configuradas");
        Console.WriteLine("- ✅ CamelCase para compatibilidade JavaScript");
        Console.WriteLine("- ✅ Ignora valores null na serialização");
        Console.WriteLine("- ✅ Formatação indentada para legibilidade");
        Console.WriteLine("- ✅ Tratamento de arquivo ausente/vazio");
        Console.WriteLine("- ✅ Repository cuida apenas de acesso a dados");
        Console.WriteLine("- ✅ Regras de negócio no Service");
    }
}
