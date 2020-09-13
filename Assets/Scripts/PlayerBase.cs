using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private List<Critter> critters;
    PoolObject poolCritters;
    protected System.Random selector = new System.Random();
    public List<Critter> Critters { get => critters; private set => critters = value; }
    protected PoolObject PoolCritters { get => poolCritters; set => poolCritters = value; }

    protected virtual void Awake()
    {

    }
    protected void EquipCritters(int numCritters)
    {
        System.Random selector = new System.Random();

        /*
        if (numCritters > 3)
            numCritters = 3;
        if (numCritters <= 0)
            numCritters = 1;
        */
        numCritters = Mathf.Clamp(numCritters, 1, 3); //Limite de Critters entre 1 y 3.

        //Aqui equipamos los critters presentes en la Pool
        for (int i = 0; i < numCritters; i++)
        {
            Critter clone = poolCritters.GetItem(transform.position);
            //clone.transform.eulerAngles = transform.eulerAngles;
            critters.Add(clone);
            critters[i].transform.parent = gameObject.transform;
        }
        for (int i = 0; i < numCritters; i++)
        {
            if (i != 0)
            {
                critters[i].gameObject.SetActive(false);
            }
        }
    }
    public void LoseCritter(Critter critter)
    {
        if (Critters.Count != 0)
        {
            
            Critters[0].gameObject.SetActive(false);
            Critters.Remove(critter);
            
            if(Critters.Count != 0)
                Critters[0].gameObject.SetActive(true); 
        }
    }
    public void AddCritters(Critter critter)
    {
        critters.Add(critter);
    }

}
