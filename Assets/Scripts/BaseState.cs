using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{

    public string stateName;

    public BaseState lastState;

    public abstract void execute();

    public abstract bool onEndState();

}
