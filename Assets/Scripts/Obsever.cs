using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obsever : MonoBehaviour
{
    public abstract void Notify();

    public abstract void Register(Player player);

    public abstract void UnRegister(Player player);
}
