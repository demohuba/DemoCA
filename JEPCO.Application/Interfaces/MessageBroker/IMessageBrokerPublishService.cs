namespace JEPCO.Application.Interfaces.MessageBroker;

public interface IMessageBrokerPublishService
{
    void Publish(string queueName, string message);
}
