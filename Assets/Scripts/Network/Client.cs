using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using MessagePack;
using Shooter.Network.Messages;

namespace Shooter.Network
{
    public delegate void ChangeConnectHandler(int id);
    public delegate void ReceiveMessageHandler(int id, IMessage message);
    
    public class Client : INetwork
    {
        private readonly EventBasedNetListener _listener;
        private readonly NetManager _client;
        private NetPeer _peer;
        public event ChangeConnectHandler Connected;
        public event ChangeConnectHandler Disconnected;
        public event ReceiveMessageHandler ReceiveMessage;

        public Client()
        {
            _listener = new EventBasedNetListener();
            _client = new NetManager(_listener);
            _client.DisconnectTimeout = 20000;
        }

        private void ListenerOnNetworkErrorEvent(IPEndPoint endpoint, SocketError socketerror)
        {
            Disconnected?.Invoke(0);
        }

        private void ListenerOnPeerConnectedEvent(NetPeer peer)
        {
            Connected?.Invoke(peer.Id);
        }

        private void ListenerOnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectinfo)
        {
            Disconnected?.Invoke(peer.Id);
        }

        public void Connect(string host, int port)
        {
            _peer?.Disconnect();
            _peer = _client.Connect(host, port, "123");
        }

        public void SendMessage(int id, IMessage message)
        {
            var serializer = MessagePackSerializer.Serialize<IMessage>(message);
            _peer.Send(serializer, DeliveryMethod.ReliableOrdered);
        }

        public void Update()
        {
            _client.PollEvents();
        }
        
        public void Start()
        {
            _client.Start();
            _listener.NetworkReceiveEvent += ListenerOnNetworkReceiveEvent;
            _listener.PeerDisconnectedEvent += ListenerOnPeerDisconnectedEvent;
            _listener.PeerConnectedEvent += ListenerOnPeerConnectedEvent;
            _listener.NetworkErrorEvent += ListenerOnNetworkErrorEvent;

        }

        private void ListenerOnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliverymethod)
        {
            var message = MessagePackSerializer.Deserialize<IMessage>(reader.GetRemainingBytes());
            ReceiveMessage?.Invoke(peer.Id, message);
        }

        public void Stop()
        {
            if (_peer != null)
            {
                _client.DisconnectPeer(_peer);
            }

            _listener.NetworkReceiveEvent -= ListenerOnNetworkReceiveEvent;
            _listener.PeerDisconnectedEvent -= ListenerOnPeerDisconnectedEvent;
            _listener.PeerConnectedEvent -= ListenerOnPeerConnectedEvent;
            _listener.NetworkErrorEvent -= ListenerOnNetworkErrorEvent;
            _client.Stop();
        }
    }
}