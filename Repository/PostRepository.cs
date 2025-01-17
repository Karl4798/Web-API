﻿using System.Collections.Generic;
using System.Linq;
using AdMedAPI.Data;
using AdMedAPI.Models;
using AdMedAPI.Repository.IRepository;

namespace AdMedAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        // Injected dependancy for the application database context
        private readonly ApplicationDbContext _db;

        // Constructor
        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<Post> GetPosts()
        {
            return _db.Posts.OrderByDescending(a => a.TimeStamp).ToList();
        }

        public Post GetPost(int postId)
        {
            return _db.Posts.FirstOrDefault(a => a.Id == postId);
        }

        public bool CreatePost(Post post)
        {
            _db.Posts.Add(post);
            return Save();
        }

        public bool UpdatePost(Post post)
        {
            _db.Posts.Update(post);
            return Save();
        }

        public bool DeletePost(Post post)
        {
            _db.Posts.Remove(post);
            return Save();
        }

        public bool PostExists(int postId)
        {
            return _db.Posts.Any(a => a.Id == postId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

    }
}