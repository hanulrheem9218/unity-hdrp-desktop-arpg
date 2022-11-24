using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class hailDropDamage : MonoBehaviour
{
    [SerializeField] Transform currentObject;
    [SerializeField] Transform groundObject;
    [SerializeField] private float attackRadius;
    [SerializeField] private float groundRadius;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Collider[] targets;
    [SerializeField] VisualEffect impactEffect;
    [SerializeField] bool isDetected;
    [SerializeField] bool isOverlaped;

    [SerializeField] bool isGroundDetected;
    [SerializeField] bool isGroundOverlaped;

    // Start is called before the first frame update
    void Start()
    {
        impactEffect = gameObject.transform.Find("impact").GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the ground touched.
        isGroundDetected = Physics.CheckSphere(groundObject.transform.position, groundRadius, groundMask);
        isDetected = Physics.CheckSphere(currentObject.transform.position, attackRadius, targetMask);
        targets = Physics.OverlapSphere(currentObject.transform.position, attackRadius, targetMask);
  
        if(isDetected && !isOverlaped && !isGroundOverlaped)
        {
            GiveDamage(100f);
            isOverlaped = true;
        }

        if(isGroundDetected && !isGroundOverlaped)
        {
            impactEffect.Play();
            StartCoroutine(DestoryObjectWithAnimation(3f));
            isGroundOverlaped = true;
        }
    }
    public IEnumerator DestoryObjectWithAnimation(float second)
    {
        Collider hailColider = gameObject.transform.GetComponent<Collider>();
        yield return new WaitForSeconds(second);
        hailColider.isTrigger = true;
        yield return new WaitForSeconds(1f);
        StopCoroutine(DestoryObjectWithAnimation(second));
        Destroy(gameObject);
    }
    
    public void GiveDamage(float damageAmount)
    {
        foreach(var target in targets)
        {
            target.transform.SendMessage("playerTakeDamage", damageAmount);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentObject.transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundObject.transform.position, groundRadius);

    }
}
