using Gravicode.TFLite;
using SkiaSharp;

namespace PersonDetector_Demo;

public class PersonDetectorModel : Model<sbyte>
{
    public enum PersonClass
    {
        NoPerson = 0,
        Person = 1
    }

    private static readonly int ArenaSize = 134 * 1024;

    public PersonDetectorModel(FileInfo modelFile)
        : base(modelFile, ArenaSize)
    {
    }

    byte[] ResizeGrayscale(string imagePath)
    {
        // Load the image from file
        using (var input = File.OpenRead(imagePath))
        using (var managedStream = new SKManagedStream(input))
        using (var codec = SKCodec.Create(managedStream))
        {
            var info = codec.Info;
            using (var bitmap = new SKBitmap(info))
            {
                codec.GetPixels(bitmap.Info, bitmap.GetPixels());

                // Resize to 96x96
                var resizedBitmap = bitmap.Resize(new SKImageInfo(96, 96), SKFilterQuality.High);
                if (resizedBitmap == null)
                {
                    Console.WriteLine("Failed to resize the image.");
                    return null;
                }

                // Convert to grayscale (BufferGray8)
                using (var grayscaleBitmap = new SKBitmap(96, 96, SKColorType.Gray8, SKAlphaType.Opaque))
                {
                    for (int y = 0; y < resizedBitmap.Height; y++)
                    {
                        for (int x = 0; x < resizedBitmap.Width; x++)
                        {
                            var color = resizedBitmap.GetPixel(x, y);
                            byte gray = (byte)(0.3 * color.Red + 0.59 * color.Green + 0.11 * color.Blue);
                            grayscaleBitmap.SetPixel(x, y, new SKColor(gray, gray, gray));
                        }
                    }

                    // Encode the grayscale bitmap to a byte array
                    using (var image = SKImage.FromBitmap(grayscaleBitmap))
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        byte[] imageBytes = data.ToArray();

                        // Do something with the byte array
                        Console.WriteLine("Grayscale image buffer length: " + imageBytes.Length);
                        return imageBytes;
                    }
                }
            }
        }
        return default;
    }
    public PersonClass Classify(string imagePath)
    {
        var resized = ResizeGrayscale(imagePath);//(imagePath.DisplayBuffer as PixelBufferBase)?.Resize<BufferGray8>(96, 96);

        var inputs = resized
            .Select(b => unchecked((sbyte)b))
            .ToArray();

        Inputs.SetData(inputs);

        var prediction = this.Predict();

        if (prediction[0] > prediction[1])
        {
            return PersonClass.NoPerson;
        }

        return PersonClass.Person;
    }
}
