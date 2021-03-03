using System;
using Microsoft.EntityFrameworkCore;

public class PaymentDbContext : DbContext
{

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {

    }

    public DbSet<Payment> Payment {get; set;}
    public DbSet<State> State{get; set;}

}