using SixLabors.ImageSharp.Formats.Webp;

namespace ImageCompressApi.Util
{
    public class CompressImage
    {
        public static void Compress(
            Stream srcImgStream,
            string fullFileName,
            int quality,
            int maxWidth,
            int maxHeight
        )
        {
            var extension = Path.GetExtension(fullFileName).ToLower();

            srcImgStream.Position = 0;

            using var image = Image.Load(srcImgStream);

            float ratioX = (float)maxWidth / (float)image.Width;
            float ratioY = (float)maxHeight / (float)image.Height;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            image.Mutate(x => x.Resize(newWidth, newHeight));

            if (extension == ".webp")
            {
                image.Save(fullFileName, new WebpEncoder { Quality = quality });
            }
            else
            {
                // Convert to Webp
                fullFileName = Path.ChangeExtension(fullFileName, ".webp");
                image.Save(fullFileName, new WebpEncoder { Quality = quality });
            }
        }
    }
}
