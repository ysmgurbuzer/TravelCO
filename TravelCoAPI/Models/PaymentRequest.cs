namespace TravelCoAPI.Models
{
    public class PaymentRequest
    {

        public decimal cartAmount { get; set; }
        public string? nonce { get; set; }
    }
}
