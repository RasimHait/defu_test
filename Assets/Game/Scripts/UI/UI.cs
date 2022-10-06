using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private Button     retryButton;
    [SerializeField] private Button     nextButton;
    [SerializeField] private Button     startButton;
    [SerializeField] private Text       counter;

    private void Awake()
    {
        OnInit();
        
        startButton.onClick.AddListener(OnBegin);
        retryButton.onClick.AddListener(() => MessageBroker.Default.Publish(new Signals.LevelReloadRequest()));
        nextButton.onClick.AddListener(() => MessageBroker.Default.Publish(new Signals.LevelReloadRequest()));

        MessageBroker.Default.Receive<Signals.LevelCompleted>()
            .Subscribe(_ => OnWin())
            .AddTo(this);
        
        MessageBroker.Default.Receive<Signals.LevelFailed>()
            .Subscribe(_ => OnFail())
            .AddTo(this);
        
        MessageBroker.Default.Receive<Signals.UnitScoreChanged>()
            .Subscribe(x => counter.text = x.currentScore.ToString())
            .AddTo(this);
    }

    private void OnInit()
    {
        menuScreen.SetActive(true);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        failScreen.SetActive(false);
    }

    private void OnBegin()
    {
        MessageBroker.Default.Publish(new Signals.LevelStarted());
        
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        winScreen.SetActive(false);
        failScreen.SetActive(false);
    }

    private void OnWin()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(true);
        failScreen.SetActive(false);
    }

    private void OnFail()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        failScreen.SetActive(true);
    }
}