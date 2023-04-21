using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPos;
    public float offset;
    void Start()
    {
        startPos = transform.position;
        offset = GetComponent<BoxCollider>().bounds.size.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - offset)
        {
            transform.position = startPos;
        }
    }
}
