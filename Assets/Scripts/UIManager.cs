using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Obsever
{
    [SerializeField] Button button1, button2, button3;
    [SerializeField] Text playerLife, enemyLife, affinityPlayer, affinityEnemy,notification;
    [SerializeField] GameObject lockPanel;
    private void Start()
    {
        
    }
    public void lockTurn()
    {
        lockPanel.SetActive(true);
    }
    public void Unlock()
    {
        lockPanel.SetActive(false);
    }

    public override void Notify()
    {
        throw new System.NotImplementedException();
    }

    public override void Register(Referee referee)
    {
        throw new System.NotImplementedException();
    }

    public override void UnRegister(Referee referee)
    {
        throw new System.NotImplementedException();
    }
}
