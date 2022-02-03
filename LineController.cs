using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] GameObject legAnchor;
    [SerializeField] GameObject bodyAnchors;
    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, bodyAnchors.transform.position);
        lr.SetPosition(1, legAnchor.transform.position);
    }
}
