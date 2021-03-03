using System;
using System.Threading.Tasks;

public interface IPaymentGateway{
    Task<Payment> ProcessPayment(Payment newPay);
}