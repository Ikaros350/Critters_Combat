using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee instance;

    [SerializeField]private EnemyLogic enemy;
    UIManager myUImanager;
    [SerializeField]private Player player;
    [SerializeField] private bool turnStart;
    [SerializeField]private Critter currentPlayerC, currentEnemyC;

    Affinity affinityTable = new Affinity();

    [SerializeField] int CRITTERS_ENEMY_COUNT;

    public delegate void WonPlayer();

    public static event WonPlayer wonPLayer;
    public static Referee Instance { get => instance; }
    public EnemyLogic Enemy { get => enemy;}
    public Player Player { get => player;}
    public Critter CurrentPlayerC { get => currentPlayerC;}
    public Critter CurrentEnemyC { get => currentEnemyC; }
    public bool TurnStart { get => turnStart; private set => turnStart = value; }

    [SerializeField]private Obsever[] observers;

    private int currentCrittersCap;

    private bool hasResgisteredObservers;
    private void Awake()
    {
        TurnStart = true;
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<EnemyLogic>();

        myUImanager = FindObjectOfType<UIManager>();
    }
    void Start()
    {
        CRITTERS_ENEMY_COUNT = enemy.Critters.Count;
        
        RegisterObservers();
        if (hasResgisteredObservers)
        {
            currentCrittersCap =0;
            //NotifyObservers();
        }
        ChangeCritter();
    }
    private void RegisterObservers()
    {
        foreach(Obsever observer in observers)
        {
            observer.Register(this);
        }
        hasResgisteredObservers = true;
    }
    private void UnregisterObservers()
    {
        hasResgisteredObservers = false;
        foreach(Obsever observer in observers)
        {
            observer.UnRegister(this);
        }
    }
    private void NotifyObservers()
    {
        if(wonPLayer != null)
        {
            wonPLayer();
        }
    }
    public void ChangeCritter()
    {
        if (player.Critters.Count  >= 1 && enemy.Critters.Count >= 1)
        {
            currentPlayerC = player.Critters[0];
            currentEnemyC = enemy.Critters[0];

            myUImanager.UpdateButtons();
           
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
   

        if (TurnStart)
        {
            TurnStart = false;
            //caso jugador mas rapido que enemigo
            if (currentPlayerC.BaseSpeed > currentEnemyC.BaseSpeed)
            {
                PlayerTurn(skill);
                timeCounter += 1f * Time.deltaTime;

               if(currentEnemyC.Hp>0)
                EnemyTurn();

            }

            //caso enemigo mas rapido que un enemigo
            if (currentPlayerC.BaseSpeed < currentEnemyC.BaseSpeed)
            {
                EnemyTurn();
                timeCounter += 1f * Time.deltaTime;

                if (currentPlayerC.Hp > 0)
                    PlayerTurn(skill);

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

                    if (currentPlayerC.Hp > 0)
                        PlayerTurn(skill);

                }

                if (coin == 0)
                {
                    PlayerTurn(skill);
                    timeCounter += 1f * Time.deltaTime;

                    if (currentEnemyC.Hp > 0)
                        EnemyTurn();
                }
            }

            Invoke("StarTurn", 0.2f);
        }
       

    }
    void StarTurn()
    {
        TurnStart = true;
    }
    void ValWin()
    {
        if (player.Critters.Count != 0 && enemy.Critters.Count != 0)
        {
            
            if (currentPlayerC.Hp <= 0)
            {
                enemy.AddCritters(currentPlayerC);
                player.LoseCritter(currentPlayerC); 
                
                if (hasResgisteredObservers)
                {
                    NotifyObservers();
                }
                ChangeCritter();
            }

            if (currentEnemyC.Hp <= 0)
            {
                player.AddCritters(currentEnemyC);
                enemy.LoseCritter(currentEnemyC);
                if (hasResgisteredObservers)
                {
                    NotifyObservers();
                }
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
            Debug.Log("jUGADOR affinity skill" + placeholderSkill.MyAffinity);
            float multipler = affinityTable.AfinityTable(placeholderSkill.MyAffinity, currentEnemyC.Affinity);
            currentEnemyC.OnHit(currentPlayerC.CurrentAtq, placeholderSkill.Power, multipler);
            ValWin();
        }
        else if (currentPlayerC.Moveset[skill] is SupportSkill)
        {
            SupportSkill placeholderSkill = currentPlayerC.Moveset[skill] as SupportSkill;
            Debug.Log("Jugador supp skill" + placeholderSkill.Name);
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
        if (currentEnemyC.Moveset[enemy.MadeAction()] is AttackSkill)
        {
            AttackSkill placeholderSkill = currentEnemyC.Moveset[enemy.MadeAction()] as AttackSkill;
            Debug.Log("Enemy affinity skill "+placeholderSkill.MyAffinity);
            float multipler = affinityTable.AfinityTable(placeholderSkill.MyAffinity, currentPlayerC.Affinity);
            currentPlayerC.OnHit(currentEnemyC.CurrentAtq, placeholderSkill.Power, multipler);
            ValWin();
        }
        else if (currentEnemyC.Moveset[enemy.MadeAction()] is SupportSkill)
        {
            SupportSkill placeholderSkill = currentEnemyC.Moveset[enemy.MadeAction()] as SupportSkill;
            Debug.Log("Enemy supp skill" + placeholderSkill.Name);
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
