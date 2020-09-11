using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UIManager : Obsever
{
    [SerializeField] Button[] buttons;
    [SerializeField] Text playerCritters, enemyCritters, affinityPlayer, affinityEnemy,notification,PlayerHp,EnemyHp;
    [SerializeField] GameObject lockPanel,win,lose;
    StringBuilder conditionString = new StringBuilder();
    
    private int countEnemy;
    private int countPlayer;
    public void lockTurn()
    {
        lockPanel.SetActive(true);
        lockPanel.GetComponentInChildren<Text>().text = conditionString.ToString();
                                                        
    }

    public void ConditionUpdate(string newCondition)
    {
        conditionString.Append(newCondition + "\n");
        
    }

    public void UnlockTurn()
    {
        lockPanel.SetActive(false);
        conditionString.Clear();
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
       
            for (int i = 0; i < 3; i++)
            {
                
                buttons[i].interactable = true;
            if (Referee.instance.CurrentPlayerC.Moveset[i] is AttackSkill)
            {
                AttackSkill placeholder = Referee.instance.CurrentPlayerC.Moveset[i] as AttackSkill;
                buttons[i].GetComponentInChildren<Text>().text = (placeholder.Name + "\n" + placeholder.MyAffinity).ToString(); 
            }
            else if(Referee.instance.CurrentPlayerC.Moveset[i] is SupportSkill)
                buttons[i].GetComponentInChildren<Text>().text = Referee.instance.CurrentPlayerC.Moveset[i].Name.ToString();


        } 
           
        
    }
    
    public override void Notify()
    {
        if(Referee.instance.CurrentEnemyC.Hp <= 0) 
        {
            countEnemy -= 1;
            if (countEnemy == 0)
                win.SetActive(true);
           
        } 
        if(Referee.instance.CurrentPlayerC.Hp <= 0)
        {
            countPlayer -= 1;
            if (countPlayer == 0)
                lose.SetActive(true);

            
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
        if (!Referee.instance.TurnStart)
            lockTurn();
        else
            UnlockTurn();
        EnemyHp.text = Referee.instance.CurrentEnemyC.Hp.ToString();
        PlayerHp.text = Referee.instance.CurrentPlayerC.Hp.ToString();
        affinityEnemy.text = Referee.instance.CurrentEnemyC.Affinity.ToString();
        affinityPlayer.text = Referee.instance.CurrentPlayerC.Affinity.ToString();


    }
}
