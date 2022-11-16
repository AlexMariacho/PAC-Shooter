namespace Shooter.Ui.Menu.Controllers
{
    public class StartMenuController : IController
    {
        private StartMenuView _view;


        public void Activate()
        {
            _view.Show();
            _view.CreateGame += OnCreateGame;
        }

        public void Deactivate()
        {
            _view.Hide();
        }

        private void OnCreateGame()
        {
            
            Deactivate();
        }

    }
}