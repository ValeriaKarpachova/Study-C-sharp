
namespace Lab4task123
{
    public class City
    {
        public string CityName { get; set; }
        public int Population { get; set; }
        public string Region { get; set; }
        public string Sights { get; set; }
    }

    public static class UkrainianRegions
    {
        public static readonly string[] Regions = new[]
        {
            "Autonomous Republic of Crimea",
            "Vinnytsia",
            "Volyn",
            "Dnipropetrovsk",
            "Donetsk",
            "Zhytomyr",
            "Transcarpathia",
            "Zaporizhia",
            "Ivano-Frankivsk",
            "Kyiv",
            "Kirovohrad",
            "Lviv",
            "Mykolaiv",
            "Odesa",
            "Poltava",
            "Rivne",
            "Sumy",
            "Ternopil",
            "Kharkiv",
            "Kherson",
            "Khmelnytskyi",
            "Cherkasy",
            "Chernivtsi",
            "Chernihiv"
        };
    }
}
