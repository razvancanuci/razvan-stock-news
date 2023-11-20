namespace backend.Application.Models;

public class NewAnswerModel
{
    public Guid SubscriberId { get; set; }

    public string OccQuestion { get; set; }
    
    public int AgeQuestion { get; set; }
}