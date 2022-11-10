using System.ComponentModel;
using Core.Input;
using UnityEngine;
using Zenject;

namespace Shooter
{
    public class Bootstrap : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            _gameManager.Start();

            // var test = _testFactory.Create(new DummyInput());
            // var test2 = _testFactory.Create(new DummySecondInput());
        }
    }
}