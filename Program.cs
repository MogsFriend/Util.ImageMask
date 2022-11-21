// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Drawing.Imaging;

Console.WriteLine("Hello, World!");

bool dstdir = false;
bool srcdir = false;

if (Directory.Exists("source")) srcdir = true;
if (!srcdir) Directory.CreateDirectory("source");

if (Directory.Exists("destination")) dstdir = true;
if (!dstdir) Directory.CreateDirectory("destination");

if (File.Exists("mask.png"))
{
    Bitmap maskimage = (Bitmap)Image.FromFile("mask.png");
    string[] files = Directory.GetFiles("source", "*.png");
    foreach(string file in files)
    {
        string filename = Path.GetFileName(file);
        if (!File.Exists(Path.Combine("destination", filename)))
        {
            Console.Write($"{filename} Working ...");
            Bitmap target = (Bitmap)Image.FromFile(file);

            for (int x = 0; x < maskimage.Width; x++)
            {
                for (int y = 0; y < maskimage.Height; y++)
                {
                    if (target.Width > x && target.Height > y)
                    {
                        var c = target.GetPixel(x, y);
                        target.SetPixel(x, y, Color.FromArgb(maskimage.GetPixel(x, y).A, c.R, c.G, c.B));
                    }
                }
            }
            target.Save(Path.Combine("destination", filename), ImageFormat.Png);
            Console.WriteLine($" OK");
        }
        else
        {
            Console.WriteLine($" Skip");
        }
    }
}