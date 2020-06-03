using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{

    public abstract void execute();

    public abstract bool onEndState();

}
