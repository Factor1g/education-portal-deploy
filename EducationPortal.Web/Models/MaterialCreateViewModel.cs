using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models
{
    public class MaterialCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Type { get; set; } // "Video", "Book", "Article"

        // Video
        public int? Duration { get; set; }
        public string? Quality { get; set; }

        // Book
        public string? Author { get; set; }
        public int? Pages { get; set; }
        public string? Format { get; set; }
        public int? Year { get; set; }

        // Article
        public DateTime? PublicationDate { get; set; }
        public string? Resource { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
