using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionProcess.Core.Entities
{
    public class TransactionRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Transaction ID cannot exceed 50 characters")]
        public string? TransactionId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Account Number cannot exceed 30 characters")]
        public string? AccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency Code must be exactly 3 characters")]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency Code must be in ISO4217 format")]
        public string? CurrencyCode { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression(@"^[ARD]$", ErrorMessage = "Status must be either A, R, or D")]
        public string? Status { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(1024)]
        public string? CreateUser { get; set; }
    }
}