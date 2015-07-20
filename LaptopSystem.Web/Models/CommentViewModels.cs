using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using LaptopSystem.Models;

namespace LaptopSystem.Web.Models
{
    public class CommentViewModel
    {
        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return comment => new CommentViewModel
                {
                    Author = comment.Author.UserName,
                    Content = comment.Content
                };
            }
        }

        public string Author { get; set; }
        public string Content { get; set; }
    }

    public class SubmitCommentModel
    {
        [Required]
        public int LaptopId { get; set; }

        [Required]
        [ShouldNotContainEmail]
        public string Comment { get; set; }
    }
}