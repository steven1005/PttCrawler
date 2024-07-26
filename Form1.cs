using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Configuration;
using System.Security.Policy;

namespace PttCrawler
{
    public partial class Form1 : Form
    {
        private HttpClient httpClient;
        private Dictionary<string, List<Post>> currentPosts;
        private int itemCount = 5; // 默认项数量为5
        private int iHotBoardStartIndex = 1;
        private int iHotBoardEndIndex = 50;
        private bool bPanelMode = false; // 扁平模式
        private int iNormatWidth = 0;
        private int iNormatHeight = 0;
        bool isFirstLabel = true;


        private bool isDragging = false;
        private Point lastCursor;


        public Form1()
        {
            InitializeComponent();
            currentPosts = new Dictionary<string, List<Post>>();
            iNormatWidth = this.ClientSize.Width;
            iNormatHeight = this.ClientSize.Height;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            httpClient = new HttpClient();
            // Load URLs from application settings
            string savedUrls = ConfigurationManager.AppSettings["CustomUrls"];
            if (!string.IsNullOrWhiteSpace(savedUrls))
            {
                textBoxCustomUrls.Text = savedUrls;
            }

            // 设置 DataGridView 列
            dataGridView.Columns.Add("Board", "版面");
            dataGridView.Columns["Board"].Width = 60;

            // 设置推文数列
            dataGridView.Columns.Add("Comments", "推");
            dataGridView.Columns["Comments"].Width = 30;

            // 设置标题列宽度为400
            dataGridView.Columns.Add("Title", "标题");
            dataGridView.Columns["Title"].Width = 400;

            var linkColumn = new DataGridViewLinkColumn
            {
                Name = "Url",
                HeaderText = "链接"
            };
            dataGridView.Columns.Add(linkColumn);

            // Set timer interval to 10000 milliseconds (10 seconds)
            timer1.Interval = 10000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            FetchAndProcessData();
        }

