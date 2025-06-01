using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using StripeCheckoutDemo.Models;

namespace StripeCheckoutDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : Controller
    {
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] CheckoutFormModel model)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card", "revolut_pay", "bacs_debit" },
                // PaymentMethodTypes = new List<string> { "bacs_debit" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = model.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = model.ProductName,
                                Description = model.ProductDescription,
                            },
                            UnitAmount = model.Amount,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                // UiMode = "embedded",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id, checkoutUrl = session.Url });
        }

        [HttpPost("create-checkout-session-maui")]
        public IActionResult CreateCheckoutSessionMaui([FromBody] CheckoutFormModel model)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card", "revolut_pay", "bacs_debit" },
                // PaymentMethodTypes = new List<string> { "bacs_debit" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = model.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = model.ProductName,
                                Description = model.ProductDescription,
                            },
                            UnitAmount = model.Amount,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success-maui?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id, checkoutUrl = session.Url });
        }


        [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet("success-maui")]
        public IActionResult Success([FromQuery] string session_id)
        {
            return Content($@"
                <html>
                <head>
                    <title>Payment Success</title>
                    <script>
                        // Try to open the app
                        window.location.href = 'stripecheckout://success?session_id={session_id}';

                        // If app doesn't open after 1 second, show a message
                        setTimeout(function() {{
                            document.getElementById('message').style.display = 'block';
                        }}, 1000);
                    </script>
                </head>
                <body>
                    <div id='message' style='display: none; text-align: center; padding: 20px;'>
                        <h2>Payment Successful!</h2>
                        <p>You can now close this window and return to the app.</p>
                    </div>
                </body>
                </html>
            ", "text/html");
        }

        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return Content($@"
                <html>
                <head>
                    <title>Payment Cancelled</title>
                    <script>
                        // Try to open the app
                        window.location.href = 'stripecheckout://cancel';
                        
                        // If app doesn't open after 1 second, show a message
                        setTimeout(function() {{
                            document.getElementById('message').style.display = 'block';
                        }}, 1000);
                    </script>
                </head>
                <body>
                    <div id='message' style='display: none; text-align: center; padding: 20px;'>
                        <h2>Payment Cancelled</h2>
                        <p>You can now close this window and return to the app.</p>
                    </div>
                </body>
                </html>
            ", "text/html");
        }
    }
}
