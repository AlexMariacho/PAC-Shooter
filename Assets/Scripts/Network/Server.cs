using System.Collections.Generic;
using LiteNetLib;
using MessagePack;
using Shooter.Network.Messages;

namespace Shooter.Network
{
    public class Server : INetwork
    {
        private readonly int _port;
        private readonly NetManager _server;
        private readonly IDictionary<int, NetPeer> _toPeers = new Dictionary<int, NetPeer>();

        public event ChangeConnectHandler Connected;
        public event ChangeConnectHandler Disconnected;
        public event ReceiveMessageHandler ReceiveMessage;

        public bool _connected;

        public Server(int port)
        {
            _port = port;
            EventBasedNetListener listener = new EventBasedNetListener();
            _server = new NetManager(listener);
            _server.DisconnectTimeout = 30000;
            listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
            listener.PeerConnectedEvent += ListenerOnPeerConnectedEvent;
            listener.PeerDisconnectedEvent += ListenerOnPeerDisconnectedEvent;
            listener.ConnectionRequestEvent += Listener_ConnectionRequestEvent;
        }

        public void Start()
        {
            _server.Start(_port);
        }

        public void Stop()
        {
            _server.Stop();
        }

        private void Listener_ConnectionRequestEvent(ConnectionRequest request)
        {
            if (!_connected)
            {
                request.AcceptIfKey("123");
                _connected = true;
            }
            else
            {
                request.Reject();
            }
        }

        private void ListenerOnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectinfo)
        {
            _connected = false;
            _toPeers.Remove(peer.Id);
            Disconnected?.Invoke(peer.Id);
        }

        private void ListenerOnPeerConnectedEvent(NetPeer peer)
        {
            _toPeers.Add(peer.Id, peer);
            Connected?.Invoke(peer.Id);
        }

        private void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader netPacketReader, byte channel, DeliveryMethod deliveryMethod)
        {
            var bytes = netPacketReader.GetRemainingBytes();
            var message = MessagePackSerializer.Deserialize<IMessage>(bytes);
            ReceiveMessage?.Invoke(peer.Id, message);
        }

        public void SendMessage(int id, IMessage message)
        {
            var serializer = MessagePackSerializer.Serialize<IMessage>(message);
            if (_toPeers.TryGetValue(id, out var peer))
            {
                peer.Send(serializer, DeliveryMethod.ReliableOrdered);
            }
        }

        public void Update()
        {
            _server.PollEvents();
        }
    }
}