using System;

namespace Fase01Heuristica.Examples.Procedural
{
    /// <summary>
    /// Exemplo de implementação PROCEDURAL do sistema de pagamento
    /// Demonstra os problemas de usar if/switch para variações
    /// </summary>
    public static class ProceduralPayment
    {
        // ENUM para tipos de pagamento
        public enum PaymentType
        {
            Pix,
            CreditCard,
            Boleto,
            DebitCard
        }

        /// <summary>
        /// Processa pagamento de forma PROCEDURAL
        /// ⚠️ PROBLEMAS: Muitos if/switch, difícil de estender, código complexo
        /// </summary>
        public static (bool Success, string Message) ProcessPayment(
            PaymentType type, 
            decimal amount, 
            string customerData)
        {
            Console.WriteLine($"\n🔧 Processando pagamento {type} de R$ {amount:F2}");
            
            // ❌ MUITOS IF/SWITCH - DECISÕES ESPALHADAS
            switch (type)
            {
                case PaymentType.Pix:
                    // Lógica específica do Pix
                    if (string.IsNullOrEmpty(customerData))
                    {
                        return (false, "Chave Pix não fornecida");
                    }
                    
                    if (!IsValidPixKey(customerData))
                    {
                        return (false, "Chave Pix inválida");
                    }
                    
                    Console.WriteLine("  Gerando QR Code para Pix...");
                    Console.WriteLine("  Validando na API do Banco Central...");
                    
                    // Simulação de processamento
                    System.Threading.Thread.Sleep(500);
                    return (true, "Pix processado com sucesso!");

                case PaymentType.CreditCard:
                    // Lógica específica do Cartão de Crédito
                    if (!IsValidCreditCard(customerData))
                    {
                        return (false, "Cartão de crédito inválido");
                    }
                    
                    Console.WriteLine("  Validando CVV...");
                    Console.WriteLine("  Verificando limite...");
                    Console.WriteLine("  Processando na operadora...");
                    
                    // Taxa específica do cartão
                    var fee = amount * 0.03m;
                    Console.WriteLine($"  Taxa da operadora: R$ {fee:F2}");
                    
                    System.Threading.Thread.Sleep(800);
                    return (true, "Cartão de crédito processado!");

                case PaymentType.Boleto:
                    // Lógica específica do Boleto
                    Console.WriteLine("  Gerando código de barras...");
                    Console.WriteLine("  Calculando data de vencimento...");
                    
                    // Validação específica do boleto
                    if (amount < 5)
                    {
                        return (false, "Valor mínimo do boleto é R$ 5,00");
                    }
                    
                    System.Threading.Thread.Sleep(1000);
                    return (true, "Boleto gerado com sucesso!");

                case PaymentType.DebitCard:
                    // Lógica específica do Cartão de Débito
                    Console.WriteLine("  Conectando com rede débito...");
                    Console.WriteLine("  Validando senha...");
                    
                    if (!HasEnoughBalance(customerData, amount))
                    {
                        return (false, "Saldo insuficiente");
                    }
                    
                    System.Threading.Thread.Sleep(600);
                    return (true, "Débito processado com sucesso!");

                default:
                    return (false, "Tipo de pagamento não suportado");
            }
        }

        /// <summary>
        /// ❌ NOVO MÉTODO = NOVO IF/SWITCH
        /// Para adicionar PayPal, precisamos modificar o switch acima
        /// </summary>
        public static bool CanUseInstallments(PaymentType type)
        {
            // Mais if/switch espalhados pelo código
            return type switch
            {
                PaymentType.CreditCard => true,
                PaymentType.Boleto => false,
                PaymentType.Pix => false,
                PaymentType.DebitCard => false,
                _ => false
            };
        }

        // Métodos auxiliares (simulados)
        private static bool IsValidPixKey(string key) => key.Length >= 5;
        private static bool IsValidCreditCard(string card) => card.Length == 16;
        private static bool HasEnoughBalance(string card, decimal amount) => amount <= 5000;
    }
}
