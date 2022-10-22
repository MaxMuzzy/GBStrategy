using Abstractions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public class GameOverPresenter : MonoBehaviour
    {
        [Inject] private IGameState _gameStatus;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _gameStatus.State.ObserveOnMainThread().Subscribe(result =>
            {
                if (result == 0)
                    _text.text = "Draw!";
                else
                    _text.text = $"Won Faction ¹{result}!";
                _view.SetActive(true);
                Time.timeScale = 0;
            });
        }
    }
}