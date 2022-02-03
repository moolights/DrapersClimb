using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] limbs;
    [SerializeField] float moveForce = 5f;
    Vector2 limbForce;

    Vector2 startPos;
    float maxDistance = 1.5f;

    public Camera cam;

    Vector2 mousePos;
    Vector2 lookDir;

    bool limbSelected;
    bool forceApplied;
    public static char selection;

    float maxVelocity = 5f;

    void Awake()
    {
        // Freezes all limbs in place
        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].constraints = RigidbodyConstraints2D.FreezeAll;
            limbs[i].velocity = Vector2.ClampMagnitude(limbs[i].velocity, maxVelocity);
        }
    }

    private void Start() 
    {
        startPos = limbs[0].position;
    }

    // Handles the input
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); 

        limbSelector(); // Selection for the limbs

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

            lookDir.Normalize(); 
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
            limbs[limbNum].rotation = angle;
        }

        // Distance still an issue
        if(Input.GetMouseButtonDown(0))
        {
            limbForce = lookDir * moveForce * Time.fixedDeltaTime;
            forceApplied = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            limbForce = new Vector2(0f, 0f);
        }

        // limits distance and works with MoveToward()
        float dst = Vector2.Distance(startPos, mousePos);
        if(dst > maxDistance)
        {
            Vector2 vect = startPos - mousePos;
            vect = vect.normalized;
            vect *= (dst - maxDistance);
            mousePos += vect;
        }
    }

    // Aims the limb to the mouse
    void FixedUpdate()
    {
        if(forceApplied)
        {
            switch(selection)
            {
            case 'f':
                //limbs[0].AddForce(limbForce, ForceMode2D.Impulse);
                limbs[0].position = Vector2.MoveTowards(limbs[0].position, mousePos, 
                moveForce * Time.fixedDeltaTime); // This is instant and feels too choppy. Also allows limbs into objects
                break;
            case 'd':
                limbs[1].AddForce(limbForce, ForceMode2D.Impulse); 
                break;
            case 's':
                limbs[2].AddForce(limbForce, ForceMode2D.Impulse); 
                break;
            case 'a':
                limbs[3].AddForce(limbForce, ForceMode2D.Impulse); 
                break;
            default:
                Debug.Log("No selection made");
                break;
            }

            forceApplied = false;
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
