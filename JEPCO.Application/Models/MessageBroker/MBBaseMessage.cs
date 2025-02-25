namespace JEPCO.Application.Models.MessageBroker;

/// <summary>
/// Message Broker Base Message
/// </summary>
public class MBBaseMessage
{
    public int Type { get; set; }
    public string Body { get; set; }
}



// For business message broker models, each business module will have its own message broker business models.
// these models will have a suffix of "MBModel"
