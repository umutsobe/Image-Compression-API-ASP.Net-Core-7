# Image-Compression-API-ASP.Net-Core-7

## How to Use?

- Clone the project to your local machine.
- Run "dotnet restore"
- Run "dotnet watch run"
- The output folder is under wwwroot/images.

Note: The System.Drawing library is now only available on Windows since .Net 7. If you want to use it on Linux, you need to use another library.

More Information: https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/7.0/system-drawing

### Image Quality Setting

You can set the quality from this line in the CompressImage class. Enter a value between 0-100.

- var parameter = new EncoderParameter(myEncoder, 90L);

### maxWidth and maxHeight Setting

- CompressImage.Compress(strm, highQualityFileUrl, 900, 900, 90);
- CompressImage.Compress(strm, thumbnailFileUrl, 300, 300, 90);

## Result

A 50 mb JPG file with a size of 15,000 x 4200 pixels and a size of 60 mb was reduced to 40 kb with the above settings. The thumbnail version was also reduced to 6 kb.
