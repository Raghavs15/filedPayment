using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Column("Id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("CreditCardNumber")]
    [Required(ErrorMessage = "Credit Card Number is Required")]
    [MaxLength(16), MinLength(16)]
    public string CreditCardNumber { get; set; }

    [Column("CardHolderName")]
    [Required(ErrorMessage = "CardHolder is Required")]
    public string CardHolder { get; set; }

    [Column("ExpirationDate")]
    [Required(ErrorMessage = "Expiration Date is Required")]
    public DateTime ExpirationDate { get; set; }

    [Column("SecurityCode")]
    [MaxLength(3), MinLength(3)]
    public string SecurityCode { get; set; }

    [Column("Amount")]
    [Required(ErrorMessage = "Amount is Required")]
    public double Amount { get; set; }

    [Column("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    public virtual State Status { get; set; }

}