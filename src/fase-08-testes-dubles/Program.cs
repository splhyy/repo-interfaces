using System;
using System.Collections.Generic;

// CONTRATOS
public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public interface ILogger
{
    void Log(string message);
}

public interface IUserRepository
{
    void AddUser(string email, string username);
    bool UserExists(string email);
}

// IMPLEMENTAÇÃO REAL (produção)
public class SmtpEmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        // Código real para enviar email via SMTP
        Console.WriteLine($"📧 Enviando email para: {to}");
        Console.WriteLine($"Assunto: {subject}");
        Console.WriteLine($"Corpo: {body}");
        Console.WriteLine("✅ Email enviado via SMTP!");
    }
}

public class DatabaseUserRepository : IUserRepository
{
    private readonly List<string> _users = new();
    
    public void AddUser(string email, string username)
    {
        _users.Add(email);
        Console.WriteLine($"💾 Usuário '{username}' salvo no banco de dados");
    }
    
    public bool UserExists(string email)
    {
        return _users.Contains(email);
    }
}

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"📝 [LOG] {DateTime.Now:HH:mm:ss}: {message}");
    }
}

// DUBLÊS DE TESTE (não fazem operações reais)
public class EmailServiceStub : IEmailService
{
    public string LastTo { get; private set; } = string.Empty;
    public string LastSubject { get; private set; } = string.Empty;
    public string LastBody { get; private set; } = string.Empty;
    public int SendCount { get; private set; }
    
    public void SendEmail(string to, string subject, string body)
    {
        // Apenas armazena os dados - NÃO envia email real
        LastTo = to;
        LastSubject = subject;
        LastBody = body;
        SendCount++;
        Console.WriteLine($"🎭 STUB: Email simulado para {to}");
    }
}

public class LoggerMock : ILogger
{
    public List<string> LogMessages { get; } = new List<string>();
    
    public void Log(string message)
    {
        LogMessages.Add(message);
        Console.WriteLine($"🎭 MOCK: Log simulado: {message}");
    }
    
    public bool WasCalledWith(string message) => LogMessages.Contains(message);
    public int CallCount => LogMessages.Count;
}

public class UserRepositoryFake : IUserRepository
{
    private readonly HashSet<string> _users = new();
    
    public void AddUser(string email, string username)
    {
        _users.Add(email);
        Console.WriteLine($"🎭 FAKE: Usuário '{username}' adicionado na memória");
    }
    
    public bool UserExists(string email) => _users.Contains(email);
}

// SERVIÇO QUE USA AS DEPENDÊNCIAS
public class UserRegistrationService
{
    private readonly IEmailService _emailService;
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;
    
    public UserRegistrationService(IEmailService emailService, ILogger logger, IUserRepository userRepository)
    {
        _emailService = emailService;
        _logger = logger;
        _userRepository = userRepository;
    }
    
    public bool RegisterUser(string email, string username)
    {
        _logger.Log($"Iniciando registro do usuário: {username}");
        
        if (_userRepository.UserExists(email))
        {
            _logger.Log($"Usuário {email} já existe");
            return false;
        }
        
        // Lógica de negócio
        _userRepository.AddUser(email, username);
        _emailService.SendEmail(email, "Bem-vindo!", $"Olá {username}, bem-vindo ao sistema!");
        _logger.Log($"Usuário {username} registrado com sucesso");
        
        return true;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 8 - TESTES COM DUBLÊS ===");
        
        Console.WriteLine("\n1. 🎭 CENÁRIO DE TESTE (com dublês):");
        var emailStub = new EmailServiceStub();
        var loggerMock = new LoggerMock();
        var userRepoFake = new UserRepositoryFake();
        var userServiceTest = new UserRegistrationService(emailStub, loggerMock, userRepoFake);
        
        // Executar teste
        var resultado = userServiceTest.RegisterUser("teste@email.com", "Usuário Teste");
        
        Console.WriteLine($"\n2. ✅ VERIFICAÇÕES DE TESTE:");
        Console.WriteLine($"Registro bem-sucedido: {resultado}");
        Console.WriteLine($"Email enviado para: {emailStub.LastTo}");
        Console.WriteLine($"Assunto do email: {emailStub.LastSubject}");
        Console.WriteLine($"Quantidade de emails enviados: {emailStub.SendCount}");
        Console.WriteLine($"Quantidade de logs: {loggerMock.CallCount}");
        Console.WriteLine($"Usuário existe no repositório: {userRepoFake.UserExists("teste@email.com")}");
        
        Console.WriteLine($"\n3. 🔄 TESTANDO USUÁRIO DUPLICADO:");
        resultado = userServiceTest.RegisterUser("teste@email.com", "Usuário Duplicado");
        Console.WriteLine($"Registro duplicado bloqueado: {!resultado}");
        
        Console.WriteLine($"\n🎯 BENEFÍCIOS DOS DUBLÊS:");
        Console.WriteLine("- ✅ Testes rápidos (sem I/O real)");
        Console.WriteLine("- ✅ Testes determinísticos");
        Console.WriteLine("- ✅ Isolamento completo");
        Console.WriteLine("- ✅ Verificação de comportamento");
        Console.WriteLine("- ✅ Não dependem de recursos externos");
    }
}
