# Fase 1 — Heurística antes do código (mapa mental)

## 🎯 Objetivo da Fase
Desenvolver uma visão arquitetural antes da implementação, mapeando a evolução de uma solução procedural para uma solução orientada a objetos com interfaces.

## 📋 Problema Escolhido: Sistema de Pagamento
**Contexto:** Um e-commerce precisa processar pagamentos usando diferentes métodos (Pix, Cartão de Crédito), com regras de negócio para escolha automática.

---

## 🗺️ Mapa de Evolução do Design

### **Quadro 1 — Abordagem Procedural**
```
// Fluxo procedural com if/switch
public class PaymentService
{
    public bool ProcessPayment(decimal amount, string method)
    {
        if (method == "PIX")
        {
            // Lógica específica do Pix
            ValidatePixKey();
            GenerateQRCode();
            return ProcessPixTransfer();
        }
        else if (method == "CREDIT_CARD")
        {
            // Lógica específica do Cartão
            ValidateCard();
            CheckFraud();
            return ProcessCardPayment();
        }
        else if (method == "BOLETO")
        {
            // Lógica específica do Boleto
            GenerateBarcode();
            return RegisterBoleto();
        }
        // ... mais ifs para novos métodos
    }
}
````
## 🔍 Onde surgem if/switch:

Decisão do método de pagamento no início do método

Lógica específica de cada método espalhada em blocos condicionais

Validações diferentes para cada método no mesmo fluxo

Retorno e tratamento de erro diferente para cada caso

## ⚠️ Problemas identificados:

Acoplamento alto entre a decisão e a execução

Dificuldade para adicionar novos métodos de pagamento

Código difícil de testar (muitos caminhos condicionais)

Violação do Open/Closed Principle
