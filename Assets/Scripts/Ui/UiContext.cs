namespace Shooter.Ui
{
    public class UiContext
    {
        public GameHudView GameHudView { get; private set; }
        public JoinGameMenuView JoinGameMenuView { get; private set; }
        public MessageDialogView MessageDialogView { get; private set; }
        public StartMenuView StartMenuView { get; private set; }

        public UiContext(GameHudView gameHudView, JoinGameMenuView joinGameMenuView, MessageDialogView messageDialogView, StartMenuView startMenuView)
        {
            GameHudView = gameHudView;
            JoinGameMenuView = joinGameMenuView;
            MessageDialogView = messageDialogView;
            StartMenuView = startMenuView;
        }
    }
}