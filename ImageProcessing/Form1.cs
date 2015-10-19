using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        EffectsHandler eHandler = new EffectsHandler();
       
        public Form1()
        {
            InitializeComponent();
        }

        //openFile
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image = (Bitmap)Image.FromFile(openFileDialog1.FileName);
            }
        }

        //saveFile
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Images|*.png;*.bmp;*.jpg";
            
            ImageFormat format = ImageFormat.Png;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                switch (extension)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }

                //saving 
                pictureBox3.Image.Save(saveFileDialog1.FileName, format);
            }
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = eHandler.GrayScale(pictureBox1.Image as Bitmap);
        }

       

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox3.Image = eHandler.ColorChanger(pictureBox2.Image as Bitmap, trackBar1.Value / 10.0f, trackBar2.Value / 10.0f, trackBar3.Value / 10.0f);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pictureBox3.Image = eHandler.ColorChanger(pictureBox2.Image as Bitmap, trackBar1.Value / 10.0f, trackBar2.Value / 10.0f, trackBar3.Value / 10.0f);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            pictureBox3.Image = eHandler.ColorChanger(pictureBox2.Image as Bitmap, trackBar1.Value / 10.0f, trackBar2.Value / 10.0f, trackBar3.Value / 10.0f);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //make by default maximized
            this.WindowState = FormWindowState.Maximized;
        }
    }

    class EffectsHandler
    {
     public Bitmap ColorChanger(Bitmap original, float red, float green, float blue)
        {

            if (original == null)
                throw new ArgumentException("There is not photo you want to work with");

            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new Image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the graysclare ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {

                    new float[] {red, green, blue, 0, 0},
                    new float[] {red, green, blue, 0, 0},
                    new float[] {red, green, blue, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw original image on the new image using the grayscale matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //release sources used
            g.Dispose();
            return newBitmap;
        }

     public Bitmap GrayScale(Bitmap original)
     {
         //create a blank bitmap the same size as original
         Bitmap newBitmap = new Bitmap(original.Width, original.Height);

         //get a graphics object from the new Image
         Graphics g = Graphics.FromImage(newBitmap);

         //create the graysclare ColorMatrix
         ColorMatrix colorMatrix = new ColorMatrix(
             new float[][]
                {

                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

         //create some image attributes
         ImageAttributes attributes = new ImageAttributes();

         //set the color matrix attribute
         attributes.SetColorMatrix(colorMatrix);

         //draw original image on the new image using the grayscale matrix
         g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
             0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

         //release sources used
         g.Dispose();
         return newBitmap;
     }


    }
}
