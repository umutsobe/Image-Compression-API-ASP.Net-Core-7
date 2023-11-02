using SixLabors.ImageSharp.Formats.Jpeg;

namespace ImageCompressApi.Util
{
    public class CompressImage
    {
        public static void Compress(
            Stream srcImgStream,
            string targetPath,
            int quality,
            int maxWidth,
            int maxHeight
        )
        {
            srcImgStream.Position = 0;

            using var image = Image.Load(srcImgStream);

            float ratioX = (float)maxWidth / (float)image.Width;
            float ratioY = (float)maxHeight / (float)image.Height;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            image.Mutate(x => x.Resize(newWidth, newHeight));
            var extension = Path.GetExtension(targetPath).ToLower();

            if (extension == ".jpg" || extension == ".jpeg")
            {
                image.Save(targetPath, new JpegEncoder { Quality = quality });
            }
            else
            {
                // Convert to JPG
                targetPath = Path.ChangeExtension(targetPath, ".jpg");
                image.Save(targetPath, new JpegEncoder { Quality = quality });
            }
        }
    }
}
