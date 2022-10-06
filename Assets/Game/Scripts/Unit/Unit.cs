using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField] private Renderer  viewRenderer;
    [SerializeField] private Track     track;
    [SerializeField] private Animator  unitAnimator;
    [SerializeField] private UniInput  input;

    [FoldoutGroup("Components"), SerializeField]
    private UnitGrow growComponent;

    [FoldoutGroup("Components"), SerializeField]
    private UnitMovement movementComponent;

    [FoldoutGroup("Components"), SerializeField]
    private UnitAnimation animatorComponent;

    [FoldoutGroup("Components"), SerializeField]
    private UnitCollision collisionComponent;

    [FoldoutGroup("Components"), SerializeField]
    private UnitCamera cameraComponent;

    [FoldoutGroup("Components"), SerializeField]
    private UnitColor colorComponent;

    private int score = 0;


    public void Initialize(ColorSettings colors)
    {
        growComponent.Initialize(view);
        movementComponent.Initialize(track, input);
        colorComponent.Initialize(colors, viewRenderer);
        animatorComponent.Initialize(unitAnimator);

        Bind();
    }


    private void Bind()
    {
        movementComponent.WhileMove  += cameraComponent.UpdatePosition;
        movementComponent.OnMoveStop += OnMoveStop;
        collisionComponent.OnCollide += OnUnitCollide;

        MessageBroker.Default.Receive<Signals.LevelStarted>()
            .Subscribe(_ => Begin())
            .AddTo(this);
    }


    private void Begin()
    {
        movementComponent.MoveStart();
        animatorComponent.ToggleRunAnimation(true);
    }


    private void OnMoveStop(bool completed)
    {
        animatorComponent.ToggleRunAnimation(false);

        if (completed)
        {
            animatorComponent.PlayVictoryAnimation();
            MessageBroker.Default.Publish(new Signals.LevelCompleted());
        }
        else
        {
            MessageBroker.Default.Publish(new Signals.LevelFailed());
        }
    }


    private void OnUnitCollide(Color color)
    {
        if (colorComponent.Compare(color))
        {
            growComponent.Grow();
            score = Mathf.Clamp(score + 1, 0, int.MaxValue);
        }
        else
        {
            growComponent.Reduce(() => movementComponent.MoveStop(false));
            score = Mathf.Clamp(score - 1, 0, int.MaxValue);
        }
        
        MessageBroker.Default.Publish(new Signals.UnitScoreChanged(score));
    }
}