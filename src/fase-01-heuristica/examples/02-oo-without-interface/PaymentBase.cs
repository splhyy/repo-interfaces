using System;

namespace Fase01Heuristica.Examples.OOWithoutInterface
{
    /// <summary>
    /// Classe base abstrata para pagamentos
    /// ✅ MELHORIA: Encapsula comportamento comum
    /// ❌ PROBLEMA: Serviço ainda conhece concretos
    /// </summary>
    public abstract class PaymentBase
    {
        public decimal Amount { get; protected set; }
        public string CustomerData { get; protected set; }
        
        protected PaymentBase(decimal amount, string customerData)
        {
            Amount = amount;
            CustomerData = customerData;
        }
        
        // Template Method Pattern
        public (bool Success, string Message) Process()
        {
            Console.WriteLine($"\n🔧 Processando {GetType().Name} de R$ {Amount:F2}");
            
            if (!Validate())
            {
                return (false, $"Validação falhou para {GetType().Name}");
            }
            
            var result = ExecutePayment();
            
            if (result)
            {
                return (true, $"{GetType().Name} processado com sucesso!");
            }
            
            return (false, $"Falha no processamento do {GetType().Name}");
        }
        
        // Métodos abstratos que as subclasses implementam
        protected abstract bool Validate();
        protected abstract bool ExecutePayment();
        
        // Método comum a todas as subclasses
        protected virtual void LogTransaction()
        {
            Console.WriteLine($"  📝 Transação registrada: {GetType().Name} - R$ {Amount:F2}");
        }
    }
}
