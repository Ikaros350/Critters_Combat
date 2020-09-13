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
    private string turnCondition;
    Affinity affinityTable = new Affinity();

    [SerializeField] int CRITTERS_ENEMY_COUNT;
    [SerializeField] int CRITTERS_PLAYER_COUNT;

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
        CRITTERS_PLAYER_COUNT = player.Critters.Count;
        RegisterObservers();
        if (hasResgisteredObservers)
        {
            currentCrittersCap =0;
            //NotifyObservers();
        }
        currentPlayerC = player.Critters[0];
        currentEnemyC = enemy.Critters[0];

        myUImanager.UpdateButtons();
       
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
        if (CRITTERS_PLAYER_COUNT >= 1 && CurrentPlayerC.Hp<=0 )
        {
            currentPlayerC = player.Critters[0];
            Debug.Log("Cambie player"); 
        }
        if(CRITTERS_ENEMY_COUNT >= 1 && CurrentEnemyC.Hp <= 0)
        {
            currentEnemyC = enemy.Critters[0];
            Debug.Log("Cambie enemy");
        }
    }
    public bool CanChange()
    {
        bool canChange = false;
        if (currentPlayerC.Moveset.Count != 0)
            canChange = true;

        return canChange;
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
                ValWin();
            }

            //caso enemigo mas rapido que un enemigo
            if (currentPlayerC.BaseSpeed < currentEnemyC.BaseSpeed)
            {
                EnemyTurn();
                timeCounter += 1f * Time.deltaTime;

                if (currentPlayerC.Hp > 0)
                    PlayerTurn(skill);
                ValWin();
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
                    ValWin();
                }

                if (coin == 0)
                {
                    PlayerTurn(skill);
                    timeCounter += 1f * Time.deltaTime;

                    if (currentEnemyC.Hp > 0)
                        EnemyTurn();
                    ValWin();
                }
            }

            
        }
       

    }
   public void StarTurn()
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
                CRITTERS_PLAYER_COUNT -= 1;

                if(CRITTERS_PLAYER_COUNT !=0)
                    ChangeCritter();
                
                myUImanager.UpdateButtons();
            }

            if (currentEnemyC.Hp <= 0)
            {
                player.AddCritters(currentEnemyC);
                enemy.LoseCritter(currentEnemyC);
                if (hasResgisteredObservers)
                {
                    NotifyObservers();
                }
                CRITTERS_ENEMY_COUNT -= 1;
                if(CRITTERS_ENEMY_COUNT !=0)
                    ChangeCritter();
            }
        }
        else
        {

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
            

            turnCondition = "El Critter:  " + currentPlayerC.Name + " del jugador uso: " + currentPlayerC.Moveset[skill].Name +"\n"+" del Atributo: "+ placeholderSkill.MyAffinity;
        }
        else if (currentPlayerC.Moveset[skill] is SupportSkill)
        {
            SupportSkill placeholderSkill = currentPlayerC.Moveset[skill] as SupportSkill;
            if (placeholderSkill != null)
            {

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

                turnCondition = "El Critter:  " + currentPlayerC.Name + " del jugador uso: " + currentPlayerC.Moveset[skill].Name;
            }
            else
            {
                Debug.Log("Soy turista no se nada version player");
            }
        }  
        myUImanager.ConditionUpdate(turnCondition);
    }
    void EnemyTurn()
    {
        Skill newSkill = currentEnemyC.Moveset[enemy.MadeAction()];

        //Turno del enemigo
        if (newSkill is AttackSkill)
        {

            AttackSkill placeholderSkill = newSkill as AttackSkill;

            if (placeholderSkill != null)
            {
               
                float multipler = affinityTable.AfinityTable(placeholderSkill.MyAffinity, currentPlayerC.Affinity);
                currentPlayerC.OnHit(currentEnemyC.CurrentAtq, placeholderSkill.Power, multipler);
                

                turnCondition = "El Critter:  " + currentEnemyC.Name + " del enemigo uso: " + newSkill.Name + "\n"+" del atributo: " + placeholderSkill.MyAffinity;
            }
            else
            {
                Debug.Log("Suerte papi");
            }
        }
        else if (newSkill is SupportSkill)
        {
            SupportSkill placeholderSkill = newSkill as SupportSkill;
            
            if (placeholderSkill != null)
            {
                

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

                turnCondition = "El Critter:  " + currentEnemyC.Name + " del enemigo uso: " + newSkill.Name;
            }
            else
            {
                Debug.Log("soy turista no se nada");
            }
        }
        myUImanager.ConditionUpdate(turnCondition);
    }
}
