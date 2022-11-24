using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityFour : MonoBehaviour
{
    public bool isDetected;
    public bool isRadiusDetected;
    [SerializeField] Transform abilityTransform;
    [SerializeField] Vector3 abilityCube;
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
        abilityTransform = gameObject.transform;
        playerObject = GameObject.Find("player").transform.Find("playerPrefab").transform;
        abilityOneImage = FindObjectOfType<abilityFour>().transform.Find("Canvas").gameObject;
        abilityOneImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Attack methods.
        if (isCubeAttack)
        {
            isDetected = Physics.CheckBox(abilityTransform.transform.position, abilityCube, abilityTransform.transform.rotation, enemyMask);
            targets = Physics.OverlapBox(abilityTransform.transform.position, abilityCube, abilityTransform.transform.rotation, enemyMask);
        }
        if (isSphereAttack)
        {
            isRadiusDetected = Physics.CheckSphere(playerObject.transform.position, abilityOneRadius, enemyMask);
            targets = Physics.OverlapSphere(playerObject.transform.position, abilityOneRadius, enemyMask);
        }

    }
    private void OnDrawGizmos()
    {
        // collider system cannot affect
        Gizmos.color = Color.red;
     //   Gizmos.DrawWireCube(abilityTransform.transform.position, abilityCube);
       // Gizmos.DrawWireSphere(playerObject.transform.position, abilityOneRadius);
    }
    public void abilityDamage(float damageAmount)
    {
        foreach (var target in targets)
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
