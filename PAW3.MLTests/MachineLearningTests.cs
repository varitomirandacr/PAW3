namespace PAW3.ML.Tests;

[TestClass()]
public class MachineLearningTests
{
    [TestMethod()]
    public void SentimentAnalysisTest()
    {
        var single = MachineLearning.CreateSentimentAnalysis(doAll: false);
        Assert.IsTrue(single.Predictions.Count() == 1);
        Assert.IsTrue(single.Accuracy > 0);
        Assert.IsTrue(single.F1Score > 0);
        Assert.IsTrue(single.Auc > 0);
        Assert.IsFalse(string.IsNullOrEmpty(single.Message));
        Assert.IsTrue(single.Message.Contains("Cannot perform metrics on single prediction"));

        var items = MachineLearning.CreateSentimentAnalysis(doAll: true);
        Assert.IsTrue(items.Predictions.Count() > 1);
        Assert.IsTrue(items.Accuracy > 0);
        Assert.IsTrue(items.F1Score > 0);
        Assert.IsTrue(items.Auc > 0);
        Assert.IsFalse(string.IsNullOrEmpty(items.Message));
        Assert.IsTrue(items.Message.Contains("Predictions for all test data."));
    }
}