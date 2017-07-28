using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Models
{
    public class Series : IResponseModel
    {
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public string TitleShort { get; set; }
        public string TitleOriginal { get; set; }
        public string Author { get; set; }
        public string Illustrator { get; set; }
        public string AuthorOriginal { get; set; }
        public string IllustratorOriginal { get; set; }
        public string Translator { get; set; }
        public string Editor { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public string Tags { get; set; }
        public string ForumLink { get; set; }
        public DateTime Created { get; set; }
        public bool Override_expiration { get; set; }
        public string Id { get; set; }
        public int PostCount { get; set; }
        public Attachment[] Attachments { get; set; }
        public int TotalVolumes { get; set; }
    }

    public class Attachment
    {
        public string FullPath { get; set; }
        public int Size { get; set; }
        public string Id { get; set; }
        public bool IsImage { get; set; }
        public string Extension { get; set; }
        public string ModelType { get; set; }
        public string ForeignKey { get; set; }
        public string Filename { get; set; }
    }
}