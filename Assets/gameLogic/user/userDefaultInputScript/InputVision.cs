using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputVision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isVisible;

    [SerializeField] private Collider[] visibleObjects;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] outsideField;
    [SerializeField] private Transform playerObject;
    [SerializeField] LayerMask restrictedObjects;
    [SerializeField] LayerMask spawnObjects;
    [SerializeField] private float visibleRadius;

    // private int counter = 0;

    // Tutorial
    [SerializeField] private int distanceFromPlayer;
    private GameObject player;
    // adding item class ?
    //private List<>
    void Start()
    {
        playerObject = this.gameObject.transform;
     //   visibleObjects = new Collider[enemies.Length];
        //initiateOptimize();
        Invoke(nameof(initiateOptimize),1f);
        //  InvokeRepeating(nameof(invisibleField), 0f, 2f);
        // InvokeRepeating(nameof(visibleField), 0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
       visibleObjects = Physics.OverlapSphere(playerObject.transform.position, visibleRadius, restrictedObjects);
       isVisible = Physics.CheckSphere(playerObject.transform.position, visibleRadius, spawnObjects);

        if(isVisible)
        {
            invisibleField();
        }
     }
    private void initiateOptimize()
    {
        // this is the problem
        enemies = GameObject.FindGameObjectsWithTag("EnemySpawn");
        outsideField = enemies;
        foreach (GameObject enemy in enemies)
       {
            enemy.transform.GetChild(0).transform.gameObject.SetActive(false);
       }
        //invisibleField();
    }
    private void autoVision()
    {

    }
   // IEnumerator CheckActivation()
   // {
   //     List<GameObject> removeList = new List<GameObject>();
   //     
   // }
    private void invisibleField()
    {

      
            // Mesh renderer more likely ?
            //GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
            //GameObject[] citizens = GameObject.FindGameObjectsWithTag("");
            Debug.Log("active");
     

            foreach (GameObject enemy in enemies)
            {
            foreach (Collider visible in visibleObjects)
            {
                {
                    if (enemy.transform.GetChild(0).transform.gameObject.name == visible.transform.GetChild(0).transform.gameObject.name)
                    {
                        Debug.Log(enemy.gameObject.transform.GetChild(0).transform.gameObject.name);
                        enemy.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Outside:"+enemy.gameObject.transform.GetChild(0).transform.gameObject.name);
                        if (isVisible && enemy.gameObject.transform.GetChild(0).transform.gameObject.activeSelf)
                        {
                           
                           for (int i = 0; i < outsideField.Length; i++)
                           {
                               if(outsideField[i].transform.GetChild(0).transform.gameObject.name == visible.transform.GetChild(0).transform.gameObject.name)
                               {
                                   outsideField[i] = null;
                               }
                             
                           }
                         //foreach(GameObject outside in outsideField)
                         //{
                         //    if (outside.transform.GetChild(0).transform.gameObject.name == visible.transform.GetChild(0).transform.gameObject.name)
                         //    {
                         //          
                         //    }
                         //}
                            //enemy.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
                            
                        }
                    }
                }
            }
            
           // enemy.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(playerObject.transform.position, visibleRadius);
    }
}
