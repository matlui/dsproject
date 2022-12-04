using dsproject.Shared;
using Dsproject_Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace dsproject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private PredictionEnginePool<MLModel.ModelInput, MLModel.ModelOutput> _predictionEnginePool;

        public PredictionController(PredictionEnginePool<MLModel.ModelInput, MLModel.ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost]
        public async Task<OutputModel> Predict(InputModel input)
        {
            MLModel.ModelInput predictionInput = new MLModel.ModelInput();

            predictionInput.Age = input.Age;
            predictionInput.Embarked_C = input.Embark == "c" ? 1 : 0;
            predictionInput.Embarked_Q = input.Embark == "q" ? 1 : 0;
            predictionInput.Embarked_S = input.Embark == "s" ? 1 : 0;
            predictionInput.SibSp = input.Sibsp;
            predictionInput.Parch = input.Parch;
            predictionInput.Fare = input.Fare;
            predictionInput.Pclass_1 = input.PassengerClass == "class1" ? 1 : 0;
            predictionInput.Pclass_2 = input.PassengerClass == "class2" ? 1 : 0;
            predictionInput.Pclass_3 = input.PassengerClass == "class3" ? 1 : 0;
            predictionInput.Sex = input.Sex == "male" ? 1 : 0;

            var r = _predictionEnginePool.Predict(predictionInput);
            var score = _predictionEnginePool.Predict(predictionInput).Probability;

            OutputModel output = new OutputModel() {
                Survive = Math.Round(score * 100)
            };

            return await Task.FromResult(output);
        }
    }
}
