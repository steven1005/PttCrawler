using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PttCrawler
{
    public partial class ImageForm : Form
    {
        private string imageUrl;
        private const int Padding = 20; // Padding around the image

        public ImageForm(string url)
        {
            CenterToScreen();
            InitializeComponent();
            imageUrl = url;
            LoadImage();
            this.BackColor = Color.Black;
        }

        private void LoadImage()
        {
            try
            {
                // Load the image from the URL
                var request = WebRequest.Create(imageUrl);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                var image = Image.FromStream(stream);

                // Calculate dimensions while maintaining aspect ratio
                int pictureBoxWidth = Math.Min(image.Width, this.ClientSize.Width - Padding);
                int pictureBoxHeight = Math.Min(image.Height, this.ClientSize.Height - Padding);

                // Adjust the PictureBox size
                pictureBox.Size = new Size(pictureBoxWidth, pictureBoxHeight);

                // Ensure PictureBox size is not larger than the image size
                if (pictureBoxWidth > image.Width)
                {
                    pictureBoxWidth = image.Width;
                }
                if (pictureBoxHeight > image.Height)
                {
                    pictureBoxHeight = image.Height;
                }

                // Resize the PictureBox to fit the image with padding
                this.ClientSize = new Size(pictureBoxWidth + Padding, pictureBoxHeight + Padding + 40); // Adding extra space for form borders and title bar

                // Set the image to the PictureBox and resize the PictureBox to fit the image
                pictureBox.Image = image;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // Ensure image scales to fit PictureBox
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
