using PersonDetector_Demo;
using static System.Net.Mime.MediaTypeNames;

namespace PersonDetector
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var test = new DetectorTest();
            test.Initialize();
            test.Run();
            Console.Read();
        }
    }

    public class DetectorTest
    {
        private PersonDetectorModel model;

        public void Initialize()
        {
            Console.WriteLine("Loading model...");

            var modelFile = new FileInfo("person_detect.tflite");
            model = new PersonDetectorModel(modelFile);

            
        }

        public void Run()
        {
            Console.WriteLine("Run...");

            var tests = new string[]
                {
                "person.bmp",
                "no_person.bmp",
                };

            foreach (var test in tests)
            {
                Console.WriteLine($"loading {test}...");
                //var img = Image.LoadFromFile(test);

                var result = model.Classify(test);
                Console.WriteLine($"this image is {result}");
            }

            
        }

    }
}
