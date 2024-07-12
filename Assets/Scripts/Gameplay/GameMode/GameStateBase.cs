using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Placement,
    Fighting,
    Result
}

public abstract class GameStateBase : IUpdatable
{
    public abstract GameState id { get; }

    public virtual void Init() { }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnExit() { }

}
