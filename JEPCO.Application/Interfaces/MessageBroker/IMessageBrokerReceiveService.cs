using JEPCO.Application.Models.MessageBroker;

namespace JEPCO.Application.Interfaces.MessageBroker
{
    public interface IMessageBrokerReceiveService
    {
        Task ProcessMessageAsync(MBBaseMessage message);
    }
}
