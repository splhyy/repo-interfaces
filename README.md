# Trabalho de Interfaces em C#

## Equipe: 
- Shara Palharini Lima

## Estrutura do Projeto:

repo-interfaces/
├── README.md
├── InterfacesSolution.sln
├── src/
│ ├── fase-01-procedural/
│ ├── fase-02-oo-sem-interface/
│ ├── fase-03-com-interfaces/
│ ├── fase-04-repository-inmemory/
│ ├── fase-05-repository-csv/
│ └── fase-06-repository-json/
└── tests/
└── InterfacesTests/


## Como Executar

### Compilar toda a solução:
```bash
dotnet build

Executar testes:
dotnet test

Executar fase específica:

# Fase 1
cd src/fase-01-procedural && dotnet run && cd ../..

# Fase 2  
cd src/fase-02-oo-sem-interface && dotnet run && cd ../..

# Fase 3
cd src/fase-03-com-interfaces && dotnet run && cd ../..

# Fase 4
cd src/fase-04-repository-inmemory && dotnet run && cd ../..

# Fase 5
cd src/fase-05-repository-csv && dotnet run && cd ../..

# Fase 6
cd src/fase-06-repository-json && dotnet run && cd ../..

Decisões de Design por Fase
Fase 1 - Procedural
Uso de switch expression para demonstrar acoplamento alto

Identificação clara dos "cheiros" de código (switch espalhado)

Fácil de entender mas difícil de manter e estender

Fase 2 - OO sem Interface
Classes abstratas para polimorfismo

Classes sealed para otimização e evitar herança desnecessária

Eliminação do switch, mas ainda acoplado à hierarquia de classes

Fase 3 - Com Interfaces
Contrato ITextFormatter para baixo acoplamento

Injeção por parâmetro para testabilidade

Catálogo por chave (Dictionary) para eliminar switches

Cliente depende apenas do contrato, não da implementação

Fase 4 - Repository InMemory
Contrato IRepository<T> único para todas implementações

Implementação em memória para testes rápidos

Serviço (BookService) não conhece detalhes de persistência

Fase 5 - Repository CSV
Mesmo contrato IRepository<T> da Fase 4

Implementação com persistência em arquivo CSV

Cliente (BookService) não muda ao alternar implementações

Fase 6 - Repository JSON
Mesmo contrato IRepository<T> das fases anteriores

Implementação com persistência JSON mais robusta

Demonstra evolução sem quebrar clientes existentes

Checklist de Qualidade Aplicado
Contratos coesos (ITextFormatter, IRepository<T>)

Alternância sem alterar cliente (BookService funciona com todas implementações)

Testes sem I/O (InMemoryRepository para testes unitários)

Ausência de switch/downcasts (usando polimorfismo)

Mudanças pequenas e localizadas (cada fase em pasta separada)

Evidências de Testes
Todos os projetos compilam e executam corretamente:
dotnet build
dotnet test

Cada fase pode ser executada individualmente conforme mostrado acima.

