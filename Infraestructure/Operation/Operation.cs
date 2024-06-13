using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Operation
{
    public class Operation : EntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public StatusOperation Status { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(20, 4)")]
        public decimal Amount { get; set; }
        public FieldOperation Field { get; set; }
        public string? Comments { get; set; }
        public DateTime? PaidDate { get; set; }
        [Column(TypeName = "decimal(20, 4)")]
        public decimal? QuoteAmount { get; set; }
        [Column(TypeName = "decimal(20, 4)")]
        public float? QuoteFee { get; set; }
        public int? QuoteNumber { get; set; }
        public string? PaidDestination { get; set; }
        public PaidMethodOperation? PaidMethod { get; set; }
    }
    /// <summary>
    /// There more status operations, in the future I'll add it
    /// </summary>
    public enum StatusOperation
    {
        NotPaid,
        Paid
    }
    /// <summary>
    /// Verify all field operations enable to track
    /// </summary>
    public enum FieldOperation
    {
        Shooping,
        Investment,
        Education,
        Other
    }
    /// <summary>
    /// Confirm al paid method operations
    /// </summary>
    public enum PaidMethodOperation
    {
        CreditCard,
        DebitCard,
        ExternalLoan,
        Cash
    }
}
