

using Models;

namespace DataAnalyzer
{
    public class DataAnalyzer
    {
        public static int GetGoodActivities()
        {
            return new Context().Activities
            .Where(x => x.Good)
            .Count();
        }

        public static int GetBadActivities()
        {
            return new Context().Activities
            .Where(x => x.Bad)
            .Count();
        }

        public static List<string> GetActivityTypes()
        {
            return new Context().Activities
            .Select(x => x.Type)
            .Distinct()
            .OrderBy(x => x)
            .ToList();
        }
    }
}