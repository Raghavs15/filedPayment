using System;
using System.Threading.Tasks;

public class PaymentRepository : IPaymentGateway
{

    private readonly PaymentDbContext _context;
    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }
    public async Task<Payment> ProcessPayment(Payment newPay)
    {
        var payAdded = _context.Payment.Add(newPay);

        var result = await _context.SaveChangesAsync();
        if (result>0)
        {
            State payState = new State();
            payState.PaymentId = newPay.Id;
            payState.PaymentStatus = "Processed";
            _context.State.Add(payState);

            var paymentState = await _context.SaveChangesAsync();
            
            newPay.Status = payState;

            return paymentState > 0 ? newPay : newPay;
        }
        else
        {
            State payState = new State();
            payState.PaymentId = newPay.Id;
            payState.PaymentStatus = "Failed";

            _context.State.Add(payState);
            var paymentState = await _context.SaveChangesAsync();
            return paymentState > 0 ? newPay : newPay;
        }
    }
}