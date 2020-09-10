using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee instance;

    [SerializeField]private EnemyLogic enemy;

    [SerializeField]private Player player;

    [SerializeField]private Critter currentPlayerC, currentEnemyC;


    public static Referee Instance { get => instance; }


    private void Awake()
    {
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
        currentPlayerC = player.Critters[0];
        currentEnemyC = enemy.Critters[0];
    }

    void Update()
    {
        EvalTurn();
    }
    public void Action()
    {
        
    }
    void EvalTurn()
    {
        if(currentPlayerC.Hp <= 0)
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
