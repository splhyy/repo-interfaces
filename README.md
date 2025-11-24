# Trabalho de Interfaces em C#

## Equipe: 
- Shara Palharini Lima

## Estrutura do Projeto:
````
repo-interfaces/
├── README.md
├── InterfacesSolution.sln
├── src/
│ ├── fase-01-procedural/
│ ├── fase-02-oo-sem-interface/
│ ├── fase-03-com-interfaces/
│ ├── fase-04-repository-inmemory/
│ ├── fase-05-repository-csv/
│ ├── fase-06-repository-json/
│ ├── fase-07-isp/
│ ├── fase-08-testes-dubles/
│ └── fase-09-cheiros-antidotos/
└── tests/
└── InterfacesTests/
````

## Sumário de Fases

- [Fase 1 - Procedural](#fase-1---procedural)
- [Fase 2 - OO sem Interface](#fase-2---oo-sem-interface)
- [Fase 3 - Com Interfaces](#fase-3---com-interfaces)
- [Fase 4 - Repository InMemory](#fase-4---repository-inmemory)
- [Fase 5 - Repository CSV](#fase-5---repository-csv)
- [Fase 6 - Repository JSON](#fase-6---repository-json)
- [Fase 7 - ISP na Prática](#fase-7---isp-na-prática)
- [Fase 8 - Testes com Dublês](#fase-8---testes-com-dublês)
- [Fase 9 - Cheiros e Antídotos](#fase-9---cheiros-e-antídotos)

## Como Executar

### Compilar toda a solução:
```bash
dotnet build
````
Executar testes:
````
dotnet test
````
Executar fase específica:
# Fase 1
```
cd src/fase-01-procedural && dotnet run && cd ../..
````
# Fase 2  
````
cd src/fase-02-oo-sem-interface && dotnet run && cd ../..
````
# Fase 3
````
cd src/fase-03-com-interfaces && dotnet run && cd ../..
````
# Fase 4
````
cd src/fase-04-repository-inmemory && dotnet run && cd ../..
````
# Fase 5
````
cd src/fase-05-repository-csv && dotnet run && cd ../..
````
# Fase 6
````
cd src/fase-06-repository-json && dotnet run && cd ../..
````
# Fase 7
````
cd src/fase-07-isp && dotnet run && cd ../..
````
# Fase 8
````
cd src/fase-08-testes-dubles && dotnet run && cd ../..
````
# Fase 9
````
cd src/fase-09-cheiros-antidotos && dotnet run && cd ../..
````
# # Decisões de Design por Fase:

# Fase 1 - Procedural
Contrato: Função FormatText com parâmetro mode

Implementações: Switch expression com casos para "upper", "lower", "title"

Problema identificado: Alto acoplamento, violação do OCP (aberto/fechado)

Sinal de alerta: Switch que cresce com cada novo formato

# Fase 2 - OO sem Interface
Contrato: Classe abstrata TextFormatter com método Apply

Implementações: UpperCaseFormatter, LowerCaseFormatter, TitleCaseFormatter

Melhoria: Eliminação do switch, uso de polimorfismo

Problema persistente: Acoplamento à hierarquia de classes concretas

# Fase 3 - Com Interfaces
Contrato: Interface ITextFormatter com método Apply

Implementações: Classes que implementam a interface

Melhoria: Baixo acoplamento, programação para contrato

Inovação: Catálogo por chave (Dictionary) para seleção dinâmica

Benefício: Testabilidade e extensibilidade

# Fase 4 - Repository InMemory
Contrato: Interface genérica IRepository<T>

Implementação: InMemoryRepository<T> com List<T>

Padrão: Repository Pattern para abstrair persistência

Benefício: Domínio isolado de detalhes de infraestrutura

# Fase 5 - Repository CSV
Contrato: Mesmo IRepository<T> da fase anterior

Implementação: CsvRepository<T> com persistência em arquivo

Demonstração: Cliente não muda ao alternar implementações

Validação: Princípio de substituição de Liskov

# Fase 6 - Repository JSON
Contrato: Mesmo IRepository<T> mantido

Implementação: JsonRepository<T> com serialização JSON

Evolução: Formato mais rico sem quebrar contrato

Ponto de composição: Troca centralizada sem afetar clientes

# Fase 7 - ISP na Prática
Problema: Interface "gorda" IRepositoryViolacaoISP<T> com muitas responsabilidades

Antídoto: Segregação em IReadRepository<T>, IWriteRepository<T>, IUnitOfWork

Benefício: Clientes dependem apenas do que usam

Aplicação: Princípio da Segregação de Interfaces (SOLID)

# Fase 8 - Testes com Dublês
Contratos: IEmailService, ILogger

Implementação real: SmtpEmailService para produção

Dublês: EmailServiceStub, LoggerMock para testes

Benefício: Testes rápidos, determinísticos e isolados

Padrão: Injeção de dependência para testabilidade

# Fase 9 - Cheiros e Antídotos
Cheiro 1: Switch por tipo - Antídoto: Polimorfismo com interfaces

Cheiro 2: Interface gorda - Antídoto: ISP e segregação

Cheiro 3: Dependência concreta - Antídoto: Inversão de dependência

Metodologia: Identificação e refatoração sistemática

# Checklist de Qualidade Aplicado
Contratos coesos: Interfaces focadas e específicas

Alternância sem alterar cliente: BookService funciona com todas implementações de IRepository

Testes sem I/O: InMemoryRepository e dublês para testes unitários

Ausência de switch/downcasts: Uso de polimorfismo e catálogos

Mudanças pequenas e localizadas: Cada fase em pasta separada

ISP aplicado: Interfaces segregadas por capacidade

Baixo acoplamento: Dependência de abstrações, não implementações

Testabilidade: Injeção de dependência e dublês

Evolução incremental: Mudanças focadas por fase

# Evidências de Qualidade

# 1. Compilação e Testes

# Todos os projetos compilam
````
dotnet build
````
# Testes executam sem I/O real
````
dotnet test
````
# 2. Alternância de Implementações
Mesmo código cliente funciona com:

InMemoryRepository<Book> (Fase 4)

CsvRepository<Book> (Fase 5)

JsonRepository<Book> (Fase 6)

Sem alterar uma linha do BookService

# 3. Testabilidade Comprovada
Testes unitários com InMemoryRepository

Dublês EmailServiceStub e LoggerMock

Isolamento completo de dependências externas

# 4. Princípios SOLID Aplicados
SRP: Classes com responsabilidade única

OCP: Extensível sem modificar código existente

LSP: Implementações substituíveis

ISP: Interfaces segregadas (Fase 7)

DIP: Dependência de abstrações

# Instruções de Execução Detalhadas
Pré-requisitos
.NET 6.0 ou superior

# Git (para controle de versão)

# Passo a passo:
Clone o repositório (se aplicável)

Navegue até a pasta do projeto:
````
cd repo-interfaces
````
Restaurar dependências:
````
dotnet restore
````
Compilar solução:
````
dotnet build
````
Executar testes:
````
dotnet test
````
# Executar fases individualmente (ver seção "Como Executar")

# # Comandos Úteis
# Ver todos os projetos na solução
````
dotnet sln list
````
# Executar fase específica
````
cd src/fase-03-com-interfaces && dotnet run
````
# Voltar para raiz
````
cd ../..
````
# # Conclusão

Este trabalho demonstra uma evolução completa desde abordagens procedural até arquiteturas modernas baseadas em interfaces, aplicando princípios SOLID, padrões de design e boas práticas de testabilidade. Cada fase introduz conceitos progressivamente, consolidando o aprendizado através de exemplos práticos e refatorações conscientes.
