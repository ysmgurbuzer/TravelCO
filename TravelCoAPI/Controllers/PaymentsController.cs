using Braintree;
using Hangfire;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelCoAPI.Models;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBraintreeGateway _braintreeGateway;
        private readonly IHttpContextAccessor _httpContext;


        public PaymentController(IBraintreeGateway braintreeGateway, IHttpContextAccessor httpContext)
        {
            _braintreeGateway = braintreeGateway;
            _httpContext = httpContext;
       
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            try
            {
                var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var transactionRequest = new TransactionRequest
                {
                    Amount = paymentRequest.cartAmount,
                    PaymentMethodNonce = paymentRequest.nonce,
                };

                var transactionResult = _braintreeGateway.Transaction.Sale(transactionRequest);

                if (transactionResult.IsSuccess())
                {

                    return Ok("Payment successful!");
                }
                else
                {
                    return BadRequest($"Payment failed: {transactionResult.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing payment: {ex.Message}");
            }

        }


    }
}

