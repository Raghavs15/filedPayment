using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class State{
    [Column("Id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

     [Column("PaymentId")]
     [ForeignKey("PaymentId")]
    public long PaymentId { get; set; }

    [Column("PaymentStatus")]
    public string PaymentStatus { get; set; }
}