namespace Models
{
    public class Activity
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Participants { get; set; }
        public decimal Price { get; set; }
        public decimal Accessibility { get; set; }
        public bool Good {get; set;}
        public bool Bad { get; set;}

        public string LongAccessibility
        {
            get
            {
                if (Accessibility < (decimal).31)
                    return "Few to no challenges";
                if (Accessibility >= (decimal).31 && Accessibility < (decimal).67)
                    return "Minor challenges";
                
                return "Major challenges";
            }
        }

        public string LongPrice
        {
            get
            {
                if(Price == 0)
                    return "Free";
                if (Price > 0 && Price < (decimal).21)
                    return "Most likely cheap";
                if (Price >= (decimal).31 && Price < (decimal).67)
                    return "Approximately moderate";
                
                return "Potentially expensive";
            }
        }
    }
}