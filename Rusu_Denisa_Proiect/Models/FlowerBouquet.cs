namespace Rusu_Denisa_Proiect.Models
{
    public class FlowerBouquet
    {
        public int Id { get; set; }
        public int FlowerId { get; set; }
        public Flower? Flower { get; set; }
        public int BouquetId { get; set; }
        public Bouquet? Bouquet { get; set; }
        public int Quantity { get; set; }
    }
}
