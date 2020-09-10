using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool 
{ // esto no se toca, es solo para tomer la cantidad de metodos que tendra la flecha.
    void Instantiate(); //momento al crear la flecha
    void Begin(Vector3 position); //momento en que se llama la flecha
    
}
