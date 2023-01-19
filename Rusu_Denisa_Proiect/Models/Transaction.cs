namespace Rusu_Denisa_Proiect.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public IList<TransactionItem> TransactionItems { get; set; }
        public float TotalPrice { get; set; }
        public void SetTotalPrice()
        {
            float total = 0;
            foreach (var item in TransactionItems)
            {
                if(item.Flower!=null)
                {
                    total += item.Flower.Price * item.Quantity;
                }
                else
                {
					total += item.Bouquet.Price * item.Quantity;
				}
            }
            TotalPrice = total;
        }
    }
}
