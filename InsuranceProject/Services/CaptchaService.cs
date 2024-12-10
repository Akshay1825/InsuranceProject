using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

using System.Drawing.Imaging;

namespace InsuranceProject.Services
{
    public class CaptchaService
    {
        private static readonly Random _random = new Random();

        // Generates a random 6-character string
        public string GenerateCaptchaText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        // Creates an image from the CAPTCHA text
        public byte[] GenerateCaptchaImage(string captchaText)
        {
            using var bitmap = new Bitmap(150, 50);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            using var font = new System.Drawing.Font("Arial", 24, FontStyle.Bold);
            using var brush = new SolidBrush(Color.Black);

            graphics.DrawString(captchaText, font, brush, 10, 5);

            using var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
