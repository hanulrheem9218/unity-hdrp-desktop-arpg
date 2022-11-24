using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectContainer : MonoBehaviour
{
    // Start is called before the first frame update
  //  [SerializeField] GameObject currentObject;
    [SerializeField] float upAndDown;
    [SerializeField] float frequency;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Float up/down with a sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * upAndDown;
        transform.position = tempPos;
    }

}
