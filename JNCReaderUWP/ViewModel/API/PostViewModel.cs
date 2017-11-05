using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCReaderUWP.ViewModel.API
{
    public class PostViewModel
    {
        public PostViewModel(Post post)
        {
            Title = post.title;
            Details = post.content;
            ForumLink = post.forumLink;
            Date = post.date;
            PrimaryImageUrl = new Uri(new Uri("https://s3.amazonaws.com/production.j-novel.club/"), post.attachments.FirstOrDefault()?.FullPath).AbsoluteUri;
        }

        public string Title { get; set; }
        public string Details { get; set; }
        public string ForumLink { get; set; }
        public DateTime Date { get; set; }

        public string PrimaryImageUrl { get; set; }
        public string DateString { get { return $"{Date:dddd, MMMM dd}"; } }
    }
}
