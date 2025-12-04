using System;
using System.Threading;

namespace Fase01Heuristica.Examples.OOWithoutInterface
{
    /// <summary>
    /// Implementação específica para Pix
    /// ✅ COESÃO: Toda lógica do Pix em uma classe
    /// </summary>
    public class PixPayment : PaymentBase
    {
        public PixPayment(decimal amount, string pixKey) 
            : base(amount, pixKey) { }
        
        protected override bool Validate()
        {
            Console.WriteLine("  🔍 Validando chave Pix...");
            
            if (string.IsNullOrEmpty(CustomerData))
            {
                Console.WriteLine("  ❌ Chave Pix não fornecida");
                return false;
            }
            
            if (CustomerData.Length < 5)
            {
                Console.WriteLine("  ❌ Chave Pix muito curta");
                return false;
            }
            
            Console.WriteLine("  ✅ Chave Pix válida");
            return true;
        }
        
        protected override bool ExecutePayment()
        {
            Console.WriteLine("  🔄 Gerando QR Code...");
            Console.WriteLine("  🌐 Conectando ao Banco Central...");
            
            // Simulação de processamento
            Thread.Sleep(500);
            
            LogTransaction();
            return true;
        }
    }
}