        private async void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var results = await FetchAndProcessData();
                Invoke(new Action(() =>
                {
                    UpdateDataGridView(results);
                    UpdateStatus("Data updated successfully.");
                }));
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    UpdateStatus($"Error updating data: {ex.Message}");
                }));
            }
        }

        private async Task<List<Post>> FetchAndProcessData()
        {
            var allPosts = new List<Post>();
            string[] urls = textBoxCustomUrls.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var url in urls)
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    var latestPosts = await GetLatestPosts(url);
                    if (latestPosts != null)
                    {
                        allPosts.AddRange(latestPosts);
                    }
                }
            }

            return allPosts;
        }

        private void UpdateDataGridView(List<Post> posts)
        {
            // Locking to ensure thread safety
            lock (dataGridView)
            {
                if (bPanelMode)
                {
                    // Clear previous labels
                    plFlatMode.Controls.Clear();
                    plFlatMode.Visible = true;
                    dataGridView.Visible = false;

                    // Group posts by URL and keep only the latest posts per URL based on itemCount
                    var groupedPosts = posts
                        .GroupBy(p => p.Url)
                        .Select(g => g.OrderByDescending(p => p.Date).Take(itemCount))
                        .SelectMany(g => g);

                    int yOffset = 10;
                    int xOffset = 10;
                    int panelWidth = plFlatMode.Width;
                    int labelSpacing = 5;
                    int maxLabelHeight = 0;

                    foreach (var post in groupedPosts)
                    {
                        string boardName = GetBoardNameFromUrl(post.Url);

                        // Create label for BoardName
                        var lblBoard = new Label
                        {
                            Text = boardName,
                            ForeColor = Color.Blue,
                            AutoSize = true
                        };
                        if (isFirstLabel)
                        {
                            lblBoard.Click += (s, e) => ExitFlatMode();
                            isFirstLabel = false;
                        }
                        lblBoard.Location = new System.Drawing.Point(xOffset, yOffset);
                        plFlatMode.Controls.Add(lblBoard);

                        // Update xOffset and check if it exceeds the panel width
                        xOffset += lblBoard.Width + labelSpacing;

                        // Create label for Title
                        var lblTitle = new Label
                        {
                            Text = post.Title,
                            AutoSize = true,
                            Cursor = Cursors.Hand
                        };
                        lblTitle.Click += (s, e) => OpenContentPopupForm(post);
                        lblTitle.Location = new System.Drawing.Point(xOffset, yOffset);
                        plFlatMode.Controls.Add(lblTitle);

                        // Update xOffset and check if it exceeds the panel width
                        xOffset += lblTitle.Width + labelSpacing;

                        // Update maxLabelHeight for the current row
                        maxLabelHeight = Math.Max(maxLabelHeight, Math.Max(lblBoard.Height, lblTitle.Height));

                        if (xOffset > panelWidth - 10)
                        {
                            // Reset xOffset and move to the next row
                            xOffset = 10;
                            yOffset += maxLabelHeight + labelSpacing;
                            maxLabelHeight = 0;
                        }
                    }
                    isFirstLabel = true;
                }
                else
                {
                    // Clear previous rows
                    dataGridView.Rows.Clear();
                    plFlatMode.Visible = false;
                    dataGridView.Visible = true;

                    // Group posts by URL and keep only the latest posts per URL based on itemCount
                    var groupedPosts = posts
                        .GroupBy(p => p.Url)
                        .Select(g => g.OrderByDescending(p => p.Date).Take(itemCount))
                        .SelectMany(g => g);

                    // Add rows to DataGridView
                    foreach (var post in groupedPosts)
                    {
                        string boardName = GetBoardNameFromUrl(post.Url);
                        dataGridView.Rows.Add(boardName, post.Comments, post.Title, post.Url);
                    }
                }
            }
        }

        private void ExitFlatMode()
        {
            bPanelMode = false;

            menuStrip1.Show();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Width = iNormatWidth;
            this.Height = iNormatHeight;

            dataGridView.Visible = true;
            plFlatMode.Visible = false;

            // 計算 Form1 的位置，使其在螢幕中心
            Rectangle screenBounds = Screen.PrimaryScreen.WorkingArea;
            int x = (screenBounds.Width - this.Width) / 2;
            int y = (screenBounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }
        private async void OpenContentPopupForm(Post post)
        {
            string content = await GetPageContent(post.Url);

            // Show the content in a new form
            // 使用 `ShowDialog` 方法避免显示多个对话框
            using (var contentPopup = new ContentPopupForm(content))
            {
                contentPopup.ShowDialog();
            }
        }

        private async Task<string> GetPageContent(string url)
        {
            // Set up the HttpClient with cookies
            HttpClientHandler handler = new HttpClientHandler { UseCookies = true };
            httpClient = new HttpClient(handler);

            // Fetch the over18 check page
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();

            // Check if the over18 warning is present
            if (content.Contains("over18-notice"))
            {
                // Extract the over18 form action URL
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);
                var formNode = doc.DocumentNode.SelectSingleNode("//form[@action='/ask/over18']");
                if (formNode != null)
                {
                    string formAction = formNode.Attributes["action"].Value;
                    var postUri = new Uri(new Uri(url), formAction);

                    // Create the post request
                    var postContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("from", "/bbs"),
                        new KeyValuePair<string, string>("yes", "yes")
                    });

                    // Send the post request to bypass the over18 check
                    HttpResponseMessage postResponse = await httpClient.PostAsync(postUri, postContent);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        // Fetch the actual page content after bypassing the over18 check
                        response = await httpClient.GetAsync(url);
                        content = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return content;
        }

        private async Task<List<Post>> GetLatestPosts(string url)
        {
            var latestPosts = new List<Post>();
            int retryCount = 0; // 設置重試計數
            const int maxRetries = 1; // 設置最大重試次數

            try
            {
                while (retryCount <= maxRetries)
                {
                    // 繞過 over18 檢查後獲取頁面內容
                    string content = await GetPageContent(url);
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(content);

                    // 根據 URL 確定 XPath
                    string xpathQuery = "//div[@class='r-ent']";

                    var posts = doc.DocumentNode.SelectNodes(xpathQuery);

                    if (posts == null)
                    {
                        retryCount++;
                        if (retryCount > maxRetries)
                        {
                            UpdateStatus($"No posts found after {retryCount} attempts. {url}.");
                            return latestPosts;
                        }
                        else
                        {
                            // 紀錄重試日誌
                            UpdateStatus($"Retrying to fetch posts... Attempt {retryCount}.");
                            continue;
                        }
                    }

                    foreach (var post in posts)
                    {
                        var dateNode = post.SelectSingleNode(".//div[@class='date']");
                        var titleNode = post.SelectSingleNode(".//div[@class='title']/a");
                        var commentsNode = post.SelectSingleNode(".//div[@class='nrec']");

                        if (dateNode != null && titleNode != null)
                        {
                            string postDate = dateNode.InnerText.Trim();
                            if (IsToday(postDate))
                            {
                                string postUrl = "https://www.ptt.cc" + titleNode.Attributes["href"].Value;
                                string postTitle = titleNode.InnerText.Trim();
                                string postComments = commentsNode?.InnerText.Trim() ?? "0"; // 默認為 "0" 如果 commentsNode 為空
                                if (!postTitle.Contains("公告"))
                                {
                                    latestPosts.Add(new Post
                                    {
                                        Title = postTitle,
                                        Url = postUrl,
                                        Comments = postComments,
                                        Date = DateTime.Now // 替換為實際日期提取（如果需要）
                                    });
                                }
                            }
                        }
                    }

                    return latestPosts.OrderByDescending(post => post.Date).Take(itemCount).ToList();
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error fetching data from {url}: {ex.Message}");
            }

            return latestPosts;
        }

        private void SaveUrls()
        {
            // Get the URLs from the text box
            string[] urls = textBoxCustomUrls.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Join URLs into a single string, separated by new lines
            string urlList = string.Join(Environment.NewLine, urls);

            // Save to application settings
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["CustomUrls"].Value = urlList;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private bool IsToday(string postDate)
        {
            DateTime date;
            // Adjust the date format according to the format used on PTT
            if (DateTime.TryParseExact(postDate, "M/d", null, System.Globalization.DateTimeStyles.None, out date))
            {
                return date.Date == DateTime.Today;
            }
            return false;
        }

        private string GetBoardNameFromUrl(string url)
        {
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split('/');
            if (pathSegments.Length > 2)
            {
                return pathSegments[2];
            }
            return "Unknown";
        }

        private async void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保点击的是 'Title' 列
            if (e.ColumnIndex == dataGridView.Columns["Title"].Index && e.RowIndex >= 0)
            {
                string url = dataGridView.Rows[e.RowIndex].Cells["Url"].Value.ToString();
                string content = await GetPageContent(url);

                // Show the content in a new form
                // 使用 `ShowDialog` 方法避免显示多个对话框
                using (var contentPopup = new ContentPopupForm(content))
                {
                    contentPopup.ShowDialog();
                }
            }
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell != null && dataGridView.CurrentCell.ColumnIndex == dataGridView.Columns["Title"].Index)
            {
                string url = dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["Url"].Value.ToString();
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Unable to open link: {ex.Message}");
                }
            }
        }

        private void UpdateStatus(string message)
        {
            toolStripStatusLabel1.Text = message;
        }

        private void btnSaveUrl_Click(object sender, EventArgs e)
        {
            SaveUrls();
            UpdateStatus("URLs saved successfully.");
        }

        private void tsmi_count_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Checked)
            {
                itemCount = Convert.ToInt16(tsmi.Text);
            }
        }

        private async void tsmi_board_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Text.StartsWith("自訂"))
            {
                string savedUrls = ConfigurationManager.AppSettings["CustomUrls"];
                if (!string.IsNullOrWhiteSpace(savedUrls))
                {
                    textBoxCustomUrls.Text = savedUrls;
                    UpdateStatus("Custom URLs loaded.");
                }
                else
                {
                    UpdateStatus("No custom URLs found in configuration.");
                }
            }

            if (tsmi.Text.StartsWith("熱門"))
            {
                try
                {
                    var hotBoards = await GetHotBoards(1, 50);
                    textBoxCustomUrls.Text = string.Join(Environment.NewLine, hotBoards.Select(board => $"https://www.ptt.cc/bbs/{board}/index.html"));
                    UpdateStatus("Hot boards URLs loaded.");
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error loading hot boards: {ex.Message}");
                }
            }
        }

        private async Task<List<string>> GetHotBoards(int start, int end)
        {
            var hotBoards = new List<string>();
            try
            {
                string url = "https://www.ptt.cc/bbs/hotboards.html";
                string content = await httpClient.GetStringAsync(url);
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(content);

                var nodes = doc.DocumentNode.SelectNodes("//div[@class='board-name']");
                if (nodes != null)
                {
                    hotBoards = nodes.Skip(start - 1).Take(end - start + 1).Select(node => node.InnerText.Trim()).ToList();
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error fetching hot boards: {ex.Message}");
            }

            return hotBoards;
        }

        private async void hotBoardCount_Click(object sender, EventArgs e)
        {
            iHotBoardStartIndex = 1;
            iHotBoardEndIndex = 50;
            try
            {
                var hotBoards = await GetHotBoards(iHotBoardStartIndex, iHotBoardEndIndex);
                textBoxCustomUrls.Text = string.Join(Environment.NewLine, hotBoards.Select(board => $"https://www.ptt.cc/bbs/{board}/index.html"));
                UpdateStatus($"Hot boards URLs loaded (Range: {iHotBoardStartIndex}-{iHotBoardEndIndex}).");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error loading hot boards (Range: {iHotBoardStartIndex}-{iHotBoardEndIndex}): {ex.Message}");
            }
        }

        private void TogglePanelMode()
        {
            bPanelMode = !bPanelMode;

            if (bPanelMode)
            {
                dataGridView.Visible = false;
                plFlatMode.Visible = true;
                menuStrip1.Hide();
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = 220;
                this.Text = ""; // Hide the title bar text
            }
            else
            {
                dataGridView.Visible = true;
                plFlatMode.Visible = false;
                menuStrip1.Show();
                this.Width = iNormatWidth;
                this.Height = iNormatHeight;
                this.Text = "Ptt Crawler"; // Restore the title bar text
            }

            UpdateDataGridView(currentPosts.Values.SelectMany(list => list).ToList()); // Refresh data
        }
        private void tsmi_model_Click(object sender, EventArgs e)
        {
            bPanelMode = false;

            dataGridView.Visible = !bPanelMode;
            plFlatMode.Visible = bPanelMode;

            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Text.StartsWith("扁平"))
            {
                bPanelMode = true;

                dataGridView.Visible = !bPanelMode;
                plFlatMode.Visible = bPanelMode;

                menuStrip1.Hide();
                this.FormBorderStyle = FormBorderStyle.None;
                this.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height - 180);
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                if (this.Width > 2220)
                    this.Width = 2220;
                this.Height = 180;
            }
            else
            {
                menuStrip1.Show();
                this.Width = iNormatWidth;
                this.Height = iNormatHeight;
            }
        }

        private void plFlatMode_MouseDown(object sender, MouseEventArgs e)
        {
            if (!bPanelMode) return;
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
            }
        }

        private void plFlatMode_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bPanelMode) return;
            if (isDragging)
            {
                Point newCursor = Cursor.Position;
                this.Location = new Point(this.Location.X + (newCursor.X - lastCursor.X), this.Location.Y + (newCursor.Y - lastCursor.Y));
                lastCursor = newCursor;
            }
        }

        private void plFlatMode_MouseUp(object sender, MouseEventArgs e)
        {
            if (!bPanelMode) return;
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void UpdataContentMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Text.StartsWith("10"))
            {
                timer1.Interval = 10000;
            }
            if (tsmi.Text.StartsWith("20"))
            {
                timer1.Interval = 20000;
            }
            if (tsmi.Text.StartsWith("30"))
            {
                timer1.Interval = 30000;
            }
        }
    }

    public class Post
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Comments { get; set; } // Number of comments
        public DateTime Date { get; set; } // Article publication date
    }
}
