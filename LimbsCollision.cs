using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsCollision : MonoBehaviour
{
    [SerializeField] Rigidbody2D leg;

    void OnCollisionEnter2D(Collision2D other) 
    {
        // Fix clicking on new surface after previously colliding
        if(other.gameObject.tag != "Surface")
        {
            return;
        }
        
        FreezeLeg();
    }

    void FreezeLeg()
    {
        leg.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
