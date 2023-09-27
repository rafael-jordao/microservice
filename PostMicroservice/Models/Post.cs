using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostMicroservice.Models
{
    public class Post
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("published")]
        public bool Published { get; set; }

        [Column("author_id")]
        public int AuthorId { get; set; }
    }
}