using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilitySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isCube, isSphere, isShoot;
    public Transform playerOrigin;
    public LayerMask enemyMask;

    public float sphereRadius;
    public Collider[] targets;
    [Header("Collision")]
    public Vector3 m_DetectorOffset = Vector3.zero;
    public Vector3 m_DetectorSize = Vector3.zero;
    public float m_DetectorOffsetZ = 0.0f;

    void Start()
    {
        playerOrigin = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSphere) { targets = Physics.OverlapSphere(playerOrigin.transform.position, sphereRadius); }
        else if (isCube) {
            Vector3 colliderPos = transform.position + (transform.forward * m_DetectorOffsetZ);
            targets = Physics.OverlapBox(colliderPos, m_DetectorSize, playerOrigin.transform.rotation, enemyMask); }
        else if (isShoot) { }
    }
    public void damageObject(float damageAmount)
    {
        foreach(var target in targets)   {  target.transform.SendMessage("enemyTakeDamage", damageAmount);  }
    }
    public void shootObject(float damageAmount)
    {

    }
    private void OnDrawGizmos()
    {
        if (isCube)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Vector3 pos = Vector3.zero;
            pos.z = m_DetectorOffsetZ;
            Gizmos.DrawWireCube(pos, m_DetectorSize * 2.0f);
        }
        else if (isSphere)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(playerOrigin.transform.position, sphereRadius);
        }
    }
}
