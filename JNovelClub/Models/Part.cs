using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNovelClub.Models
{
    public class Part : IResponseModel
    {
        public string Title { get; set; }
        public string Titleslug { get; set; }
        public string TitleShort { get; set; }
        public string TitleOriginal { get; set; }
        public string Author { get; set; }
        public string Illustrator { get; set; }
        public string AuthorOriginal { get; set; }
        public string IllustratorOriginal { get; set; }
        public string Translator { get; set; }
        public string Editor { get; set; }
        public int AartNumber { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public string Tags { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Expired { get; set; }
        public bool Preview { get; set; }
        public string ForumLink { get; set; }
        public DateTime Created { get; set; }
        public string Id { get; set; }
        public string VolumeId { get; set; }
        public string SerieId { get; set; }
        public int PostCount { get; set; }
        public Attachment[] Attachments { get; set; }

        private PartData _partData;

        public Task<PartData> GetPartData()
        {
            return null;
        }
    }
}
