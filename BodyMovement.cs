using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D theBody;
    bool locked = true;
    // Start is called before the first frame update
    void Start()
    {
        theBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
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
