using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static MGame;
public class MUI : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();
    
    public void ManagerInitialization(MGame game)
    {
        game.gameState.Subscribe(_ => OnGameStateChanged(_)).AddTo(_disposable);
    }

    void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Lobby:
                break;
            case GameState.Game:
                break;
            case GameState.Fail:
                break;
        }
    }

    public GameObject LobbyUI;
    public GameObject GameUI;
    public GameObject FailUI;

    private void OnDestroy() => _disposable.Clear();
}
