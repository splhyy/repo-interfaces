using System;

namespace Fase01Heuristica.Examples.OOWithoutInterface
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("🎯 FASE 1 - EXEMPLO OO SEM INTERFACE");
            Console.WriteLine("======================================\n");
            
            var service = new PaymentService();
            
            // Testando diferentes pagamentos
            var results = new[]
            {
                service.ProcessPayment("PIX", 150.50m, "123.456.789-00"),
                service.ProcessPayment("CREDIT_CARD", 1200.00m, "4111111111111111"),
            };
            
            foreach (var (success, message) in results)
            {
                Console.WriteLine($"Resultado: {(success ? "✅" : "❌")} {message}");
            }
            
            Console.WriteLine("\n📊 ANÁLISE DA EVOLUÇÃO:");
            Console.WriteLine("========================");
            Console.WriteLine("✅ MELHORIAS:");
            Console.WriteLine("   1. Encapsulamento: Cada tipo em sua classe");
            Console.WriteLine("   2. Coesão: Lógica específica isolada");
            Console.WriteLine("   3. Remoção de if/switch do fluxo principal");
            Console.WriteLine("   4. Fácil testar cada classe separadamente");
            
            Console.WriteLine("\n❌ PROBLEMAS PERSISTENTES:");
            Console.WriteLine("   1. Serviço ainda conhece classes concretas");
            Console.WriteLine("   2. Switch na instanciação (new)");
            Console.WriteLine("   3. Dificuldade para mockar em testes");
            Console.WriteLine("   4. Casts para métodos específicos");
            
            Console.WriteLine("\n💡 PRÓXIMO PASSO: Interfaces para desacoplamento total!");
        }
    }
}
