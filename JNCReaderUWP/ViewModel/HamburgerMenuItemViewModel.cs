using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCReaderUWP.ViewModel
{
    public class HamburgerMenuItemViewModel
    {
        public HamburgerMenuItemViewModel() { }
        public HamburgerMenuItemViewModel(char glyph, string title, Type pageType, Action secondaryAction = null)
        {
            Glyph = glyph;
            Title = title;
            PageType = pageType;
            SecondaryAction = secondaryAction;
        }

        public HamburgerMenuItemViewModel(char glyph, string title, Action secondaryAction)
        {
            Glyph = glyph;
            Title = title;
            PageType = null;
            SecondaryAction = secondaryAction;
        }

        public char Glyph { get; set; }
        public string Title { get; set; }
        public Type PageType { get; set; }
        public Action SecondaryAction { get; set; }
    }
}
