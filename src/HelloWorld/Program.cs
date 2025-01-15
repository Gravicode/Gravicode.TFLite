using HelloWorld.Models;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var model = new TestModel();
            model.Initialize();
            model.Run();
            Console.ReadLine();
        }


    }

    public class TestModel
    {
        HelloWorldModel helloWorldModel { set; get; }
        public void Initialize()
        {
            Console.WriteLine("Initialize...");

            helloWorldModel = new HelloWorldModel(HelloWorldModelData.Data);

            helloWorldModel.InterferenceCount = 0;

            Console.WriteLine("Initialize complete...");

            
        }

        public void Run()
        {
            Console.WriteLine("Run...");

            bool isSuccessful = true;

            var result = helloWorldModel.GetReferenceResults();

            for (int i = 0; i < result.Length; i++)
            {
                float position = helloWorldModel.InterferenceCount / (float)helloWorldModel.InterferencesPerCycles;
                float x = position * helloWorldModel.XRange;

                sbyte xQuantized = Quantize(x);
                helloWorldModel.Inputs.SetData(new sbyte[] { xQuantized });

                var outputs = helloWorldModel.Predict();

                float y = Dequantize(outputs[0]);

                Console.WriteLine($" {i} - {(x, y)} ");

                if (!AreFloatsEqual(x, result[i].X) || !AreFloatsEqual(y, result[i].Y))
                {
                    Console.WriteLine($"{i} failed - expected {(result[i].X, result[i].Y)}");
                    isSuccessful = false;
                }

                helloWorldModel.IncrementInterferenceCount();
            }

            helloWorldModel.Dispose();

            if (isSuccessful)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }

            
        }

        private sbyte Quantize(float x)
        {
            return (sbyte)((x / helloWorldModel.InputQuantizationParams.Scale) + helloWorldModel.InputQuantizationParams.ZeroPoint);
        }

        private float Dequantize(sbyte yQuantized)
        {
            return (yQuantized - helloWorldModel.OutputQuantizationParams.ZeroPoint) * helloWorldModel.OutputQuantizationParams.Scale;
        }

        private bool AreFloatsEqual(float x, float y)
        {
            return MathF.Abs(x - y) < 1e-6;
        }
    }
}
