using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace JNCReaderUWP.ViewModel.API
{
    public class PartViewModel
    {
        private Part _model;

        public PartViewModel(Part part)
        {
            _model = part;
            Title = part.Title;
            ShortTitle = part.TitleShort;
            Desctiption = part.Description;
            Expired = part.Expired;
            Preview = part.Preview;
        }

        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Desctiption { get; set; }
        public bool Expired { get; set; }
        public bool Preview { get; set; }

        public SolidColorBrush BackgroundBrush { get { return Expired && !Preview ? new SolidColorBrush(Color.FromArgb(255, 96, 96, 96)) : new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)); } }

        public async Task<PartData> GetPartData()
        {
            return await _model.GetPartData();
        }
    }
}
