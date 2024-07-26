using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace PttCrawler
{
    public partial class ContentPopupForm : Form
    {
        private ToolTip imagePreviewToolTip;
        private string hoveredUrl;

        public ContentPopupForm(string content)
        {
            CenterToScreen();
            InitializeComponent();
            this.BackColor = Color.Black;
            richTextBoxContent.ForeColor = Color.WhiteSmoke;
            richTextBoxContent.BackColor = Color.Black;
            richTextBoxContent.Text = FormatContent(content);
            imagePreviewToolTip = new ToolTip();
        }
        private string FormatContent(string content)
        {
            // 查找“作者”的索引
            var authorIndex = content.IndexOf("作者");
            if (authorIndex >= 0)
            {
                // 提取从“作者”开始到内容结束的部分
                var contentAfterAuthor = content.Substring(authorIndex);

                // 查找标题的索引，这里假设标题以“標題”开始
                var titleIndex = content.IndexOf("標題");
                string title = string.Empty;

                if (titleIndex >= 0 && titleIndex < authorIndex)
                {
                    // 提取标题内容
                    var contentBeforeAuthor = content.Substring(0, authorIndex);
                    var titleEndIndex = contentBeforeAuthor.IndexOf(Environment.NewLine, titleIndex);
                    if (titleEndIndex == -1)
                    {
                        titleEndIndex = contentBeforeAuthor.Length;
                    }

                    // 获取标题并移除标题后的内容
                    title = contentBeforeAuthor.Substring(titleIndex, titleEndIndex - titleIndex).Trim();
                }

                // 将标题放在“作者”之前
                content = title + Environment.NewLine + contentAfterAuthor;
            }

            // 替换特定标签并移除HTML标签
            var formattedContent = content.Replace("<div class='article-metaline'>", Environment.NewLine);
            var cleanContent = RemoveHtmlTags(formattedContent);

            cleanContent = cleanContent.Trim();

            // 分割内容为行
            var lines = cleanContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var nonBlankLines = new List<string>();

            bool lastLineWasBlank = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (!lastLineWasBlank)
                    {
                        nonBlankLines.Add(string.Empty);
                        lastLineWasBlank = true;
                    }
                }
                else
                {
                    nonBlankLines.Add(line);
                    lastLineWasBlank = false;
                }
            }

            // 将行连接起来，并替换多余的空行
            var cleanedContent = string.Join(Environment.NewLine, nonBlankLines)
                .Replace($"{Environment.NewLine}{Environment.NewLine}", Environment.NewLine);

            return cleanedContent;
        }

        private bool IsImageUrl(string url)
        {
            var imageExtensions = new[] { ".jpg", ".jpeg", ".bmp", ".gif", "png", "PNG", "JPG", "JPEG" };
            return imageExtensions.Any(ext => url.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        private string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(input);
            return doc.DocumentNode.InnerText;
        }

        private void richTextBoxContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                // Check if the link is an image URL
                if (IsImageUrl(e.LinkText))
                {
                    // Show the image in a new form
                    ShowImagePopup(e.LinkText);
                }
                else if (Uri.IsWellFormedUriString(e.LinkText, UriKind.Absolute))
                {
                    // Open the URL in the default web browser
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = e.LinkText,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Invalid URL: " + e.LinkText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowImagePopup(string imageUrl)
        {
            var imageForm = new ImageForm(imageUrl);
            imageForm.Show();
        }

        private void richTextBoxContent_MouseMove(object sender, MouseEventArgs e)
        {
            int index = richTextBoxContent.GetCharIndexFromPosition(e.Location);
            string text = richTextBoxContent.Text;
            string[] lines = text.Split('\n');

            // Find the URL in the text
            string url = FindUrlAtPosition(e.Location, lines);
            if (url != null && IsImageUrl(url))
            {
                hoveredUrl = url;
                imagePreviewToolTip.SetToolTip(richTextBoxContent, string.Empty);
                imagePreviewToolTip.Show($"<img src=\"{url}\" width=\"300\" />", this, e.Location.X + 10, e.Location.Y + 10);
            }
            else
            {
                hoveredUrl = null;
                imagePreviewToolTip.Hide(richTextBoxContent);
            }
        }

        private void richTextBoxContent_MouseLeave(object sender, EventArgs e)
        {
            imagePreviewToolTip.Hide(richTextBoxContent);
        }

        private string FindUrlAtPosition(Point location, string[] lines)
        {
            int lineIndex = location.Y / richTextBoxContent.Font.Height;
            if (lineIndex >= 0 && lineIndex < lines.Length)
            {
                string line = lines[lineIndex];
                var urlPattern = @"(http[s]?://[^\s""<>]+)";
                var match = Regex.Match(line, urlPattern);
                if (match.Success)
                {
                    return match.Value;
                }
            }
            return null;
        }
    }
}
