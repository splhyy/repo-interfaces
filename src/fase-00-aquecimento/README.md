# Fase 0 - Aquecimento Conceitual: Contratos de Capacidade

## 📋 Objetivo da Fase
Treinar o olhar de design identificando objetivos fixos e peças alternáveis em situações reais.

## 🎯 Metodologia
Para cada caso, identificamos:
- **Objetivo estável**: O que se quer alcançar
- **Contrato**: Capacidade prometida (sem revelar "como")
- **Implementações**: Peças alternáveis que cumprem o mesmo contrato
- **Política**: Regra clara para escolher entre implementações
- **Riscos**: Limitações ou observações de cada implementação

## 💼 Caso 1: Entrega de Relatório

### Objetivo
Entregar relatório processado ao destinatário de forma confiável e eficiente

### Contrato
`entregar documento processado`

### Implementações
- **A → E-mail com anexo PDF**: Entrega via protocolo SMTP
- **B → Upload para nuvem**: Armazenamento em cloud com link compartilhado

### Política de Escolha
`documentos < 10MB → E-mail; documentos ≥ 10MB → Nuvem`

### Riscos e Observações
- **E-mail**: Limite de tamanho de anexo, dependência de servidor SMTP
- **Nuvem**: Requer conexão internet, questões de privacidade em clouds públicas

## 💳 Caso 2: Processamento de Pagamento

### Objetivo
Realizar transação financeira de forma segura e confiável

### Contrato
`processar transação financeira`

### Implementações
- **A → Cartão de crédito**: Autorização em tempo real via operadora
- **B → Boleto bancário**: Pagamento offline com vencimento

### Política de Escolha
`compras até R$ 500 → Cartão; compras acima de R$ 500 → Boleto`

### Riscos e Observações
- **Cartão**: Pode ser negado, taxa de intermediação, chargeback
- **Boleto**: Prazo de vencimento, risco de não pagamento, processamento manual

## 🎓 Aprendizados de Design

### Princípios Aplicados
- **Baixo Acoplamento**: Contratos desacoplados de implementações
- **Alta Coesão**: Cada implementação foca em uma estratégia específica
- **Extensibilidade**: Novas implementações podem ser adicionadas sem modificar contratos

### Padrões Identificados
- **Strategy Pattern**: Diferentes algoritmos para o mesmo objetivo
- **Factory Pattern**: Política atua como fábrica para seleção de implementações

## 📁 Estrutura dos Artefatos
```
fase-00-aquecimento/
├── README.md # Este arquivo
├── fase-00-conceitual.md # Documento original da entrega
└── fase-00-aquecimento.csproj # Projeto .NET
````
## 🚀 Como Visualizar
Esta fase é conceitual e não possui código executável. Os artefatos podem ser visualizados diretamente no GitHub:

- `README.md` - Documentação completa da fase
- `fase-00-conceitual.md` - Casos concretos com contratos e políticas

## 🎯 Próximos Passos
Esta fase conceitual prepara o terreno para a implementação prática nas fases seguintes, onde esses conceitos serão materializados em código.

