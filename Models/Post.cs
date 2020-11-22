using System;
using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models
{
    // Post entity
    public class Post
    {
        // Post fields stored in the database
        [Key]
        public int Id { get; set; }
        [Required] public string PostName { get; set; }
        [Required] public string PostDescription { get; set; }
        [Required] public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}