using System;
using Fase01Heuristica.Examples.Procedural;

namespace Fase01Heuristica.Examples.Procedural
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("🎯 FASE 1 - EXEMPLO PROCEDURAL");
            Console.WriteLine("================================\n");
            
            // Testando diferentes tipos de pagamento
            var testCases = new[]
            {
                (ProceduralPayment.PaymentType.Pix, 150.50m, "12345678900"),
                (ProceduralPayment.PaymentType.CreditCard, 1200.00m, "4111111111111111"),
                (ProceduralPayment.PaymentType.Boleto, 89.90m, ""),
                (ProceduralPayment.PaymentType.DebitCard, 300.00m, "1234567890123456"),
            };
            
            foreach (var (type, amount, data) in testCases)
            {
                var result = ProceduralPayment.ProcessPayment(type, amount, data);
                Console.WriteLine($"Resultado: {(result.Success ? "✅" : "❌")} {result.Message}");
                Console.WriteLine();
            }
            
            Console.WriteLine("\n📊 ANÁLISE DOS PROBLEMAS:");
            Console.WriteLine("==========================");
            Console.WriteLine("❌ 1. MUITOS IF/SWITCH: Decisões espalhadas no código");
            Console.WriteLine("❌ 2. DIFÍCIL MANUTENÇÃO: Novo método = modificar switch");
            Console.WriteLine("❌ 3. BAIXA COESÃO: Lógica de cada tipo misturada");
            Console.WriteLine("❌ 4. DIFÍCIL TESTAR: Muitos caminhos condicionais");
            Console.WriteLine("❌ 5. VIOLA OCP: Fechado para modificação");
            
            Console.WriteLine("\n💡 SOLUÇÃO: Orientação a Objetos com Polimorfismo!");
        }
    }
}
