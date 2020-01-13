using HomeAutomationService.Helpers;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeAutomationService.MachineLearning
{

    public class AutomationTrainingData
    {
        [Column(ordinal: "0")]
        public string DeviceDate;
        [Column(ordinal: "1", name: "Label")]
        public bool State;
    }

    public class AutomationPrediction
    {
       [ColumnName("PredictedLabel")]
        public bool State;
    }

    public class RoutineAI : IDisposable
    {
        private static ITransformer TrainedModel { get; set; }
        private MLContext MLContext { get; set; }
        static List<AutomationTrainingData> trainningData = new List<AutomationTrainingData>();
        public static  List<AutomationTrainingData> LoadTrainningData()
        {
            var dataList = new List<AutomationTrainingData>();

            try
            {
                using (var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "MachineLearning/Data", "data.json")))
                {
                    dataList = new NewtonsoftJsonSerializer().DeserializeFromString<List<AutomationTrainingData>>(sr.ReadToEnd());
                }
            }
            catch (Exception)
            { }

            return dataList;
        }

        public  RoutineAI()
        {
            MLContext = new MLContext();
            LoadModel(MLContext);
           //CreateModel(MLContext);
        }

        public static void LoadModel(MLContext mlContext)
        {
            using (var stream = new FileStream("MachineLearning/data/model.zip", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                TrainedModel = mlContext.Model.Load(stream);
            }
            //Console.WriteLine("Model Loaded");
        }

        public bool PredictDeviceState(string DeviceId, string Hour, string Minute) { 
            var predictionFunction = TrainedModel.MakePredictionFunction<AutomationTrainingData, AutomationPrediction>(MLContext);
            var input = new AutomationTrainingData
            {
                DeviceDate = string.Format("{0}-{1}{2}", DeviceId, Hour, Minute)
            };
            var automationPrediction = predictionFunction.Predict(input);
            //Console.WriteLine();
            
            return automationPrediction.State;
        }

        public  void CreateModel(MLContext mlContext) { 
                        
            trainningData = LoadTrainningData();
                             
            IDataView dataView = mlContext.CreateStreamingDataView(trainningData);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("DeviceDate", "Features")
                .Append(mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 500, numTrees:500, minDatapointsInLeaves: 1));

            var model = pipeline.Fit(dataView);

            using (var fs = new FileStream(@"MachineLearning/data/model.zip", FileMode.Create))
                mlContext.Model.Save(model, fs);

            Console.WriteLine("Model Build complete.");
           
        }

        public static void CreateDataFolderStructure(string folderName)
        {
            Directory.CreateDirectory(string.Format("/{0}", folderName));
        }

        public void Dispose()
        {            
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

        //private static string _trainDataPath => Path.Combine(Environment.CurrentDirectory, "MachineLearning/Data", "data - Copy (2).csv");
        //private static string _testDataPath => Path.Combine(Environment.CurrentDirectory, "MachineLearning/Data", "data - Copy (2).csv");
        //private static string _modelPath => Path.Combine(Environment.CurrentDirectory, "MachineLearning/Data", "model.zip");

        //private static MLContext _mlContext;
        //private static PredictionEngine<AutomationDataTest, IssuePrediction> _predEngine;
        //private static ITransformer _trainedModel;
        //static IDataView _trainingDataView;
        //public static void Regression()
        //{
        //    _mlContext = new MLContext(seed: 0);
        //    _trainingDataView = _mlContext.Data.LoadFromTextFile<AutomationDataTest>(_trainDataPath, hasHeader: true, separatorChar: ',');
        //    var pipeline = ProcessData();
        //    var trainingPipeline = BuildAndTrainModel(_trainingDataView, pipeline);

        //    Evaluate();
        //}

        //public static IEstimator<ITransformer> ProcessData()
        //{
        //    var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "State", outputColumnName: "Label")
        //        .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Device", outputColumnName: "deviceFeaturized"))
        //        .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "TimeOfDay", outputColumnName: "timeOfDayFeaturized"))
        //        .Append(_mlContext.Transforms.Concatenate("Features", "deviceFeaturized", "timeOfDayFeaturized"));
                
        //    return pipeline;
        //}

        //public static IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        //{
        //    var trainingPipeline = pipeline.Append(_mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeaves: 1))
        //         .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        //    _trainedModel = trainingPipeline.Fit(trainingDataView);
        //    _predEngine = _trainedModel.CreatePredictionEngine<AutomationDataTest, IssuePrediction>(_mlContext);

        //    AutomationDataTest issue = new AutomationDataTest()
        //    {
        //        Day = 6,
        //        TimeOfDay = "0910",
        //        Device = 72
        //    };

        //    var prediction = _predEngine.Predict(issue);
        //    Console.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.State} ===============");
        //    return trainingPipeline;

        //}

        //public static void Evaluate()
        //{
        //    var testDataView = _mlContext.Data.LoadFromTextFile<AutomationDataTest>(_testDataPath, hasHeader: true);
        //    var testMetrics = _mlContext.BinaryClassification.Evaluate(_trainedModel.Transform(testDataView));
        //    Console.WriteLine($"*************************************************************************************************************");
        //    Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
        //    Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
        //    Console.WriteLine($"*       MicroAccuracy:    {testMetrics.Accuracy:0.###}");
        //    Console.WriteLine($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
        //    Console.WriteLine($"*************************************************************************************************************");
        //    SaveModelAsFile(_mlContext, _trainedModel);
        //}

        //private static void SaveModelAsFile(MLContext mlContext, ITransformer model)
        //{
        //    using (var fs = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
        //        mlContext.Model.Save(model, fs);
        //    Console.WriteLine("The model is saved to {0}", _modelPath);
        //    PredictIssue();
        //}
        //private static void PredictIssue()
        //{
        //    ITransformer loadedModel;
        //    using (var stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {
        //        loadedModel = _mlContext.Model.Load(stream);
        //    }
        //    AutomationDataTest singleIssue = new AutomationDataTest() { Day = 6, TimeOfDay = "0910", Device = 73  };
        //    _predEngine = loadedModel.CreatePredictionEngine<AutomationDataTest, IssuePrediction>(_mlContext);
        //    var prediction = _predEngine.Predict(singleIssue);
        //    Console.WriteLine($"=============== Single Prediction - Result: {prediction.State} ===============");
        //}
    }

