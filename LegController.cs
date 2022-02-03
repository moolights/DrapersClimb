using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] _legs;
    [SerializeField] FrictionJoint2D[] _joints;
    [SerializeField] Camera _cam;

    Vector2 legDir;
    Vector2 mousePos;

    Vector2 legForce;
    float force = 300;

    bool selected;
    bool fired;

    int activeLeg;

    void Update()
    {
        LegSelecter();
        LegDirection();
        LimitRadius();
        ApplyForce();
    }

    void FixedUpdate() 
    {
        ShootLeg();
    }

    void LegDirection()
    {
        if(!selected)
        {
            return;
        }
        mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        legDir = mousePos - _legs[activeLeg].position;

        float angle = Mathf.Atan2(legDir.y, legDir.x) * Mathf.Rad2Deg - 90f;
        _legs[activeLeg].rotation = angle;   
    }

    void ShootLeg()
    {
        if(!fired)
        {
            return;
        }
       
        _legs[activeLeg].AddForce(legForce, ForceMode2D.Impulse);
        fired = false;
    }

    void LegSelecter()
    {   
        if(Input.GetKeyDown("f"))
        {
            selected = true;
            activeLeg = 0;           
        }
        if(Input.GetKeyDown("d"))
        {
            selected = true;
            activeLeg = 1;            
        }
        if(Input.GetKeyDown("s"))
        {
            selected = true;
            activeLeg = 2;            
        }
        if(Input.GetKeyDown("a"))
        {
            selected = true;
            activeLeg = 3;
        }
        if(!Input.anyKey)
        {
            selected = false;
        }
    }

    void LimitRadius()
    {
        if(!selected)
        {
            return;
        }

        Vector2 center = _joints[activeLeg].connectedAnchor;
        float maxRadius = 1f;
        float distanceFromCenter = Vector2.Distance(center, _legs[activeLeg].position);

        if(distanceFromCenter > maxRadius)
        {
            Vector2 vect = _legs[activeLeg].position - center;
            vect = vect.normalized;
            _legs[activeLeg].position = vect + center;
        }
    }

    void ApplyForce()
    {
        if(Input.GetMouseButtonDown(0) && selected)
        {
            legForce = legDir * force * Time.fixedDeltaTime;
            fired = true;
        }   
    }
}
