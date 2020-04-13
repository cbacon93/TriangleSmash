using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomDrop
{
    public GameObject droppingObject;
    public int number=1;
    public float probability=1f;

    public RandomDrop(GameObject droppingObject, int number, float probability)
    {
        this.droppingObject = droppingObject;
        this.number = number;
        this.probability = probability;
    }
}
