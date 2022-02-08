using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D theBody;
    bool locked = true;

    void Start()
    {
        theBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && locked)
        {
            theBody.constraints = RigidbodyConstraints2D.None;
            locked = false;
            Debug.Log("Unfreeze!");
        }
        else if(Input.GetKeyDown("space") && !locked)
        {
            theBody.constraints = RigidbodyConstraints2D.FreezeAll;
            locked = true;
            Debug.Log("Freeze!");
        }
    }
}
