namespace Rusu_Denisa_Proiect.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public IEnumerable<FlowerBouquet>? FlowerBouquets { get; set; }
    }
}
