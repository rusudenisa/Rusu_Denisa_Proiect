namespace Rusu_Denisa_Proiect.Models
{
    public class TransactionItem
    {
        public int Id { get; set; }
        public int? FlowerId { get; set; }
        public Flower Flower { get; set; }
        public int? BouquetId { get; set; }
        public Bouquet Bouquet { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int Quantity { get; set; }
    }
}
