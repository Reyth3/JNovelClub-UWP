using HtmlAgilityPack;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace JNCReaderUWP.Helpers
{
    public class HTMLHelper
    {
        public static List<Block> ConvertHTML(string html)
        {
            var blocks = new List<Block>();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                var block = ConvertHTMLNode(node) as Block;
                if (block != null)
                    blocks.Add(block);
            }
            return blocks;
        }

        private static TextElement ConvertHTMLNode(HtmlNode node)
        {
            if (node.NodeType != HtmlNodeType.Text)
                switch (node.Name)
                {
                    case "h1":
                        var header1 = new Windows.UI.Xaml.Documents.Paragraph() { FontSize = 26, FontWeight = FontWeights.Medium, TextAlignment = Windows.UI.Xaml.TextAlignment.Center };
                        header1.Inlines.Add(new Run() { Text = node.InnerText });
                        return header1;
                    case "h2":
                        var header2 = new Windows.UI.Xaml.Documents.Paragraph() { FontSize = 17, FontWeight = FontWeights.Medium };
                        header2.Inlines.Add(new Run() { Text = node.InnerText });
                        return header2;
                    case "p":
                        var paragraph = new Paragraph() { TextIndent = 16, FontSize = 16, Margin = new Windows.UI.Xaml.Thickness(6) };
                        foreach (var childNode in node.ChildNodes)
                            paragraph.Inlines.Add(ConvertHTMLNode(childNode) as Inline);
                        return paragraph;
                    case "b":
                    case "strong":
                        var bold = new Bold();
                        foreach (var childNode in node.ChildNodes)
                            bold.Inlines.Add(ConvertHTMLNode(childNode) as Inline);
                        return bold;
                    case "i":
                    case "em":
                        var italics = new Italic();
                        foreach (var childNode in node.ChildNodes)
                            italics.Inlines.Add(ConvertHTMLNode(childNode) as Inline);
                        return italics;
                    case "u":
                        var underline = new Underline();
                        foreach (var childNode in node.ChildNodes)
                            underline.Inlines.Add(ConvertHTMLNode(childNode) as Inline);
                        return underline;
                    case "img":
                        var imageBlock = new Paragraph();
                        var image = new InlineUIContainer();
                        var imageEx = new ImageEx() { IsCacheEnabled = true, Stretch = Windows.UI.Xaml.Media.Stretch.Uniform, Padding = new Windows.UI.Xaml.Thickness(4) };
                        imageEx.Source = node.GetAttributeValue("src", "");
                        image.Child = imageEx;
                        imageBlock.Inlines.Add(image);
                        return imageBlock;
                }
            else
            {
                var text = new Run() { Text = node.InnerText };
                return text;
            }
            return null;
        }
    }

    public static class HTMLHelperExtensions
    {
        public static void SetHTML(this RichTextBlock rtb, string html)
        {
            var blocks = HTMLHelper.ConvertHTML(html);
            rtb.Blocks.Clear();
            foreach (var block in blocks)
                rtb.Blocks.Add(block);
        }
    }
}
