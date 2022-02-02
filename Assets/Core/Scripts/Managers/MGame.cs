using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGame : MonoBehaviour
{
    public Player player;

    public ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>();

    private bool _activeGameplay;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void Awake() => InitManager();
    
    private void Start()
    {
        gameState.Value = GameState.Game;
    }
    void InitManager() => ManageSubscriptions();

    void ManageSubscriptions()
    {
        gameState.Subscribe(OnGameStateChanged);
    }
    void OnGameStateChanged(GameState newState)
    {
        
    }
    public enum GameState {Lobby, Game, Fail}

    
    // Эта часть здесь только потому что в этом задании кастомная физика, она была написана для быстроты (спустя 3,5 часа задания)
    void Update()
    {
        if(gameState.Value != GameState.Game) return;
        if (Math.Abs(player.transform.position.y) > 4.5)
        {
            // fail
            gameState.Value = GameState.Fail;
            _disposable.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    /*
     На выполнение ушло 3 часа 52 минуты
     
     Задание:
     1) Шарик якобы двигается по коридору, но по факту это пол с потолком двигаются по экрану.
     2) В полу и потолке есть дыры. Нажатие на экран меняет гравитацию и шарик перепрыгивает с пола на потолок. Повторное нажатие возвращает его вниз.
     3) Задача игрока - не допустить, чтобы шарик провалился в дырку. 
     4) Необходим минимальный игровой интерфейс: пауза, старт, подсчет очков и пр. 
     Рандомный размер дыр и платформ. Главное, чтобы игралось, работало и не ломалось. Никакой физики и настоящей гравитации не требуется. Должен быть только работающий прототип.
     */
}
