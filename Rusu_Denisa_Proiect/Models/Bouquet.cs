namespace Rusu_Denisa_Proiect.Models
{
    public class Bouquet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public void SetPrice()
        {
            this.Price = FlowerBouquets.Sum(fb => fb.Flower.Price * fb.Quantity);
        }
        public IList<FlowerBouquet> FlowerBouquets { get; set; }
    }
}
