using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using Newtonsoft.Json.Linq;
using quizartsocial_backend.Models;
using quizartsocial_backend.Services;

namespace backEnd.Controllers
{
    [Route("api")]
    [ApiController]
    public class SocialController : ControllerBase
    {
        // ITopic topicObj;
        GraphDb graph;

        public SocialController(GraphDb graph)
        {
            // this.topicObj=_topicObj;
            this.graph = graph;
        }
        //Post user
        [HttpPost]
        [Route("user")]
        public IActionResult CreateUser([FromBody] User value)
        {
            graph.graph.Cypher
              .Create("(u:User)")
              .Set("u={value}")
              .WithParams(new
              {
                  id = value.userId,
                  value
              })
              .ExecuteWithoutResultsAsync();
            return Ok();
        }
        //Post topics
        [HttpPost("topics")]
        public async Task AddTopicToDBAsync(Topic obj)
        {
            List<Post> posts = new List<Post>();
            if (obj.posts != null)
            {
                posts = new List<Post>(obj.posts);
                obj.posts = null;
            }
            await graph.graph.Cypher
                .Create("(t:Topic)")
                .Set("t={obj}")
                .WithParams(new
                {
                    obj
                })
                .ExecuteWithoutResultsAsync();
        }

        //Posts by user on topic
        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            var query = graph.graph.Cypher
             .Match("(u:User)", "(t:Topic)")
             .Where((User u) => u.userId == post.userId)
             .AndWhere((Topic t) => t.topicId == post.topicId)
             .Create("(u)-[:created]->(p:Post {post})-[:onTopic]->(t)")
             .WithParams(new
             {
                 post
             });
            Console.WriteLine(query.Query.QueryText);
            await query.ExecuteWithoutResultsAsync();
            return Ok();
        }
        //comment on post by user
        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> WriteComment([FromBody] Comment comment)
        {
            var query = graph.graph.Cypher
               .Match("(u:User)", "(p:Post)")
               .Where((User u) => u.userId == comment.userId)
               .AndWhere((Post p) => p.postId == comment.postId)
               .Create("(u)-[:writes]->(c:Comment {comment})-[:onPost]->(p)")
               .WithParams(new
               {
                   comment
               });
            Console.WriteLine(query.Query.QueryText);
            await query.ExecuteWithoutResultsAsync();
            return Ok();
        }
        //get all users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userResults = await graph.graph.Cypher
             .Match("(user:User)")
             .Return(user => user.As<User>())
             .ResultsAsync;
            return Ok(userResults);
        }

        //Get user by id
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var userResult = await graph.graph.Cypher
           .Match("(user:User)")
           .Where((User user) => user.userId == userId)
           .Return(user => user.As<User>())
           .ResultsAsync;
            return Ok(userResult);
        }
        //Get post by id
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var postResult = await graph.graph.Cypher
           .Match("(user:User)")
           .Where((Post post) => post.postId == postId)
           .Return(post => post.As<User>())
           .ResultsAsync;
            return Ok(postResult);
        }
        //get all topics
        [HttpGet("topics")]
        public async Task<IActionResult> GetAllTopics()
        {
            var allTopics = await graph.graph.Cypher
             .Match("(topic:Topic)")
             .Return(topic => topic.As<Topic>())
             .ResultsAsync;
            return Ok(allTopics);
        }

        //get all posts 
        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var allPosts = await graph.graph.Cypher
             .Match("(post:Post)")
             .Return(post => post.As<Post>())
             .ResultsAsync;
            return Ok(allPosts);
        }
        //get all posts on a topic 
        [HttpGet("topic/posts/{t_id}")]
        public async Task<IActionResult> GetPostsOnTopic(int t_id)
        {
            var postsOnTopic = await graph.graph.Cypher
             .OptionalMatch("(p:Post)-[:onTopic]-(t:Topic)")
             .Where((Topic t) => t.topicId == t_id)
             .Return((t, p) => new
             {
                 Topic = t.As<Topic>(),
                 Post = p.CollectAs<Post>()
             })
             .ResultsAsync;
            return Ok(postsOnTopic);
        }
        //get all comments on a post 
        [HttpGet("post/comments/{p_id}")]
        public async Task<IActionResult> GetCommentsOnPost(int p_id)
        {
            var commentsOnPost = await graph.graph.Cypher
             .OptionalMatch("(c:Comment)-[:onPost]-(p:Post)")
             .Where((Post p) => p.postId == p_id)
             .Return((p, c) => new
             {
                 Post = p.As<Post>(),
                 Comment = c.CollectAs<Comment>()
             })
             .ResultsAsync;
            return Ok(commentsOnPost);
        }

        //get all posts by a user
        [HttpGet("user/posts/{u_id}")]
        public async Task<IActionResult> GetPostsByUser(string u_id)
        {
            var postsByUser = await graph.graph.Cypher
             .OptionalMatch("(u:User)-[:created]-(p:Post)")
             .Where((User u) => u.userId == u_id)
             .Return((u, p) => new
             {
                 User = u.As<User>(),
                 Post = p.CollectAs<Post>()
             })
             .ResultsAsync;

        //    var user1 = await graph.graph.Cypher
        //         .Match("(user:User)")
        //         .Where((User user) => user.userId == u_id)
        //         .Return(user => user.As<User>())
        //    .ResultsAsync;
        //     // return Ok(user1);

        //     var postsByUser = await graph.graph.Cypher
        //         .OptionalMatch("(u)-[:created]-(p:Post)")
        //         .Where((User u) => u.userId == u_id)
        //         .Return((u, p) => new
        //         {
        //             User = u.As<User>(),
        //             Post = p.CollectAs<Post>()
        //         })
        //         .ResultsAsync;
            return Ok(postsByUser);
        }

        // delete a user
        [HttpDelete("user/{id}")]
        public void DeleteUser(string id)
        {
            graph.graph.Cypher
           .Match("(user:User)")
           .Where((User user) => user.userId == id)
           .Delete("user")
           .ExecuteWithoutResults();
        }
        // delete a post
        [HttpDelete("post/{id}")]
        public void DeletePost(int id)
        {
            graph.graph.Cypher
           .Match("(post:Post)")
           .Where((Post post) => post.postId == id)
           .Delete("post")
           .ExecuteWithoutResults();
        }
        // delete a comment
        [HttpDelete("comment/{id}")]
        public void DeleteComment(int id)
        {
            graph.graph.Cypher
           .Match("(comment:Comment)")
           .Where((Comment comment) => comment.commentId == id)
           .Delete("comment")
           .ExecuteWithoutResults();
        }
    }
}

