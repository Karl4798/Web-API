﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required] public string PostName { get; set; }
        [Required] public string PostDescription { get; set; }
        [Required] public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}