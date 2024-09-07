namespace Database.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Series> SerieProducerList { get; set; } = null!;
    }
}