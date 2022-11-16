// namespace Shooter.Ui
// {
//     public class JoinGame : IController
//     {
//         private readonly GameManager _gameManager;
//         private JoinGameMenuView _view;
//         private Client _client;
//
//         public event NetworkHandler ClientConnected;
//         public event CancelHandler Cancel;
//
//         public JoinGame(GameManager gameManager)
//         {
//             _gameManager = gameManager;
//             _view = _gameManager.JoinGameMenuView;
//         }
//
//         public void Activate()
//         {
//             _view.Join += ViewOnJoin;
//             _view.Cancel += ViewOnCancel;
//             _view.Show();
//         }
//
//         private void ViewOnCancel()
//         {
//             Cancel?.Invoke(this);
//         }
//
//         private void ViewOnJoin(string ipAddress)
//         {
//             _gameManager.NetworkManager.Stop();
//           
//             _client = new Client();
//             _client.Connected += ClientOnConnected;
//             _client.Disconnected += ClientOnDisconnected;
//             _client.Start();
//             _client.Connect(ipAddress, 8785);
//             _gameManager.NetworkManager.SetNetwork(_client, false);
//         }
//
//         private void ClientOnDisconnected(int id)
//         {
//             _client.Connected -= ClientOnConnected;
//             _client.Disconnected -= ClientOnDisconnected;
//             var message = _gameManager.MessageDialogView;
//             message.InitMessage("Ошибка", "Невозможно соединиться с сервером", false);
//             message.Ok += MessageOnOk;
//             message.Show();
//         }
//
//         private void MessageOnOk(MessageDialogView sender)
//         {
//             sender.Ok -= MessageOnOk;
//             sender.Hide();
//         }
//
//         private void ClientOnConnected(int id)
//         {
//            
//             _client.Connected -= ClientOnConnected;
//             _client.Disconnected -= ClientOnDisconnected;
//             ClientConnected?.Invoke(this,_client);
//         }
//
//         public void Deactivate()
//         {
//             _view.Join -= ViewOnJoin;
//             _view.Cancel -= ViewOnCancel;
//             _view.Hide();
//         }
//     }
// }