using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FistBox : MonoBehaviour
{
    public enum type
    {
        force,
        swift,
        deft
    }

    [SerializeField] int damage;
    [SerializeField] type fistType;


    
}
