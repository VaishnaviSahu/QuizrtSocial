using System.Collections.Generic;
using System.Threading.Tasks;

namespace quizartsocial_backend.Models
{
    public interface ITopic
    {
        Task AddTopicToDBAsync(Topic obj);
        Task DelTopicFromDBAsync(string topicName);
        Task DelTopicByIdAsync(int id);
        Task AddPostToDBAsync(Post obj);
        Task AddUserToDBAsync(User obj);
        Task AddCommentToDBAsync(Comment obj);
        // Task AddComment(Comment obj);
        Task<List<Post>> GetPostsAsync(string topicName);
        Task<List<Topic>> FetchTopicsFromDbAsync();
       // void GetTopicsFromRabbitMQ();
    }
}

// List<Topic> GetAllTopicName();
// List<Topic> GetAllTopicImage();
// List<Post> GetAllPost();

// List<UserC> GetAllUserName();
// List<UserC> GetAllUserImage();
// void AddUserToDB(User obj);
// List<post> GetAllPosts();
// List<comments> GetAllComments();
// Task<List<string>> fetchTopicAsync();
// void AddUser(User obj);