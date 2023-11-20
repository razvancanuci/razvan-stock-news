using Microsoft.ML.Data;

namespace backend.Application.Models;

public class PredictionModel
{
    [ColumnName("Score")]
    public float PredictionValue { get; set; }
}