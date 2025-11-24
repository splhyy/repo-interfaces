using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 9 - CHEIROS E ANTÍDOTOS ===");
        Console.WriteLine("Identificação de problemas comuns e suas soluções\n");
        
        DemonstrarCheiro1_SwitchPorTipo();
        DemonstrarCheiro2_InterfaceGorda();
        DemonstrarCheiro3_DependenciaConcreta();
        
        Console.WriteLine("\n🎯 CONCLUSÃO: Padrões de design resolvem cheiros comuns!");
    }
    
    static void DemonstrarCheiro1_SwitchPorTipo()
    {
        Console.WriteLine("🔍 CHEIRO 1: Switch por Tipo");
        Console.WriteLine("----------------------------------------");
        
        Console.WriteLine("ANTES (Procedural com switch):");
        Console.WriteLine("""
        string Processar(string tipo, string dados)
        {
            return tipo switch
            {
                "csv" => ProcessarCsv(dados),
                "json" => ProcessarJson(dados),
                "xml" => ProcessarXml(dados),
                _ => throw new ArgumentException("Tipo não suportado")
            };
        }
        """);
        
        Console.WriteLine("PROBLEMAS:");
        Console.WriteLine("- ❌ Viola OCP (aberto/fechado)");
        Console.WriteLine("- ❌ Acoplamento alto");
        Console.WriteLine("- ❌ Dificuldade de extensão");
        Console.WriteLine("- ❌ Testabilidade reduzida");
        
        Console.WriteLine("\nDEPOIS (Antídoto - Polimorfismo):");
        Console.WriteLine("""
        interface IProcessador 
        {
            string Processar(string dados);
        }
        
        class ProcessadorCsv : IProcessador { ... }
        class ProcessadorJson : IProcessador { ... }
        class ProcessadorXml : IProcessador { ... }
        
        // Uso com catálogo:
        var processadores = new Dictionary<string, IProcessador>
        {
            ["csv"] = new ProcessadorCsv(),
            ["json"] = new ProcessadorJson()
        };
        
        string Processar(string tipo, string dados) => processadores[tipo].Processar(dados);
        """);
        
        Console.WriteLine("BENEFÍCIOS:");
        Console.WriteLine("- ✅ Fácil adicionar novos processadores");
        Console.WriteLine("- ✅ Baixo acoplamento");
        Console.WriteLine("- ✅ Testabilidade com dublês");
        Console.WriteLine("- ✅ Respeita OCP");
    }
    
    static void DemonstrarCheiro2_InterfaceGorda()
    {
        Console.WriteLine("\n🔍 CHEIRO 2: Interface Gorda");
        Console.WriteLine("----------------------------------------");
        
        Console.WriteLine("ANTES (Interface com muitas responsabilidades):");
        Console.WriteLine("""
        interface IRepositorioGordo<T>
        {
            // Operações CRUD
            void Add(T item);
            void Update(T item);
            void Delete(T item);
            T GetById(int id);
            
            // Consultas
            IEnumerable<T> GetAll();
            IEnumerable<T> Find(Func<T,bool> predicate);
            IEnumerable<T> FindPaged(int page, int size);
            
            // Estatísticas
            int Count();
            bool Exists(Func<T,bool> predicate);
            
            // Transações
            void BeginTransaction();
            void Commit();
            void Rollback();
            
            // ... mais 10 métodos
        }
        """);
        
        Console.WriteLine("PROBLEMAS:");
        Console.WriteLine("- ❌ Viola ISP (segregação de interfaces)");
        Console.WriteLine("- ❌ Clientes forçados a implementar métodos não usados");
        Console.WriteLine("- ❌ Dificuldade de mock em testes");
        Console.WriteLine("- ❌ Alta complexidade");
        
        Console.WriteLine("\nDEPOIS (Antídoto - ISP):");
        Console.WriteLine("""
        // Interfaces segregadas por responsabilidade
        interface IReadRepository<T> 
        {
            T GetById(int id);
            IEnumerable<T> GetAll();
            IEnumerable<T> Find(Func<T,bool> predicate);
            IEnumerable<T> FindPaged(int page, int size);
        }
        
        interface IWriteRepository<T>
        {
            void Add(T item);
            void Update(T item);
            void Delete(T item);
        }
        
        interface IQueryRepository<T>
        {
            int Count();
            bool Exists(Func<T,bool> predicate);
        }
        
        interface IUnitOfWork
        {
            void BeginTransaction();
            void Commit();
            void Rollback();
        }
        """);
        
        Console.WriteLine("BENEFÍCIOS:");
        Console.WriteLine("- ✅ Cada interface tem uma responsabilidade");
        Console.WriteLine("- ✅ Clientes dependem apenas do que usam");
        Console.WriteLine("- ✅ Facilidade de teste com mocks específicos");
        Console.WriteLine("- ✅ Manutenibilidade melhorada");
    }
    
    static void DemonstrarCheiro3_DependenciaConcreta()
    {
        Console.WriteLine("\n🔍 CHEIRO 3: Dependência Concreta");
        Console.WriteLine("----------------------------------------");
        
        Console.WriteLine("ANTES (Acoplamento direto):");
        Console.WriteLine("""
        class RelatorioService
        {
            private readonly SqlDatabase _database;
            
            public RelatorioService()
            {
                _database = new SqlDatabase(); // Acoplamento concreto
            }
            
            public void GerarRelatorio()
            {
                var dados = _database.Query("SELECT * FROM relatorios");
                // processamento...
            }
        }
        """);
        
        Console.WriteLine("PROBLEMAS:");
        Console.WriteLine("- ❌ Dificuldade de teste (depende de banco real)");
        Console.WriteLine("- ❌ Impossível alternar implementações");
        Console.WriteLine("- ❌ Viola DIP (inversão de dependência)");
        Console.WriteLine("- ❌ Testes lentos e frágeis");
        
        Console.WriteLine("\nDEPOIS (Antídoto - Inversão de Dependência):");
        Console.WriteLine("""
        interface IDatabase
        {
            IEnumerable<object> Query(string sql);
            void Execute(string sql);
        }
        
        class RelatorioService
        {
            private readonly IDatabase _database;
            
            public RelatorioService(IDatabase database) // Injeção de dependência
            {
                _database = database;
            }
            
            public void GerarRelatorio()
            {
                var dados = _database.Query("SELECT * FROM relatorios");
                // processamento...
            }
        }
        
        // Implementações:
        class SqlDatabase : IDatabase { ... }
        class InMemoryDatabase : IDatabase { ... } // para testes
        
        // Teste fácil:
        var databaseMock = new DatabaseMock();
        var service = new RelatorioService(databaseMock); // Fácil de testar!
        """);
        
        Console.WriteLine("BENEFÍCIOS:");
        Console.WriteLine("- ✅ Testabilidade com dublês");
        Console.WriteLine("- ✅ Flexibilidade para alternar implementações");
        Console.WriteLine("- ✅ Respeita DIP (dependa de abstrações)");
        Console.WriteLine("- ✅ Código mais modular e mantenível");
    }
}
