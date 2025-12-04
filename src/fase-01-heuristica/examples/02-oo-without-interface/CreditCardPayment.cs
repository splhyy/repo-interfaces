using System;
using System.Threading;

namespace Fase01Heuristica.Examples.OOWithoutInterface
{
    /// <summary>
    /// Implementação específica para Cartão de Crédito
    /// </summary>
    public class CreditCardPayment : PaymentBase
    {
        private const decimal FeePercentage = 0.03m;
        
        public CreditCardPayment(decimal amount, string cardNumber) 
            : base(amount, cardNumber) { }
        
        protected override bool Validate()
        {
            Console.WriteLine("  🔍 Validando cartão...");
            
            if (string.IsNullOrEmpty(CustomerData) || CustomerData.Length != 16)
            {
                Console.WriteLine("  ❌ Número do cartão inválido");
                return false;
            }
            
            Console.WriteLine("  ✅ Cartão válido");
            return true;
        }
        
        protected override bool ExecutePayment()
        {
            Console.WriteLine("  🔄 Processando na operadora...");
            
            // Lógica específica do cartão
            var fee = Amount * FeePercentage;
            Console.WriteLine($"  💰 Taxa: R$ {fee:F2}");
            
            // Simulação
            Thread.Sleep(800);
            
            LogTransaction();
            return true;
        }
        
        // ⚠️ PROBLEMA: Método específico que o cliente pode querer acessar
        public bool CanUseInstallments()
        {
            return Amount > 100;
        }
    }
}
