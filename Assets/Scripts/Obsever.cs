using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obsever : MonoBehaviour
{
    public abstract void Notify();

    public abstract void Register(Referee referee);

    public abstract void UnRegister(Referee referee);
}
