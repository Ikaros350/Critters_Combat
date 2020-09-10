using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Obsever
{
    [SerializeField] Button[] buttons;
    [SerializeField] Text playerCritters, enemyCritters, affinityPlayer, affinityEnemy,notification;
    [SerializeField] GameObject lockPanel;
    
    private int countEnemy;
    private int countPlayer;
    public void lockTurn()
    {
        lockPanel.SetActive(true);
    }
    public void UnlockTurn()
    {
        lockPanel.SetActive(false);
    }
    private void Start()
    {
        countEnemy = Referee.instance.Enemy.Critters.Count;
        countPlayer = Referee.instance.Player.Critters.Count;
        enemyCritters.text = countEnemy.ToString();
        playerCritters.text = countPlayer.ToString();
        
    }
    public void UpdateButtons()
    {
        for (int i = 0; i < Referee.instance.CurrentPlayerC.Moveset.Count; i++)
        {
            Debug.Log("Los botones cambiaran");
            buttons[i].interactable = true;
            buttons[i].GetComponentInChildren<Text>().text = Referee.instance.CurrentPlayerC.Moveset[i].Name;
        }
    }
    private void DowndateButtons()
    {
        for (int i = 0; i < Referee.instance.CurrentPlayerC.Moveset.Count; i++)
        {
            Debug.Log("Los se borarran");
            buttons[i].interactable = false;
            buttons[i].GetComponentInChildren<Text>().text = "";
        }
    }
    public override void Notify()
    {
        if(Referee.instance.CurrentEnemyC.Hp <= 0) 
        {
            countEnemy -= 1;
        } 
        if(Referee.instance.CurrentPlayerC.Hp <= 0)
        {
            countPlayer -= 1;
            DowndateButtons();
            //UpdateButtons();
        } 
        UpdateCountCritters();
    }
    private void UpdateCountCritters()
    {
       if(enemyCritters != null)
        {
            enemyCritters.text = countEnemy.ToString();
            playerCritters.text = countPlayer.ToString();
        }
    }

    public override void Register(Referee referee)
    {
        Referee.wonPLayer += Notify;
    }

    public override void UnRegister(Referee referee)
    {
        Referee.wonPLayer -= Notify;
    }

    private void Update()
    {
    //    if (!Referee.instance.TurnStart)
    //        lockTurn();
    //    else
    //        UnlockTurn();
    }
}
