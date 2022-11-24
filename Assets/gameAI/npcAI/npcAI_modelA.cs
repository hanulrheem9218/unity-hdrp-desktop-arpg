using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npcAI_modelA : MonoBehaviour
{
    //npc AI DATA SECTION
    // core items
    [Header("you must add this settings")]

    // this is changed.
    [SerializeField] int randomMovingRange;
    [SerializeField] Transform[] coreTargetPath;
    [SerializeField] Transform coreTarget;
    [SerializeField] Transform coreFrontAI;
    public float coreBackRadius;
    [SerializeField] Transform Target;
    [SerializeField] Transform coreRayView;

    // Trasnform Detection SECTION
    public Transform npcObject;
    public float playerDetectionRange;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] LayerMask playerMask;

    // if Corpse founded.

    [SerializeField] float corpseDetectionRange;
    [SerializeField] bool isCorpseDetected;
    [SerializeField] LayerMask corpseMask;

    // UI SECTION
    [SerializeField] bool ignoreUI;
    [SerializeField] Transform damageViewPosition;
    public GameObject damageViewObject;

    public float npcHealth;
    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float fixedHealth;
    [SerializeField] float localDamageCount;
    [SerializeField] Transform itemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] Text npcText;
    // displaying the gameObject
    [SerializeField] GameObject displayUIObject;
    // Range field properties STATUS properties DETECTION, SHOOT, MELEE
    public float coreDetectionRange;
    public float coreShootRange;
    public float coreMeleeRange;
    public float coreMovementSpeed;
    public float coreTurnSpeed;
    //Animation Section 
    [SerializeField] Animator npcAnimator;
    // Start is called before the first frame update


    //Additional attributes.
    [SerializeField] bool isnpcDead;

    //behaviour scripts
    [SerializeField] bool LookAtObject;
    public bool LookAtBack;
    public bool LookAtFront;
    [SerializeField] bool LookShootPlayer;
    [SerializeField] bool LookAttackPlayer;
    [SerializeField] bool visionFull;
    // is it allowed ? relates to animation status.
    [SerializeField] bool attackAllowed;
    [SerializeField] bool shootAllowed;

    // Changing next wave.
    [SerializeField] private int TargetIndex = 0;
    [SerializeField] private int TargetMaxPath = 0;
    [SerializeField] private bool npcFollowPrevious;
    [SerializeField] private bool npcFollowNext;
    [SerializeField] private bool npcFollowReset;
    [Header("Initiate Path follow")]
    [SerializeField] private bool initiateFollowPath;
    //Test outputs



    // Ragdoll fucntion
    [SerializeField] CapsuleCollider mainCollider;
    [SerializeField] Rigidbody mainRigidBody;


    [SerializeField] bool randomMove;
    void Start()
    {

        coreFrontAI = GameObject.Find("npcManager").transform;
       
        npcObject = gameObject.transform;
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform;
        itemDisplayUI = GameObject.Find(npcObject.name).transform.Find("Canvas").transform;

        damageViewPosition = GameObject.Find(npcObject.name).transform.Find("damageView").transform;
        displayUIObject = itemDisplayUI.gameObject;
        npcText = itemDisplayUI.transform.Find("Slider").Find("Text").GetComponent<Text>();
        updated_HealthUIObject = GameObject.Find(npcObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
        // more preferences
        updated_HealthUIObject.maxValue = npcHealth;
        updated_HealthUIObject.minValue = 0f;
        updated_HealthUIObject.value = npcHealth;
        npcText.text = npcObject.name;
        fixedHealth = npcHealth;
        displayUIObject.SetActive(false);
        InvokeRepeating(nameof(getTargetWithTransform), 1,2f);
        IdentifyCoreTargets();
        DeactiveRagdoll();
     
        
    }
    public void ActiveRagdoll()
    {
        npcAnimator.enabled = false;
        mainCollider.enabled = false;
        // bug fixes.
        mainRigidBody = GameObject.Find(npcObject.name).transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = true;
        mainRigidBody.useGravity = false; // no dropping please. error
        mainRigidBody.detectCollisions = false;
        Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;

        ragDollColliders = npcObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = npcObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach(Collider col in ragDollColliders)
        {
            col.enabled = true;

        }
        foreach(Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = false;
            
        }
        Invoke("DestoryWithEffect", 5f);
    }
    public void DestoryWithEffect()
    {
      //  Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;
        ragDollRigidBodys = npcObject.transform.GetComponentsInChildren<Rigidbody>();
      //  ragDollColliders = npcObject.transform.GetComponentsInChildren<Collider>();
      //  foreach (Collider col in ragDollColliders)
      //  {
      //      col.enabled = false;
      //  }
        foreach (Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = true;
            rigid.detectCollisions = false;
           // rigid.gameObject.SetActive(false);
        }
    }
    public void DeactiveRagdoll()
    {

        Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;

        ragDollColliders = npcObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = npcObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        foreach(Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = true;
        }
        mainCollider = GameObject.Find(npcObject.name).transform.GetComponent<CapsuleCollider>();
        mainCollider.enabled = true;
        mainRigidBody = GameObject.Find(npcObject.name).transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = false;
    }
    public void IdentifyCoreTargets()
    {
        coreTargetPath = new Transform[GameObject.FindGameObjectWithTag("TargetPoints").transform.childCount];
        for(int i = 0; i < coreTargetPath.Length; i++)
        {
            coreTargetPath[i] = GameObject.FindGameObjectWithTag("TargetPoints").transform.GetChild(i);
        }
        coreTarget = coreTargetPath[0];
        TargetMaxPath = coreTargetPath.Length;
    }
    public void getTargetWithNextTransform()
    {
        if (!npcFollowNext)
        {
                    coreTarget = coreTargetPath[TargetIndex];
                    ++TargetIndex;
            }
    }
    public void getTargetWithPreviousTransform()
    {
        if (!npcFollowPrevious)
        {

            --TargetIndex;
            coreTarget = coreTargetPath[TargetIndex];
        }
    }

    public void interactiveDisplay()
    {
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
 
    public void getTargetWithTransform()
    {
        if (this.gameObject.activeInHierarchy)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] corpses = GameObject.FindGameObjectsWithTag("Corpse");

            //Get final distance for the gameobject.
            float coreShortestDistanceAI = Mathf.Infinity;
            GameObject nearest_object = null;

            float distanceToBack = Vector3.Distance(npcObject.transform.position, coreTarget.transform.position);

            // those are enemies, with types
            // this is for the player object detections.
            foreach (GameObject player in players)
            {
                float distanceToObject = Vector3.Distance(npcObject.transform.position, player.transform.position);

                if (distanceToObject < coreShortestDistanceAI)
                {
                    coreShortestDistanceAI = distanceToObject;
                    nearest_object = player;
                }
            }
            // ally detections 

            //corpse detections
            foreach (GameObject corpse in corpses)
            {
                float distanceToObject = Vector3.Distance(npcObject.transform.position, corpse.transform.position);

                if (distanceToObject < coreShortestDistanceAI)
                {
                    coreShortestDistanceAI = distanceToObject;
                    nearest_object = corpse;
                }
            }

            // identifying the objects

            if (nearest_object != null && coreShortestDistanceAI <= coreDetectionRange)
            {
                // custom
                if (!visionFull)
                {
                    visionFull = true;
                    CancelInvoke(nameof(getTargetWithTransform));
                }
                // Debug.Log("detecting player object");
                Target = nearest_object.transform;
                // TRUE
                LookAttackPlayer = false;
                LookAtObject = true;
                LookShootPlayer = false;

                LookAtBack = false;
                LookAtFront = false;
                if (coreShortestDistanceAI <= coreShootRange)
                {
                    Debug.Log("shoot");
                    LookAttackPlayer = false;
                    LookAtObject = false;
                    LookShootPlayer = true;

                    LookAtBack = false;
                    LookAtFront = false;
                    if (coreShortestDistanceAI <= coreMeleeRange)
                    {
                        Debug.Log("mellee");
                        //Attack Mechanics
                        // attackAllowed = true;
                        LookAttackPlayer = true;
                        LookAtObject = false;
                        LookShootPlayer = false;

                        LookAtBack = false;
                        LookAtFront = false;
                    }


                }
            }
            else
            {
                LookAtObject = false;
                LookAtBack = true;

                if (!isnpcDead && distanceToBack <= coreBackRadius)
                {
                    // two booleans Experimentals
                    if (!initiateFollowPath)
                    {
                        // new condition
                        LookAtBack = false;
                        coreWalkForward();
                        if (!randomMove)
                        {
                            coreMovementSpeed = 0.3f;
                            randomMove = true;

                            StartCoroutine(moveRandomly(10f));
                        }
                        //  coreMovementSpeed = 1f;
                        // LookAtFront = true;
                        // LookAtBack = false;
                    }

                    // if condition is enabled
                    if (initiateFollowPath)
                    {
                        LookAtBack = true;
                        LookAtFront = false;
                        if (!npcFollowNext && TargetIndex >= 0f && !npcFollowReset)
                        {
                            getTargetWithNextTransform();
                            if (TargetMaxPath < TargetIndex)
                            {
                                // print("Rest");
                                npcFollowNext = true;
                            }
                            else if (TargetMaxPath == TargetIndex)
                            {

                                npcFollowNext = true;
                                npcFollowPrevious = false;
                                npcFollowReset = true;
                            }
                        }
                        if (!npcFollowPrevious && npcFollowReset)
                        {


                            getTargetWithPreviousTransform();
                            if (TargetIndex == 0)
                            {
                                npcFollowNext = false;
                                npcFollowPrevious = true;
                                npcFollowReset = false;
                            }
                        }
                    }
                    //LookAtFront = true;
                    //LookAtBack = false;
                }
            }
        }

    }

    public void OnMouseEnter()
    {
        displayUIObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        if (!ignoreUI)
        {
            displayUIObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        // check if any dead corpses are founded
        // isCorpseDetected = Physics.CheckSphere(npcObject.transform.position, corpseDetectionRange, corpseMask);
        // isPlayerDetected = Physics.CheckSphere(npcObject.transform.position, playerDetectionRange, playerMask);
        // sightOnPlayer();
        // sightOnCorpse();
        // Behaviour status
        interactiveDisplay();
        if (this.gameObject.activeInHierarchy && !isnpcDead && visionFull) getTargetWithTransform();
        // if it dies
    
        // Old detection system but it can 
        if (this.gameObject.activeInHierarchy && !isnpcDead && LookAtObject) { coreLookAtObject(Target.transform); coreWalkForward(); }
        if (this.gameObject.activeInHierarchy && !isnpcDead && LookAtBack) { coreLookAtBack(); coreWalkForward(); }
        if (this.gameObject.activeInHierarchy && !isnpcDead && LookAtFront) { coreLookAtFront(); }
        // Attacking status.
        if (this.gameObject.activeInHierarchy && !isnpcDead && LookShootPlayer) coreLookAtObject(Target.transform);
      
        // this property only happen once.
     
        // animation status SMART AI CONDITIONS NOW
        if (this.gameObject.activeInHierarchy && !isnpcDead && LookAtObject || !isnpcDead && LookAtBack) npcAnimator.SetBool("isWalking", true);
        else npcAnimator.SetBool("isWalking", false);
        //  if (!isnpcDead && LookAtBack) npcAnimator.SetBool("isWalking", true);
        //  else npcAnimator.SetBool("isWalking", false);
        //  if (!isnpcDead && LookAtFront) npcAnimator.SetBool("isWalking", true);
        //  else npcAnimator.SetBool("isWalking", false);
        if (this.gameObject.activeInHierarchy && !isnpcDead && LookAttackPlayer) npcAnimator.SetBool("isAttacking", true);
        else npcAnimator.SetBool("isAttacking", false);
     //   if (!isnpcDead && LookShootPlayer) npcAnimator.SetBool("isShooting", true);
     //   else npcAnimator.SetBool("isShooting", false);
    }

    public IEnumerator moveRandomly(float seconds)
    {

        //random Geneartor.
       
        float randomY = Random.Range(-randomMovingRange, randomMovingRange);
 
            // accessing new transform data
    
          
        this.gameObject.transform.rotation = Quaternion.Euler(0f,randomY,0f);        
            //Invoke(nameof(moveRandomly), 3f);
            // change y random values.
        yield return new WaitForSeconds(seconds);
        randomMove = false;
        StopCoroutine(moveRandomly(seconds));
        Debug.Log("h");
        
        
  
    }
   
  public void enemyTakeDamage(float damageAmount)
    {
         localDamageCount++; // this is for local damage count so it doesnt merge.
        GameObject damageView = Instantiate(damageViewObject.gameObject, damageViewPosition);
        damageView.name = this.gameObject.name + "_" + "DamageView" + localDamageCount;
        damageViewPosition.DetachChildren();
        showDamageStatus damageScript = GameObject.Find(damageView.name).transform.GetComponent<showDamageStatus>();
        //damageScript.StartCoroutine(damageScript.destoryDamageStatus01(1f));
        Text damageDisplayAmount = GameObject.Find(damageView.name).transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        // damageDisplayAmount.color = Color.white; // in default damage
        int divided = ((int)fixedHealth / 2);

        if (damageAmount <= divided)
        {
            damageScript.StartCoroutine(damageScript.destoryDamageStatus01(1f));
            damageDisplayAmount.text = damageAmount.ToString("0");
            damageDisplayAmount.color = Color.white; // small damagge
        }
        else if (damageAmount <= 75f)
        {
            damageScript.StartCoroutine(damageScript.destoryDamageStatus02(1f));
            damageDisplayAmount.text = damageAmount.ToString("0");
            damageDisplayAmount.color = Color.yellow; // Mid Damage

        }
        else if (damageAmount <= fixedHealth)
        {
            damageScript.StartCoroutine(damageScript.destoryDamageStatus02(1f));
            damageDisplayAmount.text = damageAmount.ToString("0");
            damageDisplayAmount.color = Color.red; // in critical damage
        }


        damageDisplayAmount.CrossFadeAlpha(0, 0.3f, false);
        //Access rigidbody system
        // int randomX = Random.Range(-5, 15);
        int randomY = Random.Range(100, 120);
        int randomNX = Random.Range(-100, 120);
        //int randomNY = Random.Range(100, 120);
        Rigidbody damageRigidBody = GameObject.Find(damageView.name).GetComponent<Rigidbody>();
        damageRigidBody.AddForce(Vector3.up * randomY + Vector3.forward + randomNX * Vector3.right, ForceMode.Acceleration);


        // keyword  Bugfix
        Target = coreTarget.transform;
        ignoreUI = true;
        displayUIObject.SetActive(true);
        updated_HealthUIObject.value -= damageAmount;
        npcHealth -= damageAmount;
        if (npcHealth <= 0)
        {
            Target = coreTarget.transform;
            isnpcDead = true;
            ignoreUI = false;
            npcObject.tag = "Untagged";
            displayUIObject.SetActive(false);
            ActiveRagdoll();
            GameObject minimap = this.gameObject.transform.Find("MINIMAP_playerUI").transform.gameObject;
            minimap.SetActive(false);
        }
    }
    public void coreWalkForward()
    {
        //  npcAnimator.SetBool("isWalking", true);
        npcObject.transform.position += npcObject.transform.forward * Time.deltaTime * coreMovementSpeed;
    }
    public void coreLookAtObject(Transform Target)
    {

        Vector3 dir = Target.transform.position - npcObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(npcObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        npcObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    public void coreLookAtBack()
    {// walking true
     //   npcAnimator.SetBool("isWalking", true);
        Vector3 dir = coreTarget.transform.position - npcObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(npcObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        npcObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }
    public void coreLookAtFront()
    {
        Vector3 dir = coreFrontAI.transform.position - npcObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(npcObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        npcObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnDrawGizmos()
    {
        // those are deteciton range fields.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(npcObject.transform.position, playerDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(npcObject.transform.position, corpseDetectionRange);
        //shooting attributes
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(npcObject.transform.position, coreDetectionRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(npcObject.transform.position, coreShootRange);
        Gizmos.DrawWireSphere(npcObject.transform.position, coreMeleeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coreTarget.transform.position, coreBackRadius);
        Gizmos.DrawWireSphere(coreFrontAI.transform.position, coreBackRadius);
    }
}
