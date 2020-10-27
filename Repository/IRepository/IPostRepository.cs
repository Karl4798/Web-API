using AdMedAPI.Models;
using System.Collections.Generic;

namespace AdMedAPI.Repository.IRepository
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        Post GetPost(int postId);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool PostExists(int postId);
        bool Save();
    }
}