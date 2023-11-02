using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageCompressApi.Util;

public class CompressImage
{
    public static void Compress(Stream srcImgStream, string targetPath, int maxWidth, int maxHeight)
    {
        using var image = Image.FromStream(srcImgStream);

        float ratioX = (float)maxWidth / (float)image.Width;
        float ratioY = (float)maxHeight / (float)image.Height;
        float ratio = Math.Min(ratioX, ratioY);

        int newWidth = (int)(image.Width * ratio);
        int newHeight = (int)(image.Height * ratio);

        var bitmap = new Bitmap(image, newWidth, newHeight);

        var imgGraph = Graphics.FromImage(bitmap);
        imgGraph.SmoothingMode = SmoothingMode.Default;
        imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

        imgGraph.DrawImage(image, 0, 0, newWidth, newHeight);

        var extension = Path.GetExtension(targetPath).ToLower();

        if (extension == ".png" || extension == ".gif")
        {
            bitmap.Save(targetPath, image.RawFormat);
        }
        else if (extension == ".jpg" || extension == ".jpeg")
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            Encoder myEncoder = Encoder.Quality;
            var encoderParameters = new EncoderParameters(1);
            var parameter = new EncoderParameter(myEncoder, 90L);
            encoderParameters.Param[0] = parameter;

            bitmap.Save(targetPath, ImageFormat.Jpeg);
        }
        else if (extension == ".webp")
        {
            ImageCodecInfo webpEncoder = GetEncoder(ImageFormat.Webp);

            Encoder myEncoder = Encoder.Quality;
            var encoderParameters = new EncoderParameters(1);
            var parameter = new EncoderParameter(myEncoder, 90L);
            encoderParameters.Param[0] = parameter;

            bitmap.Save(targetPath, ImageFormat.Webp);
        }

        bitmap.Dispose();
        imgGraph.Dispose();
        image.Dispose();
    }

    public static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }

        return null;
    }
}
