using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quizartsocial_backend.Models
{
    public class Post
    {
        [Key]
        public int postId{get; set;}
        public string post{get; set;}
        public int commentId{get; set;}
        public int topicId { get; set; }
       public string topicName{get; set;}
        public string userId { get; set; }
    }
}
