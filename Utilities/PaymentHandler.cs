using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

public class PaymentHandler
{
    private readonly IPaymentGateway _service;


    public PaymentHandler(IPaymentGateway service)
    {
        _service = service;
    }
    public async Task<ApiResponse<object>> HandlePaymentGateway(int tries, Payment newPay)
    {
        ApiResponse<object> apiResponse = new ApiResponse<object>();
        try
        {
            var polly = Policy.Handle<Exception>()
           .WaitAndRetryAsync(tries, sleep => TimeSpan.FromSeconds(3));

            await polly.ExecuteAsync(async () =>
            {
                apiResponse.Message = "Payment is processed";
                apiResponse.IsSucceed = false;
                apiResponse.Data = await _service.ProcessPayment(newPay);
            });
            return apiResponse;
        }
        catch (Exception ex)
        {
            apiResponse.Message = "The request is invalid";
            apiResponse.IsSucceed = false;
            apiResponse.Exception = ex;
            return apiResponse;
        }

    }
}
