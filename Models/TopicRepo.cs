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

// namespace quizartsocial_backend
// {
//     public class TopicRepo : ITopic
//     {
//         SocialContext context;
//         public TopicRepo(SocialContext _context)
//         {
//             this.context = _context;
//         }
//         public async Task<List<Post>> GetPostsAsync(string topicName)
//         {
//             List<Post> posts = await context.Topics
//                                .Where(t => t.topicName == topicName)
//                                .Include("posts").SelectMany(s => s.posts)
//                                .Include("comments")
//                                .ToListAsync();
//                                 return posts;
//         }
//         public async Task<List<Topic>> FetchTopicsFromDbAsync()
//         {
//             Topic test1= new Topic();
//             Topic test2= new Topic();
//             test1.topicName="book";
//             test1.topicImage="sad";
//             await AddTopicToDBAsync(test1);
//             await AddTopicToDBAsync(test2);
//             List<Topic> res = await context.Topics.ToListAsync();
//             return res;
//         }

//         public async Task AddTopicToDBAsync(Topic obj)
//         {
//             if (context.Topics.FirstOrDefault(n => n.topicName == obj.topicName) == null)
//             {
//                 await context.Topics.AddAsync(obj);
//                 await context.SaveChangesAsync();
//             }
//         }

//         public async Task AddPostToDBAsync(Post obj)
//         {
//             await context.Posts.AddAsync(obj);
//             await context.SaveChangesAsync();
//         }

//         public async Task AddUserToDBAsync(User obj)
//         {
//             if(context.Users.FirstOrDefault(n => n.userId == obj.userId) == null)
//             {
//                 await context.Users.AddAsync(obj);
//                 await context.SaveChangesAsync();
//             }
//         }

//         public async Task AddCommentToDBAsync(Comment obj)
//         {
//             await context.Comments.AddAsync(obj);
//             await context.SaveChangesAsync();
//         }
//     }
// }

// /*
//         public List<Topic> GetAllTopicImage()
//         {
//              var userFaker = new Faker<Topic>()
//             .RuleFor(t => t.topic_image, f => f.Image.People());
//             .RuleFor(t => t.topic_image, f => f.Internet.Avatar());
//             var users = userFaker.Generate(1);
//             return users;
//         }
        
//         public List<Topic> GetAllTopicName()
//         {
//              var userFaker1 = new Faker<Topic>()
//             .RuleFor(t => t.topic_name, f => f.Name.FirstName());
//             var myusers = userFaker1.Generate(1);
//             return myusers;
//         }

//         public List<Post> GetAllPost()
//         {
//              var userFaker2 = new Faker<Post>()
//             .RuleFor(t => t.posts, f => f.Lorem.Sentence());
//             var myusers = userFaker2.Generate(1);
//             return myusers;
//         }
        

// public List<UserC> GetAllUserImage()
// {
//     var userFaker3 = new Faker<UserC>()
//     .RuleFor(t => t.topic_image, f => f.Image.People());
//     .RuleFor(t => t.user_image, f => f.Internet.Avatar());
//     var users = userFaker3.Generate(1);
//     return users;
// }

// public List<UserC> GetAllUserName()
// {
//     var userFaker4 = new Faker<UserC>()
//     .RuleFor(t => t.user_name, f => f.Name.FirstName());
//     var myusers = userFaker4.Generate(1);
//     return myusers;
// }

//  public async Task<List<string>> fetchTopicAsync()
// {
//      string topicUrl = "http://172.23.238.164:8080/api/quizrt/topics";
//     //The 'using' will help to prevent memory leaks.
//     //Create a new instance of HttpClient
//     List<string> lg = new List<string>();
//      using (HttpClient client = new HttpClient())

//     //Setting up the response...         
//     using (HttpResponseMessage res = await client.GetAsync(topicUrl))
//     using (HttpContent content = res.Content)
//     {
//         string data = await content.ReadAsStringAsync();
//        // Console.WriteLine(data+"prateeeeeeeeeeeeeeeeeeeeeeek");
//         //data = data.Trim( new Char[] { '[',']' } );
//          JArray json = JArray.Parse(data);
//          // Console.WriteLine(json+"jkfsfjksfjsjfhskhfks");
//         // string ret;
//         // if (data != null)
//         // {
//         //     for(int i=0;i<json.Count;i++)
//         //     {
//         //         ret=(string)json[i]["topicName"];
//         //         if(!(lg.Contains(ret)))
//         //         lg.Add(ret);
//         //     }
//         // }
//         string value;
//         if(data != null){
//             for(int i = 0;i < json.Count; i++)
//             {
//                 value = (string)json[i];
//                 if(!(lg.Contains(value)))
//                 {
//                     lg.Add(value);
//                 }
//             //    Console.WriteLine(json[i]);
//             }
//         }
//         return lg;
//     }
// }

// List<List<Post>> x= await context.Topics
//        .Where(t => t.topicName == topicName)
//        .Include("posts").Select(s =>s.posts)
//        .Include("comments")
//        .ToListAsync();
// Console.WriteLine(x+"asdadsasddsa");
// */