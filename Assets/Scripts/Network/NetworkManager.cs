using System;
using Shooter.Network.Messages;

namespace Shooter.Network
{
    public delegate void ChangeCommandHandler(CommandType command);
    
    public class NetworkManager
    {
        private INetwork _network;
        private bool _isHost;

        public event Action Disconnected;
        public event ChangeCommandHandler ChangeCommand;

        public bool IsHost => _isHost;

        public void SetNetwork(INetwork network, bool isHost)
        {
            if (_network != null)
            {
                _network.Disconnected-=NetworkOnDisconnected;
                _network.ReceiveMessage -= NetworkOnReceiveMessage;
            }
            _network = network;
            _network.Disconnected+=NetworkOnDisconnected;
            _network.ReceiveMessage +=NetworkOnReceiveMessage;
            _isHost = isHost;
        }

        private void NetworkOnReceiveMessage(int id, IMessage message)
        {
            if (message is ChangeCommandMessage command)
            {
                ChangeCommand?.Invoke(command.Type);
            }
        }

        private void NetworkOnDisconnected(int id)
        {
            Disconnected?.Invoke();
        }

        public void Stop()
        {
            _network?.Stop();
        }

        public void Update()
        {
            _network?.Update();
        }
    }
}