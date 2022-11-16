// namespace Shooter.Ui
// {
//     public delegate void PlayerHandler(IPlayer player);
//     public class StartMenu : IController
//     {
//         private readonly StartMenuView _startMenu;
//         private readonly GameManager _gameManager;
//
//         public event PlayerHandler ReceivePlayer;
//
//         public StartMenu(GameManager gameManager)
//         {
//             _gameManager = gameManager;
//             _startMenu = _gameManager.MenuView;
//         }
//
//         private void StartMenuOnCreateGame()
//         {
//             var createGame = new CreateGame(_gameManager);
//             createGame.ClientConnected += CreateGameClientConnected;
//             createGame.Cancel += CreateGameCancel;
//             createGame.Activate();
//         }
//
//         private void StartMenuOnJoinGame()
//         {
//             var joinGame = new JoinGame(_gameManager);
//             joinGame.ClientConnected += CreateGameClientConnected;
//             joinGame.Cancel += CreateGameCancel;
//             joinGame.Activate();
//         }
//
//         private void CreateGameCancel(IController sender)
//         {
//             sender.Deactivate();
//             Detach(sender);
//         }
//
//         private void CreateGameClientConnected(IController sender, INetwork network)
//         {
//             sender.Deactivate();
//             var player = new NetworkPlayer(network);
//             player.Activate();
//             ReceivePlayer?.Invoke(player);
//             Detach(sender);
//         }
//
//         private void Detach(IController controller)
//         {
//             switch (controller)
//             {
//                 case CreateGame createGame:
//                     createGame.ClientConnected -= CreateGameClientConnected;
//                     createGame.Cancel -= CreateGameCancel;
//                     break;
//                 case JoinGame joinGame:
//                     joinGame.ClientConnected -= CreateGameClientConnected;
//                     joinGame.Cancel -= CreateGameCancel;
//                     break;
//             }
//         }
//       
//
//         private void StartMenuOnSingleGame()
//         {
//             var player = new UiPlayer(_gameManager.FieldControllerView);
//             player.Activate();
//             ReceivePlayer?.Invoke(player);
//         }
//
//         public void Activate()
//         {
//             _startMenu.Show();
//             _startMenu.SingleGame += StartMenuOnSingleGame;
//             _startMenu.JoinGame += StartMenuOnJoinGame;
//             _startMenu.CreateGame += StartMenuOnCreateGame;
//         }
//
//         public void Deactivate()
//         {
//             _startMenu.Hide();
//             _startMenu.SingleGame -= StartMenuOnSingleGame;
//             _startMenu.JoinGame -= StartMenuOnJoinGame;
//             _startMenu.CreateGame -= StartMenuOnCreateGame;
//         }
//     }
// }