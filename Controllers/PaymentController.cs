using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace filed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentGateway _service;


        public PaymentController(ILogger<PaymentController> logger,
                                 IPaymentGateway service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Payment newPay)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                if (ModelState.IsValid && newPay.CreditCardNumber.ValidateCNN())
                {
                    PaymentHandler handler = new PaymentHandler(_service);

                    switch (newPay.Amount)
                    {
                        case var expression when newPay.Amount <= 20:
                            apiResponse.Data = await handler.HandlePaymentGateway(1, newPay);
                            break;

                        case var expression when (newPay.Amount >= 20 && newPay.Amount <= 500):
                            apiResponse.Data = await handler.HandlePaymentGateway(2, newPay);
                            break;

                        case var expression when newPay.Amount >= 500:
                            apiResponse.Data = await handler.HandlePaymentGateway(3, newPay);
                            break;

                        default:
                            apiResponse.Data = await handler.HandlePaymentGateway(1, newPay);
                            break;
                    }

                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.Message = "The request is invalid";
                    apiResponse.IsSucceed = false;
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSucceed = false;
                apiResponse.Exception = ex;
                apiResponse.Message = ex.Message;
                return BadRequest(apiResponse);
            }
        }
    }
}
