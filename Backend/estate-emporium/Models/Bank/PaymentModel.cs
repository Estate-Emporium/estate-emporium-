namespace estate_emporium.Models.Bank
{
    public class PaymentModel
    {
        public long SenderId { get; set; }
        public long AmountInMibiBBDough { get; set; }
        public Recipient Recepient { get; set; }
        public string Reference { get; set; }
    }
}
