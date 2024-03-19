namespace _Models
{
    public class Activity
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Participants { get; set; }
        public decimal Price { get; set; }
        public decimal Accessibility { get; set; }
    }
}