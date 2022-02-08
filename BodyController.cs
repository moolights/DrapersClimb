using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _theBody;

    Vector2 bodyDir;

    void Start()
    {
        _theBody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void Update()
    {
        PlaceBody();
    }

    void PlaceBody()
    {
        if(!Input.GetKeyDown(KeyCode.LeftControl))
        {
            return;
        }
        _theBody.constraints = RigidbodyConstraints2D.None;
    }
}
