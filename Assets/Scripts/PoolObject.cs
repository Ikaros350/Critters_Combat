using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] Critter item; //item que hara a la piscina
    //public static System.Random selector;
    [SerializeField] int currentCritters; //critters
    IPool [] items; //arreglo del item con la interfaz
    Critter[] objects; //arreglo de objetos
    int index = 0; // cuenta la posicion de los item en el arreglo

    void Awake()
    {
        //selector = new System.Random(System.DateTime.Now.Millisecond);

        if (item.GetComponent<IPool>() == null) // si el item no tiene la interfaz IPool no funcionara
        {
            Debug.LogError("No hay interface IPool En el objeto");
            return;
        }
        items = new IPool[currentCritters]; // pone la cantidad de item disponibles en la piscina
        objects = new Critter[currentCritters]; // pone la cantidad de gameobjects
        for (int i = 0; i < currentCritters; i++) // aca se hace la creacion
        {
            
            objects[i] = Instantiate(item, transform.position, Quaternion.identity);
            objects[i].transform.parent = transform;
            
            items[i] = objects[i].GetComponent<IPool>();
            items[i].Instantiate(); //esto se activa en el critter
        }

    }
    public Critter GetItem(Vector3 position) //pasa a la siguiente posicion
    {
        items[index].Begin(position);
        Critter tmp = objects[index];
        index++;
        if (index >= items.Length) index = 0;
        return tmp;
    }

}
