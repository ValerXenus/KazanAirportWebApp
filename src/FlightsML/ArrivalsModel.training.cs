﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace FlightsML
{
    public partial class ArrivalsModel
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"dayTime", @"dayTime"),new InputOutputColumnPair(@"windSpeed", @"windSpeed"),new InputOutputColumnPair(@"visibility", @"visibility"),new InputOutputColumnPair(@"airPressure", @"airPressure"),new InputOutputColumnPair(@"temperature", @"temperature"),new InputOutputColumnPair(@"airlineId", @"airlineId"),new InputOutputColumnPair(@"cityId", @"cityId")})      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"dayTime",@"windSpeed",@"visibility",@"airPressure",@"temperature",@"airlineId",@"cityId"}))      
                                    .Append(mlContext.Regression.Trainers.FastTree(new FastTreeRegressionTrainer.Options(){NumberOfLeaves=124,MinimumExampleCountPerLeaf=3,NumberOfTrees=49,MaximumBinCountPerFeature=168,LearningRate=0.0746630967091553F,FeatureFraction=0.93768867877097F,LabelColumnName=@"delayTime",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
