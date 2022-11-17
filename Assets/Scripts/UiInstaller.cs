using Shooter.Ui;
using UnityEngine;
using Zenject;

namespace Shooter.Root
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private GameHudView _gameHudView;
        [SerializeField] private JoinGameMenuView _joinGameMenuView;
        [SerializeField] private MessageDialogView _messageDialogView;
        [SerializeField] private StartMenuView _startMenuView;
        
        public override void InstallBindings()
        {
            UiContext context = new UiContext(_gameHudView, _joinGameMenuView, _messageDialogView, _startMenuView);
            Container.Bind<UiContext>().FromInstance(context).AsSingle().NonLazy();
        }
    }
}