using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbedPlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] limbs;
    [SerializeField] float moveForce = 5f;

    public Camera cam;

    Vector2 mousePos;
    Vector2 lookDir;

    bool limbSelected = false;
    public static char selection;

    void Awake()
    {
        // Freezes all limbs in place
        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Handles the input
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); 

        limbSelector(); // Selection for the limbs

        if(Input.GetButtonDown("Fire1"))
        {
            // Need to fixed how clean code is AND move all physics to FixedUpdate
            // Also, need to limit distance limbs can go
            switch(selection)
            {
                case 'f':
                    lookDir = mousePos - limbs[0].position;
                    lookDir.Normalize(); 
                    limbs[0].AddForce(lookDir * moveForce, ForceMode2D.Impulse); 
                    break;
                case 'd':
                    lookDir = mousePos - limbs[1].position;
                    lookDir.Normalize(); 
                    limbs[1].AddForce(lookDir * moveForce, ForceMode2D.Impulse); 
                    break;
                case 's':
                    lookDir = mousePos - limbs[2].position;
                    lookDir.Normalize(); 
                    limbs[2].AddForce(lookDir * moveForce, ForceMode2D.Impulse); 
                    break;
                case 'a':
                    lookDir = mousePos - limbs[3].position;
                    lookDir.Normalize(); 
                    limbs[3].AddForce(lookDir * moveForce, ForceMode2D.Impulse); 
                    break;
                default:
                    Debug.Log("No selection made");
                    break;
            }
        }
    }

    // Aims the limb to the mouse
    void FixedUpdate()
    {
        int limbNum;

        if(limbSelected)
        {
            switch(selection)
            {
                case 'f':
                    lookDir = mousePos - limbs[0].position;
                    limbNum = 0;
                    break;
                case 'd':
                    lookDir = mousePos - limbs[1].position;
                    limbNum = 1;
                    break;
                case 's':
                    lookDir = mousePos - limbs[2].position;
                    limbNum = 2;
                    break;
                case 'a':
                    lookDir = mousePos - limbs[3].position;
                    limbNum = 3;
                    break;
                default:
                    lookDir = new Vector2(0, 0);
                    limbNum = -1;
                    break;
            }

            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
            limbs[limbNum].rotation = angle;
        }
    }

    // Need to fix code. It's too repetative
    void limbSelector()
    {
        if(Input.GetKeyDown("f") && !limbSelected)
        {
            limbSelected = true;
            limbs[0].constraints = RigidbodyConstraints2D.None;
            selection = 'f';
        }
        else if(Input.GetKeyUp("f"))
        {
            limbSelected = false;
            selection = 'x';
        }
        
        if(Input.GetKeyDown("d") && !limbSelected)
        {
            limbSelected = true;
            limbs[1].constraints = RigidbodyConstraints2D.None;
            selection = 'd';
        }
        else if(Input.GetKeyUp("d"))
        {
            limbSelected = false;
            selection = 'x';
        }
        
        if(Input.GetKeyDown("s") && !limbSelected)
        {
            limbSelected = true;
            limbs[2].constraints = RigidbodyConstraints2D.None;
            selection = 's';
        }
        else if(Input.GetKeyUp("s"))
        {
            limbSelected = false;
            selection = 'x';
        }
        
        if(Input.GetKeyDown("a") && !limbSelected)
        {
            limbSelected = true;
            limbs[3].constraints = RigidbodyConstraints2D.None;
            selection = 'a';
        }
        else if(Input.GetKeyUp("a"))
        {
            limbSelected = false;
            selection = 'x';
        }
    }
}
