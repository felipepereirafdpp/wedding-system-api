using MercadoPago.Client;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.Xml;
using wedding_system_api.Data;
using wedding_system_api.Dtos;
using wedding_system_api.InterfaceServices;
using wedding_system_api.Models;
using wedding_system_api.Settings;

namespace wedding_system_api.Services
{
    public class PaymentService : IPaymentInterface
    {
        private readonly wedding_system_apiContext _context;
        private readonly MercadoPagoSettings _settings;

        public PaymentService(
            wedding_system_apiContext context,
            IOptions<MercadoPagoSettings> options)
        {
            _context = context;
            _settings = options.Value;
        }

        public async Task<CreatePaymentResponseDto?> CreateCheckoutAsync(Guid presentId)
        {
            // 1. O preço vem SEMPRE do banco, nunca do cliente
            var present = await _context.presentsModel.FindAsync(presentId);

            if (present == null)
            {
                return null;
            }

            if (present.PricePresent <= 0)
            {
                throw new InvalidOperationException("O presente não tem um preço válido para cobrança.");
            }

            // 2. Registra a tentativa de pagamento como Pendente
            var payment = new PaymentModel
            {
                Id = Guid.NewGuid(),
                PresentId = present.Id,
                Amount = present.PricePresent,
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // 3. Monta a preferência. O external_reference liga o Mercado Pago ao nosso registro
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Id = present.Id.ToString(),
                        Title = present.NamePresent,
                        Description = present.DescriptionPresent,
                        Quantity = 1,
                        CurrencyId = "BRL",
                        UnitPrice = payment.Amount
                    }
                },
                ExternalReference = payment.Id.ToString()
            };

            var requestOptions = new RequestOptions
            {
                AccessToken = _settings.AccessToken
            };

            // 4. Envia ao Mercado Pago
            Preference preference;

            try
            {
                var client = new PreferenceClient();
                preference = await client.CreateAsync(request, requestOptions);
            }
            catch (Exception)
            {
                // Se o Mercado Pago falhar, não deixamos um pagamento "Pendente" para sempre
                payment.Status = PaymentStatus.Cancelled;
                payment.MercadoPagoStatus = "preference_creation_failed";
                payment.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                throw;
            }

            // 5. Guarda o id da preferência e devolve o link do checkout
            payment.MercadoPagoReferenceId = preference.Id;
            payment.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new CreatePaymentResponseDto
            {
                PaymentId = payment.Id,
                PreferenceId = preference.Id,
                CheckoutUrl = preference.InitPoint
            };
        }
    }
}