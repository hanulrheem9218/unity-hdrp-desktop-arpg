using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultMapBlock : MonoBehaviour
{
    [SerializeField] bool isPlayerDetected;
    [SerializeField] bool isVisbile;
    [SerializeField] bool isNotVisible;
    [SerializeField] Transform mapBlockTransform;
    [SerializeField] Vector3 mapBlockDetection;
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject[] visibleObjects;
    // Start is called before the first frame update
    void Start()
    {
        mapBlockTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckBox(mapBlockTransform.transform.position, mapBlockDetection, Quaternion.identity, playerMask);
        if(isPlayerDetected && !isNotVisible)
        {
           
                activeVisibleObjects();
                isNotVisible = true;
                isVisbile = false;
        }
        else if(!isPlayerDetected && !isVisbile)
        {
        
                deativeVisibleObjects();
                isNotVisible = false;
                isVisbile = true;
        }
    }
    
    public void activeVisibleObjects()
    {
        for(int i = 0; i < visibleObjects.Length; i++)
        {
            dissolveActive dissovled = visibleObjects[i].transform.GetComponent<dissolveActive>();
            dissovled.isDissolved = true;
        }
    }
    public void deativeVisibleObjects()
    {
        for (int i = 0; i < visibleObjects.Length; i++)
        {
            dissolveActive dissovled = visibleObjects[i].transform.GetComponent<dissolveActive>();
            dissovled.isNotDissolved = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(mapBlockTransform.transform.position, mapBlockDetection * 2);
    }
}
