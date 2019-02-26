using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(Bot bot);
    void Update();
    void Exit();
}
