using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class hailStone : MonoBehaviour
{

    [SerializeField] bool isPlayerDetected;
    [SerializeField] Transform currentObject;
    [SerializeField] Vector3 playerDetection;
    [SerializeField] LayerMask playerMask;
    [SerializeField] bool isHailInteractive;
    [SerializeField] private GameObject hailObject;
    [SerializeField] private Transform hailDropPosition;
    [SerializeField] private Transform hailRainPosition;
    [SerializeField] VisualEffect hailEffect;
    [SerializeField] int hailSpawns;
    // if the object detects it self
    //[SerializeField] Collider[] targets;
    // Start is called before the first frame update
    void Start()
    {
        hailDropPosition = gameObject.transform.Find("hailDrop").transform;
        hailRainPosition = gameObject.transform.Find("hailRain").transform;
        hailEffect = hailRainPosition.GetComponent<VisualEffect>();
        hailEffect.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckBox(currentObject.transform.position, playerDetection, Quaternion.identity, playerMask);
        
        if (isPlayerDetected && !isHailInteractive)
        {
            hailEffect.Play();
            StartCoroutine(SpawnHail(0.1f));
            isHailInteractive = true;
        }
    }
    public IEnumerator SpawnHail(float second)
    {
        // fixed y position and spawn random directions.
     
        for(int i = 0; i <= hailSpawns; i++)
        {
            hailDropPosition.transform.position = Vector3.zero;
            int randomZ = Random.Range(-5, 5);
            int randomX = Random.Range(-5, 5);
            hailDropPosition.transform.position = new Vector3((transform.position.x + randomX),(transform.position.y + 30f), (transform.position.z + randomZ));
            GameObject hail = (GameObject)Instantiate(hailObject, hailDropPosition);

            float scale = Random.Range(0.2f, 1.2f);
            hail.transform.localScale = new Vector3(scale, scale, scale);
            Rigidbody hailRigidBody = hail.GetComponent<Rigidbody>();
            hailRigidBody.AddForce(new Vector3(0, 0, 200f));
            hailDropPosition.DetachChildren();
            yield return new WaitForSeconds(second);
        }
        StopCoroutine(SpawnHail(second));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawCube(currentObject.transform.position, playerDetection * 2);
    }
}
