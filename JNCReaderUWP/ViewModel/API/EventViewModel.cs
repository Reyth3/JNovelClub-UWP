using JNovelClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCReaderUWP.ViewModel.API
{
    public class EventViewModel
    {
        public EventViewModel(Event e)
        {
            Id = e.Id;
            Title = e.Name;
            Details = e.Details;
            Date = e.Date;
            PrimaryImageUrl = new Uri(new Uri("https://s3.amazonaws.com/production.j-novel.club/"), e.Attachments.FirstOrDefault()?.FullPath).AbsoluteUri;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public string PrimaryImageUrl { get; set; }

        public string DateString { get { return $"{Date:dddd, MMMM dd}"; } }
        public string Countdown { get { return Date > DateTime.Now ? $"~{(Date - DateTime.Now).TotalHours:0} Hours Until Release" : null; } }
    }
}
