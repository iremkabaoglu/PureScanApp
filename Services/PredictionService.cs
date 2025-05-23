using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Collections.Generic;
using System;
using System.Drawing;

namespace PureScanApp.Services
{
    public class PredictionService
    {
        private readonly InferenceSession _session;

        public PredictionService()
        {
            var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Services", "model.onnx");
            _session = new InferenceSession(modelPath);
        }

        public string Predict(string imagePath)
        {
            var image = new Bitmap(imagePath);
            var resized = new Bitmap(image, new Size(224, 224));
            var input = new DenseTensor<float>(new[] { 1, 224, 224, 3 });

            for (int y = 0; y < 224; y++)
            {
                for (int x = 0; x < 224; x++)
                {
                    var pixel = resized.GetPixel(x, y);
                    input[0, y, x, 0] = pixel.R / 255f;
                    input[0, y, x, 1] = pixel.G / 255f;
                    input[0, y, x, 2] = pixel.B / 255f;
                }
            }

            var inputs = new List<NamedOnnxValue>
    {
        NamedOnnxValue.CreateFromTensor("input", input)
    };

            using var results = _session.Run(inputs);
            var output = results.First().AsEnumerable<float>().ToArray();
            int maxIndex = Array.IndexOf(output, output.Max());

            var classNames = new[]
            {
        "ISANA Med",
        "Doa Nemlendirici Krem",
        "ArkoNem Nemlendirici Bakım Kremi",
        "Bepanthol",
        "Cream Co",
        "Meruderm Kil Maskesi",
        "ArkoNem Krem",
        "Mia",
        "Urban Saç Bakım Kremi",
        "Meruderm Güneş Kremi"
    };

            return classNames[maxIndex];
        }

    }
}
