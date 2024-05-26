using DomainModel.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dashboard_budget.DTOs.Operation
{
    public class OperationDashboardDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public StatusOperation Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public FieldOperation Field { get; set; }
        public string? Comments { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? QuoteAmount { get; set; }
        public float? QuoteFee { get; set; }
        public int? QuoteNumber { get; set; }
        public string? PaidDestination { get; set; }
        public PaidMethodOperation? PaidMethod { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
