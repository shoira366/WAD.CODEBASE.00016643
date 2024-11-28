namespace WAD.CODEBASE._00016643.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int NewspaperId { get; set; }
        public Newspaper? Newspaper { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
