namespace WAD.CODEBASE._00016643.Models
{
    public class Newspaper
    {
        public int NewspaperId { get; set; }
        public string NewspaperName { get; set; }
        public string NewspaperDescription { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
