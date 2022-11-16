// namespace Shooter.Ui
// {
//     public delegate void NetworkHandler(IController sender, INetwork network);
//     public delegate void CancelHandler(IController sender);
//     public class CreateGame : IController
//     {
//         private readonly CreateGameMenuView _createGameMenuView;
//         private readonly GameManager _gameManager;
//         private Server _server;
//
//         public event NetworkHandler ClientConnected;
//         public event CancelHandler Cancel;
//
//         public CreateGame(GameManager gameManager)
//         {
//             _gameManager = gameManager;
//             _createGameMenuView = _gameManager.CreateGameMenuView;
//             _gameManager.NetworkManager.Stop();
//             _server = new Server(8785);
//         }
//
//         private void ServerOnConnected(int id)
//         {
//             ClientConnected?.Invoke(this, _server);
//             Deactivate();
//         }
//
//         private void CreateGameMenuViewOnCancel()
//         {
//             Cancel?.Invoke(this);
//             Deactivate();
//         }
//
//         public void Activate()
//         {
//             _gameManager.NetworkManager.Stop();
//             _server.Connected += ServerOnConnected;
//             _createGameMenuView.Cancel+=CreateGameMenuViewOnCancel;
//             _createGameMenuView.Show();
//             _server.Start();
//             _gameManager.NetworkManager.SetNetwork(_server, true);
//         }
//
//         public void Deactivate()
//         {
//             _server.Connected -= ServerOnConnected;
//             _createGameMenuView.Cancel -= CreateGameMenuViewOnCancel;
//             _createGameMenuView.Hide();
//         }
//     }
// }