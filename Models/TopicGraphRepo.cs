// using System;
// using Bogus;
// using System.Linq;
// using System.Collections.Generic;
// using quizartsocial_backend.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Newtonsoft.Json.Linq;
// using System.IO;
// using System.Net;
// using Neo4jClient;
// using quizartsocial_backend.Services;

// namespace quizartsocial_backend
// {
//     public class TopicGraphRepo : ITopic
//     {
//         private IGraphClient graph;
//         public TopicGraphRepo(GraphDb graphDb)
//         {
//             this.graph = graphDb.graph;
//         }

//         // public async Task AddCommentToDBAsync(Comment obj)
//         public async Task AddComment(Comment obj)
//         {
//             await graph.Cypher
//                 .Create("(c:comment)")
//                 .Set("c={obj}")
//                 .WithParams(new
//                 {
//                     obj
//                 })
//                 .ExecuteWithoutResultsAsync();
//         }

//         public Task AddCommentToDBAsync(Comment obj)
//         {
//             throw new NotImplementedException();
//         }

//         public Task AddPostToDBAsync(Post obj)
//         {
//             throw new NotImplementedException();
//         }

//         // public async Task AddPostToDBAsync(Post obj)
//         // {
//         //     List<Comment> comments = new List<Comment>();
//         //     if (obj.comments != null)
//         //     {
//         //         comments = new List<Comment>(obj.comments);
//         //         obj.comments = null;
//         //     }
//         //     var topicId = obj.topicId;
//         //     obj.topicId = null;
//         //     obj.topicName = null;
//         //     await graph.Cypher
//         //         .Create("(p:Post)")
//         //         .Set("p={obj}")
//         //         .Match("(t:Topic {topicId:{topicId}})")
//         //         .With("p,t")
//         //         .Create("(p)-[:POSTED_IN]->(t)")
//         //         .WithParams(new
//         //         {
//         //             obj,
//         //             topicId = topicId
//         //         })
//         //         .ExecuteWithoutResultsAsync();
//         // }

//         public async Task AddTopicToDBAsync(Topic obj)
//         {
//             List<Post> posts = new List<Post>();
//             if (obj.posts != null)
//             {
//                 posts = new List<Post>(obj.posts);
//                 obj.posts = null;
//             }
//             await graph.Cypher
//                 .Create("(t:Topic)")
//                 .Set("t={obj}")
//                 .WithParams(new
//                 {
//                     obj
//                 })
//                 .ExecuteWithoutResultsAsync();
//         }

//         public async Task AddUserToDBAsync(User obj)
//         {
//             List<Post> posts = new List<Post>();
//             if (obj.posts != null)
//             {
//                 posts = new List<Post>(obj.posts);
//                 obj.posts = null;
//             }
//             List<Comment> comments = new List<Comment>();
//             if (obj.comments != null)
//             {
//                 comments = new List<Comment>(obj.comments);
//                 obj.comments = null;
//             }
//             await graph.Cypher
//                .Create("(u:User)")
//                .Set("u={obj}")
//                .WithParams(new
//                {
//                    obj
//                })
//                .ExecuteWithoutResultsAsync();

//         }

//         public async Task<List<Topic>> FetchTopicsFromDbAsync()
//         {
//             return new List<Topic>(await graph.Cypher
//                 .Match("(t:Topic)")
//                 .Return(t => t.As<Topic>())
//                 .ResultsAsync);

//         }

//         public async Task<List<Post>> GetPostsAsync(string topicName)
//         {
//              return new List<Post>(await graph.Cypher
//                 .Match("(p.post)")
//                 .Where((Post p) => p.topicName == topicName)
//                 .Return(p => p.As<Post>())
//                 .ResultsAsync);
//         }

    
//     }
// }