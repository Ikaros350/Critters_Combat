using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee instance;

    [SerializeField]private EnemyLogic enemy;

    [SerializeField]private Player player;
    private bool turnStart;
    [SerializeField]private Critter currentPlayerC, currentEnemyC;
    Affinity affinityTable = new Affinity();

    public static Referee Instance { get => instance; }


    private void Awake()
    {
        turnStart = true;
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
        player = FindObjectOfType<Player>();
        //player = GetComponent<Player>();

        enemy = FindObjectOfType<EnemyLogic>();
        //enemy = GetComponent<EnemyLogic>();
    }
    void Start()
    {


        ChangeCritter();
    }
    public void ChangeCritter()
    {
        if (player.Critters.Count  >= 1 && enemy.Critters.Count >= 1)
        {
            currentPlayerC = player.Critters[0];
            currentEnemyC = enemy.Critters[0];


            Debug.Log("criter del jugado canntidad de skills: " + currentPlayerC.Moveset.Count);
            Debug.Log("criter del enemigo canntidad de skills: " + currentEnemyC.Moveset.Count);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ValWin();
          
        }
    }
    public void Action(int skill)
    {
        float timeCounter = 0;
         turnStart = true;

        if (turnStart)
        {
            //caso jugador mas rapido que enemigo
            if (currentPlayerC.BaseSpeed > currentEnemyC.BaseSpeed)
            {

                PlayerTurn(skill);
                timeCounter += 1f * Time.deltaTime;

               
                 EnemyTurn();
               
                turnStart = false;




            }

            //caso enemigo mas rapido que un enemigo
            if (currentPlayerC.BaseSpeed < currentEnemyC.BaseSpeed)
            {
                EnemyTurn();
                timeCounter += 1f * Time.deltaTime;

                
                PlayerTurn(skill);

                turnStart = false;
               

            }

            //caso velocidades iguales
            if (currentPlayerC.BaseSpeed == currentEnemyC.BaseSpeed)
            {
                System.Random coinRandom = new System.Random();

                int coin = coinRandom.Next(0, 2);
               
                if (coin == 1)
                {
                    EnemyTurn();
                    timeCounter += 1f * Time.deltaTime;

                    
                    PlayerTurn(skill);

                    turnStart = false; 
                }

                if (coin == 0)
                {
                    PlayerTurn(skill);
                    timeCounter += 1f * Time.deltaTime;

                    
                    EnemyTurn();

                    turnStart = false; 
                }
            }
        }

        Debug.Log(timeCounter);
    }
    void ValWin()
    {
        if (player.Critters.Count != 0 && enemy.Critters.Count != 0)
        {
            
            if (currentPlayerC.Hp <= 0)
            {
                enemy.AddCritters(currentPlayerC);
                player.LoseCritter(currentPlayerC);

                ChangeCritter();
            }

            if (currentEnemyC.Hp <= 0)
            {
                player.AddCritters(currentEnemyC);
                enemy.LoseCritter(currentEnemyC);
                ChangeCritter();
            }
        }
     


    }
    void PlayerTurn(int skill)
    {
        //Turno del jugador
        if (currentPlayerC.Moveset[skill] is AttackSkill)
        {
            AttackSkill placeholderSkill = currentPlayerC.Moveset[skill] as AttackSkill;
            float multipler = affinityTable.AfinityTable(placeholderSkill.MyAffinity, currentEnemyC.Affinity);
            currentEnemyC.OnHit(currentPlayerC.CurrentAtq, placeholderSkill.Power, multipler);
            ValWin();
        }
        else if (currentPlayerC.Moveset[skill] is SupportSkill)
        {
            SupportSkill placeholderSkill = currentPlayerC.Moveset[skill] as SupportSkill;
            switch (placeholderSkill.Name)
            {
                case "AtkUp":
                    currentPlayerC.AlterState(0);
                    break;
                case "DefUp":
                    currentPlayerC.AlterState(1);
                    break;
                case "SpdDwn":
                    currentEnemyC.AlterState(2);
                    break;
            }
        }
    }
    void EnemyTurn()
    {
        //Turno del enemigo
        if (enemy.MadeAction() is AttackSkill)
        {
            AttackSkill placeholderSkill = enemy.MadeAction() as AttackSkill;
            float multipler = affinityTable.AfinityTable(placeholderSkill.MyAffinity, currentPlayerC.Affinity);
            currentPlayerC.OnHit(currentEnemyC.CurrentAtq, placeholderSkill.Power, multipler);
            ValWin();
        }
        else if (enemy.MadeAction() is SupportSkill)
        {
            SupportSkill placeholderSkill = enemy.MadeAction() as SupportSkill;
            switch (placeholderSkill.Name)
            {
                case "AtkUp":
                    currentEnemyC.AlterState(0);
                    break;
                case "DefUp":
                    currentEnemyC.AlterState(1);
                    break;
                case "SpdDwn":
                    currentPlayerC.AlterState(2);
                    break;
            }
        }
    }
}
