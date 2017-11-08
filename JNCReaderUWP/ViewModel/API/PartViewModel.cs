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
            Id = _model.Id;
            Title = part.Title;
            ShortTitle = part.TitleShort;
            Desctiption = part.Description;
            Expired = part.Expired;
            Preview = part.Preview;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Desctiption { get; set; }
        public bool Expired { get; set; }
        public bool Preview { get; set; }

        public SolidColorBrush BackgroundBrush
        {
            get
            {
                if (Preview && !Expired)
                    return new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                else if (!Preview && !Expired)
                    return new SolidColorBrush(Color.FromArgb(255, 255, 220, 0));
                else return new SolidColorBrush(Color.FromArgb(255, 96, 96, 96));
            }
        }

        public async Task<PartData> GetPartData()
        {
            return await _model.GetPartData();
        }
    }
}
