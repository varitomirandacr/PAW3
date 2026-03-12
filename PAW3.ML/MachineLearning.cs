using Microsoft.ML;
using Microsoft.ML.Data;

namespace PAW3.ML;

public class ModelInput
{
    [LoadColumn(0)]
    public bool Label { get; set; }

    [LoadColumn(1)]
    public string Text { get; set; }
}

public class ModelOutput
{
    public string Text { get; set; }

    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Probability { get; set; }

    public float Score { get; set; }
}

public class SentimentIssue
{
    [LoadColumn(0)]
    public bool Label { get; set; }

    [LoadColumn(1)]
    public string Text { get; set; }
}

public class SentimentAnalysisResult
{
    public IEnumerable<ModelOutput> Predictions { get; set; }

    public double Accuracy { get; set; }

    public double F1Score { get; set; }

    public double Auc { get; set; }

    public string Message { get; set; }
}

public class MachineLearning
{
    public static SentimentAnalysisResult CreateSentimentAnalysis(bool doAll)
    {
        var dataPath = Path.Combine(AppContext.BaseDirectory/*Environment.CurrentDirectory*/, "data", "data.csv");

        //Step 1. Create an ML Context
        MLContext ctx = new();

        //Step 2. Read in the input data from a text file for model training
        IDataView data = ctx.Data.LoadFromTextFile<ModelInput>(dataPath, hasHeader: true, separatorChar: ',');

        //Step 3. Split dataset (80% train / 20% test)
        var splitData = ctx.Data.TrainTestSplit(data, testFraction: 0.2);

        var trainData = splitData.TrainSet;
        var testData = splitData.TestSet;

        //Step 4. Build your data processing and training pipeline
        var pipeline = ctx.Transforms.Text
            .FeaturizeText("Features", nameof(SentimentIssue.Text))
            .Append(ctx.BinaryClassification.Trainers
            .LbfgsLogisticRegression("Label", "Features"));

        //Step 5. Train model
        var model = pipeline.Fit(trainData);

        //Step 6. Evaluate model
        var predictions = model.Transform(testData);

        //Step 7. Create the metrics
        var metrics = ctx.BinaryClassification.Evaluate(predictions);

        Console.WriteLine($"Accuracy: {metrics.Accuracy}");
        Console.WriteLine($"F1Score: {metrics.F1Score}");
        Console.WriteLine($"AUC: {metrics.AreaUnderRocCurve}");

        //Step 7. Save model
        ctx.Model.Save(model, trainData.Schema, "sentiment-model.zip");

        if (doAll)
        {
            //Step 8. Test predictions (ALL DATA)
            var results = ctx.Data.CreateEnumerable<ModelOutput>(
                predictions,
                reuseRowObject: false);

            return new SentimentAnalysisResult
            {
                Predictions = [.. results],
                Accuracy = metrics.Accuracy,
                F1Score = metrics.F1Score,
                Auc = metrics.AreaUnderRocCurve,
                Message = $"Predictions for all test data. Count: {results.Count()}"
            };
        }
        else
        {
            //Step 8. Test prediction (SINGLE MODEL)
            var engine = ctx.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            var sample = new ModelInput { Text = "This is a horrible movie" };
            var prediction = engine.Predict(sample);

            return new SentimentAnalysisResult
            {
                Predictions = [prediction],
                Accuracy = metrics.Accuracy,
                F1Score = metrics.F1Score,
                Auc = metrics.AreaUnderRocCurve,
                Message = "Cannot perform metrics on single prediction"
            };
        }
    }

    public void Explanation(ModelOutput prediction)
    {
        Console.WriteLine("ML.NET Sentiment Prediction Explanation");
        Console.WriteLine("--------------------------------------------------");

        Console.WriteLine("Input Text:");
        Console.WriteLine($"\"{prediction.Text}\"");
        Console.WriteLine();

        Console.WriteLine("Prediction Result:");
        Console.WriteLine($"Prediction : {prediction.Prediction}");
        Console.WriteLine($"Probability: {prediction.Probability:F3}");
        Console.WriteLine($"Score      : {prediction.Score:F3}");
        Console.WriteLine();

        Console.WriteLine("Field Interpretation:");
        Console.WriteLine($"Prediction  -> {(prediction.Prediction ? "Positive sentiment" : "Negative sentiment")}");
        Console.WriteLine($"Probability -> {prediction.Probability:P1} chance of positive sentiment");
        Console.WriteLine($"Score       -> Raw logistic regression score before probability conversion");
        Console.WriteLine();

        Console.WriteLine("How Binary Classification Works in ML.NET");
        Console.WriteLine("true  -> Positive sentiment");
        Console.WriteLine("false -> Negative sentiment");
        Console.WriteLine();

        Console.WriteLine("Decision Threshold:");
        Console.WriteLine("Probability > 0.5  -> Positive");
        Console.WriteLine("Probability < 0.5  -> Negative");
        Console.WriteLine();

        Console.WriteLine($"Your Probability = {prediction.Probability:F3}");

        Console.WriteLine($"Therefore Prediction = {(prediction.Prediction ? "Positive" : "Negative")}");
        Console.WriteLine();

        Console.WriteLine($"Interpretation:");
        Console.WriteLine($"\"{prediction.Text}\" is predicted as {(prediction.Prediction ? "POSITIVE" : "NEGATIVE")} sentiment.");
        Console.WriteLine();

        Console.WriteLine("Score Interpretation:");
        Console.WriteLine("Positive score -> leaning positive");
        Console.WriteLine("Negative score -> leaning negative");
        Console.WriteLine("Score near 0   -> uncertain");
        Console.WriteLine();

        Console.WriteLine($"Your Score = {prediction.Score:F3}");

        if (prediction.Score < 0)
            Console.WriteLine("The model leans toward NEGATIVE sentiment.");
        else
            Console.WriteLine("The model leans toward POSITIVE sentiment.");

        Console.WriteLine();

        Console.WriteLine("Probability vs Score");
        Console.WriteLine("ML.NET converts score to probability using:");
        Console.WriteLine("Probability = 1 / (1 + e^(-Score))");
        Console.WriteLine();

        Console.WriteLine("Important Observation:");
        Console.WriteLine("If probability is close to 0.5, the model is uncertain.");
        Console.WriteLine("Lower confidence may mean:");
        Console.WriteLine("- The dataset is small");
        Console.WriteLine("- Training examples are limited");
        Console.WriteLine("- The model needs more data to learn patterns");
    }

    public void Explanation()
    {
        // MSDN
        // https://dotnet.microsoft.com/en-us/apps/ai/ml-dotnet

        // ML.NET Sentiment Prediction Explanation
        // --------------------------------------------------
        // 
        // Input Text:
        //         "This is a horrible movie"
        // 
        // Prediction Result:
        // Prediction: False
        // Probability: 0.339
        // Score: -0.666
        // 
        // Field Interpretation:
        // Prediction->Negative sentiment
        // Probability-> 33.9 % chance of positive sentiment
        // Score->Raw logistic regression score before probability conversion

        // Probability vs Score
        // ML.NET converts score to probability using:
        // Probability = 1 / (1 + e^(-Score))
        // That's why you see:
        // Score = -0.666 -> Probability = 1 / (1 + e^(0.666)) ≈ 0.339
    }
}
