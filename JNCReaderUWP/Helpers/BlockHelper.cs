using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;

namespace JNCReaderUWP.Helpers
{
    public static class BlockHelper
    {
        public static string GetRawText(this Block block)
        {
            var p = block as Paragraph;
            if(p != null)
            {
                var text = "";
                foreach (var inline in p.Inlines)
                    if (inline is Run)
                        text += (inline as Run).Text;
                    else if (inline is Bold)
                        text += ((inline as Bold).Inlines.FirstOrDefault() as Run)?.Text;
                    else if (inline is Italic)
                        text += ((inline as Italic).Inlines.FirstOrDefault() as Run)?.Text;
                    else if (inline is LineBreak)
                        text += "\n";
                return text;
            }
            return null;
        }
    }
}
