using System;

namespace Fase01Heuristica.Examples.WithInterface.Contracts
{
    /// <summary>
    /// CONTRATO: Define o comportamento que todo método de pagamento deve ter
    /// ✅ NÃO revela "como" implementar, apenas "o que" deve fazer
    /// </summary>
    public interface IPaymentMethod
    {
        /// <summary>
        /// Processa o pagamento
        /// </summary>
        /// <returns>Sucesso ou falha</returns>
        (bool Success, string Message) ProcessPayment(decimal amount, string customerData);
        
        /// <summary>
        /// Verifica se o método está disponível
        /// </summary>
        bool IsAvailable();
        
        /// <summary>
        /// Taxa específica do método (se houver)
        /// </summary>
        decimal GetFee(decimal amount);
        
        /// <summary>
        /// Tipo do pagamento (para identificação)
        /// </summary>
        string PaymentType { get; }
    }
    
    /// <summary>
    /// Interface segregada para métodos que suportam parcelamento
    /// ✅ PRINCÍPIO ISP: Interfaces específicas
    /// </summary>
    public interface IInstallmentPayment
    {
        bool CanUseInstallments(decimal amount);
        int MaxInstallments(decimal amount);
    }
    
    /// <summary>
    /// Interface segregada para métodos que geram comprovante
    /// </summary>
    public interface IReceiptGenerator
    {
        string GenerateReceipt(string transactionId);
    }
}
