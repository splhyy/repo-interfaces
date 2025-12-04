using System;

namespace Fase01Heuristica.Examples.OOWithoutInterface
{
    /// <summary>
    /// Serviço que processa pagamentos
    /// ⚠️ PROBLEMA: Ainda conhece as classes concretas
    /// </summary>
    public class PaymentService
    {
        public (bool Success, string Message) ProcessPayment(
            string paymentType, 
            decimal amount, 
            string customerData)
        {
            // ❌ AINDA TEM SWITCH PARA INSTANCIAR
            PaymentBase payment = paymentType.ToUpper() switch
            {
                "PIX" => new PixPayment(amount, customerData),
                "CREDIT_CARD" => new CreditCardPayment(amount, customerData),
                // ❌ NOVO TIPO = NOVA LINHA AQUI
                _ => throw new ArgumentException($"Tipo não suportado: {paymentType}")
            };
            
            return payment.Process();
        }
        
        // ❌ PROBLEMA: Se precisar de método específico, precisa fazer cast
        public bool CheckInstallments(string paymentType, decimal amount, string customerData)
        {
            if (paymentType.ToUpper() == "CREDIT_CARD")
            {
                var payment = new CreditCardPayment(amount, customerData);
                return payment.CanUseInstallments(); // ✅ Funciona
            }
            
            // ❌ E os outros tipos? Precisamos verificar cada um
            return false;
        }
    }
}
