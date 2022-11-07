using Shooter.Network.Messages;

namespace Shooter.Network
{
    public interface INetwork
    {
        event ChangeConnectHandler Connected;
        event ChangeConnectHandler Disconnected;
        event ReceiveMessageHandler ReceiveMessage;
        void SendMessage(int id, IMessage message);
        void Update();
        void Start();
        void Stop();
    }
}