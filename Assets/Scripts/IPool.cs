using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool 
{ // esto no se toca, es solo para tomer la cantidad de metodos que la creacion de critters.
    void Instantiate(); //momento al crear el critter
    void Begin(Vector3 position); //momento en que se llama el critter
    
}
