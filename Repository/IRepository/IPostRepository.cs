using AdMedAPI.Models;
using System.Collections.Generic;

namespace AdMedAPI.Repository.IRepository
{
    // Post repository interface
    public interface IPostRepository
    {
        // All methods implemented in the repository
        ICollection<Post> GetPosts();
        Post GetPost(int postId);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool PostExists(int postId);
        bool Save();
    }
}