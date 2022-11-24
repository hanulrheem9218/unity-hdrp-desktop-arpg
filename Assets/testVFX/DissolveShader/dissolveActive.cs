using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dissolveActive : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dissolveObject;
    public dissolveActive dissolveScript;
    public Material objectMaterial;
    public Shader objectShader;

    //Shader value
    public bool isDissolved;
    private bool isDissolveLock;

    public bool isNotDissolved;
    private bool isNotDissolveLock;
    void Start()
    {
        dissolveObject = gameObject;
        dissolveScript = dissolveObject.transform.GetComponent<dissolveActive>();

        // Example Material myMaterial = Resources.Load("Materials/MyMaterial", typeof(Material)) as Material;
        objectMaterial = gameObject.GetComponent<Renderer>().material;
        objectShader = objectMaterial.shader;
        // changing parameter values with the dissolveValue.
    }

    // Update is called once per frame
    void Update()
    {
        if(isDissolved && !isDissolveLock && !isNotDissolved)
        {
            isDissolveLock = true;
            StartCoroutine(dissolved(0.001f, 1f));
        }

        if(isNotDissolved && !isNotDissolveLock && !isDissolved)
        {
            isNotDissolveLock = true;
            StartCoroutine(notDissolved(0.001f, -1f));
        }
    }

    public IEnumerator dissolved(float seconds, float max)
    {
      
        for (float i = -1; i <= max; i += 0.05f)
        {
            objectMaterial.SetFloat("Vector1_bc6aa97729ca4d52ae31e68126c1d1c8", i);
            yield return new WaitForSeconds(seconds);
        }
        StopCoroutine(dissolved(seconds, max));
        isDissolveLock = false;
        isDissolved = false;
    }

    public IEnumerator notDissolved(float seconds, float min)
    {
      
        for (float i = 1f; i >= min; i -= 0.05f)
        {
            objectMaterial.SetFloat("Vector1_bc6aa97729ca4d52ae31e68126c1d1c8", i);
            yield return new WaitForSeconds(seconds);
        }
        StopCoroutine(notDissolved(seconds, min));
        isNotDissolved = false;
        isNotDissolveLock = false;
    }
    }
