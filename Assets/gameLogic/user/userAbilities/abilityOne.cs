using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityOne : MonoBehaviour
{
    public bool isDetected;
    public bool isRadiusDetected;
    [SerializeField] Transform abilityOneTransform;
    [SerializeField] Vector3 abilityOneCube;
    [SerializeField] LayerMask enemyMask;
  //  [SerializeField] Color color;
    [SerializeField] float abilityOneRadius;
    // Player prefabs
    [SerializeField] Transform playerObject;
    
    // options
    public bool isCubeAttack;
    public bool isSphereAttack;
    [SerializeField] Collider[] targets;
    public GameObject abilityOneImage;
    // Start is called before the first frame update
    void Start()
    {
        abilityOneTransform = gameObject.transform;
        playerObject = GameObject.Find("player").transform.Find("playerPrefab").transform;
        abilityOneImage = FindObjectOfType<abilityOne>().transform.Find("Canvas").gameObject;
        abilityOneImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Attack methods.
        if(isCubeAttack)
        {
            isDetected = Physics.CheckBox(abilityOneTransform.transform.position, abilityOneCube, abilityOneTransform.transform.rotation, enemyMask);
            targets = Physics.OverlapBox(abilityOneTransform.transform.position, abilityOneCube, abilityOneTransform.transform.rotation, enemyMask);
        }
       if(isSphereAttack)
        {
            isRadiusDetected = Physics.CheckSphere(playerObject.transform.position, abilityOneRadius, enemyMask);
            targets = Physics.OverlapSphere(playerObject.transform.position, abilityOneRadius, enemyMask);
        }
      
    }
    private void OnDrawGizmos()
    {
        // collider system cannot affect
      //  Gizmos.color = Color.red;
     //  Gizmos.DrawWireCube(abilityOneTransform.transform.position, abilityOneCube);
     //  Gizmos.DrawWireSphere(playerObject.transform.position, abilityOneRadius);
    }
    public void abilityDamage(float damageAmount)
    {
        foreach(var target in targets)
        {
            target.transform.SendMessage("enemyTakeDamage", damageAmount);
        }

    }
    public void allyAttack(float damageAmount)
    {
        foreach (var target in targets)
        {
            target.transform.SendMessage("enemyTakeDamage", damageAmount);
        }
    }
}
