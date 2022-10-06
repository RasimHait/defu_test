using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private ColorSettings colorSettings;
    [SerializeField] private Unit          unit;
    [SerializeField] private Track         track;


    private void Start()
    {
        unit.Initialize(colorSettings);
        track.GeneratePoints(colorSettings, 3, 5, 0.3f, 3, 30, 1);

        MessageBroker.Default.Receive<Signals.LevelReloadRequest>()
            .Subscribe(_ => Reload())
            .AddTo(this);
    }


    private void Reload()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    
}