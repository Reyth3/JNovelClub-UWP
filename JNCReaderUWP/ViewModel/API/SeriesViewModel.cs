using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCReaderUWP.ViewModel
{
    public class SeriesViewModel
    {
        public SeriesViewModel(Series s)
        {
            Title = s.Title;
            Description = s.Description;
            OriginalTitle = s.TitleOriginal;
            IllustratedBy = $"{s.Illustrator} ({s.IllustratorOriginal})";
            Author = $"{s.Author} ({s.AuthorOriginal})";
            TranslatedBy = s.Translator;
            ShortDescription = s.DescriptionShort;
            CoverUrl = new Uri(new Uri("https://s3.amazonaws.com/production.j-novel.club/"), s.Attachments.FirstOrDefault()?.FullPath).AbsoluteUri;
            Tags = s.Tags;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string OriginalTitle { get; set; }
        public string TranslatedBy { get; set; }
        public string IllustratedBy { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string CoverUrl { get; set; }
        public string Tags { get; set; }

        public KeyValueViewModel[] Properties { get
            {
                return new KeyValueViewModel[]
                {
                    new KeyValueViewModel("Original Title", OriginalTitle),
                    new KeyValueViewModel("Author", Author),
                    new KeyValueViewModel("Illustrator", IllustratedBy),
                    new KeyValueViewModel("Translator", TranslatedBy),
                    new KeyValueViewModel("Description", Description),
                    new KeyValueViewModel("Tags", Tags),
                };
            }
        }
    }
}
