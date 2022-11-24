using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class bossAISoulEater : MonoBehaviour
{
    //Boss AI DATA SECTION
    // core items
    [Header("you must add this settings")]
    [SerializeField] bossDataAI DATA_boss;
    [SerializeField] Transform coreBackAI;
    [SerializeField] Transform coreFrontAI;
    [SerializeField] float coreBackRadius;
    [SerializeField] Transform Target;
    [SerializeField] Transform coreRayView;
    
    // Trasnform Detection SECTION
    public Transform bossObject;
    [SerializeField] float playerDetectionRange;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] LayerMask playerMask;

    // if Corpse founded.
    
    [SerializeField] float corpseDetectionRange;
    [SerializeField] bool isCorpseDetected;
    [SerializeField] LayerMask corpseMask;

    // UI SECTION
    [SerializeField] bool ignoredUI;
    [SerializeField] Transform damageViewPosition;

    [SerializeField] float bossHealth;
    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float fixedHealth;
    [SerializeField] float localDamageCount;
    [SerializeField] Transform itemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] Text bossText;
    // displaying the gameObject
    [SerializeField] GameObject displayUIObject;
    // Range field properties STATUS properties DETECTION, SHOOT, MELEE
    [SerializeField] float coreDetectionRange;
    [SerializeField] float coreShootRange;
    [SerializeField] float coreMeleeRange;
    [SerializeField] float coreMovementSpeed;
    [SerializeField] float coreTurnSpeed;
    //Animation Section 
    [SerializeField] Animator bossAnimator;
    // Start is called before the first frame update


    //Additional attributes.
    [SerializeField] bool isBossDead;

    //behaviour scripts
    [SerializeField] bool LookAtObject;
    [SerializeField] bool LookAtBack;
    [SerializeField] bool LookAtFront;
    [SerializeField] bool LookShootPlayer;
    [SerializeField] bool LookAttackPlayer;
    // is it allowed ? relates to animation status.
    [SerializeField] bool attackAllowed;
    [SerializeField] bool shootAllowed;

    // AI Preferences
    [SerializeField] int duplicatedEnemyNames;
    [SerializeField] string[] aiNames;

    [SerializeField] CapsuleCollider mainCollider;
    [SerializeField] Rigidbody mainRigidBody;
    //Test outputs

    void Start()
    {
        // System Preferences
        aiNames = new string[duplicatedEnemyNames];
        for (int i = 0; i < duplicatedEnemyNames; ++i)
        {
            Debug.Log("Created");
            aiNames[i] = DATA_boss.DATA_objectName + i;
        }
        // Check if the variable matches.
        for (int j = 0; j < duplicatedEnemyNames; ++j)
        {
            if (aiNames[j] == DATA_boss.DATA_LOCALNAME)
            {
                bossObject = gameObject.transform;
                bossObject.name = DATA_boss.DATA_LOCALNAME;
                mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform;
                itemDisplayUI = GameObject.Find(bossObject.name).transform.Find("Canvas").transform;

                damageViewPosition = GameObject.Find(bossObject.name).transform.Find("damageView").transform;
                displayUIObject = itemDisplayUI.gameObject;
                bossText = itemDisplayUI.transform.Find("Slider").Find("Text").GetComponent<Text>();
                updated_HealthUIObject = GameObject.Find(bossObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
                // more preferences
                playerDetectionRange = DATA_boss.DATA_playerDetectionRange;
                coreDetectionRange = DATA_boss.DATA_AIenemyDetectionRange;
                bossHealth = DATA_boss.DATA_enemyHealth;

                updated_HealthUIObject.maxValue = bossHealth;
                updated_HealthUIObject.minValue = 0f;
                updated_HealthUIObject.value = bossHealth;

                coreDetectionRange = DATA_boss.DATA_coreDetectionRangeAI;
                coreMeleeRange = DATA_boss.DATA_coreMeleeRangeAI;
                coreTurnSpeed = DATA_boss.DATA_coreTurnSpeedAI;
                coreMovementSpeed = DATA_boss.DATA_coreMoveSpeedAI;
                coreShootRange = DATA_boss.DATA_coreShootRangeAI;
                coreBackRadius = DATA_boss.DATA_backRadius;
                bossText.text = bossObject.name;
                fixedHealth = bossHealth;
                bossAnimator = GameObject.Find(bossObject.name).transform.Find("Soldier_demo").transform.GetComponent<Animator>();
                //  mainCollider = GameObject.Find(bossObject.name).transform.GetComponent<CapsuleCollider>();
                //  mainCollider.enabled = true;
                displayUIObject.SetActive(false);
                // IdentifyCoreTargets();
                //DeactiveRagdoll();
            }

        }

            bossObject = gameObject.transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        itemDisplayUI = GameObject.Find(bossObject.name).transform.Find("Canvas").transform.Find("Slider").transform;
        damageViewPosition = GameObject.Find(bossObject.name).transform.Find("damageView").transform;
        displayUIObject = itemDisplayUI.gameObject;
        bossText = itemDisplayUI.transform.Find("Text").GetComponent<Text>();

        // more preferences
        updated_HealthUIObject = GameObject.Find(bossObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
        bossText.text = bossObject.name;
        displayUIObject.SetActive(false);
    }
    public void interactiveDisplay()
    {
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    public void sightOnPlayer()
    {
        if (isPlayerDetected)
        {

        }
        else if (!isPlayerDetected)
        {

        }
    }
    public void sightOnCorpse()
    {
        if (isCorpseDetected)
        {

        }
        else if(!isCorpseDetected)
        {

        }
    }
    public void getTargetWithTransform()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject[] corpses = GameObject.FindGameObjectsWithTag("Corpse");

        //Get final distance for the gameobject.
        float coreShortestDistanceAI = Mathf.Infinity;
        GameObject nearest_object = null;

        float distanceToBack = Vector3.Distance(bossObject.transform.position, coreBackAI.transform.position);

        // those are enemies, with types
        // this is for the player object detections.
        foreach(GameObject player in players)
        {
            float distanceToObject = Vector3.Distance(bossObject.transform.position, player.transform.position);

            if(distanceToObject < coreShortestDistanceAI)
            {
                coreShortestDistanceAI = distanceToObject;
                nearest_object = player;
            }
        }    
        // ally detections 
        foreach(GameObject ally in allies)
        {
            float distanceToObject = Vector3.Distance(bossObject.transform.position, ally.transform.position);
           
            if(distanceToObject < coreShortestDistanceAI)
            {
                coreShortestDistanceAI = distanceToObject;
                nearest_object = ally;
            }
        }
        //corpse detections
        foreach (GameObject corpse in corpses)
        {
            float distanceToObject = Vector3.Distance(bossObject.transform.position, corpse.transform.position);

            if (distanceToObject < coreShortestDistanceAI)
            {
                coreShortestDistanceAI = distanceToObject;
                nearest_object = corpse;
            }
        }

        // identifying the objects

        if(nearest_object != null && coreShortestDistanceAI <= coreDetectionRange)
        {
         // Debug.Log("detecting player object");
            Target = nearest_object.transform;
            // TRUE
            LookAttackPlayer = false;
            LookAtObject = true;
            LookShootPlayer = false;

            LookAtBack = false;
            LookAtFront = false;
          if(coreShortestDistanceAI <= coreShootRange)
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
            
            if(!isBossDead && distanceToBack <= coreBackRadius)
            {
                LookAtFront = true;
                LookAtBack = false;
            }
            Target = coreBackAI.transform;
        }  
                

    }

    public void OnMouseEnter()
    {
        displayUIObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        if(!ignoredUI)
        {
            displayUIObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // check if any dead corpses are founded
        // isCorpseDetected = Physics.CheckSphere(bossObject.transform.position, corpseDetectionRange, corpseMask);
        // isPlayerDetected = Physics.CheckSphere(bossObject.transform.position, playerDetectionRange, playerMask);
        // sightOnPlayer();
        // sightOnCorpse();
        // Behaviour status
        interactiveDisplay();
        if (!isBossDead) getTargetWithTransform();
        // Old detection system but it can 
        if (!isBossDead && LookAtObject) { coreLookAtObject(Target.transform); coreWalkForward(); }
        if (!isBossDead && LookAtBack) { coreLookAtBack(); coreWalkForward(); }
        if (!isBossDead && LookAtFront) { coreLookAtFront(); }
        // Attacking status.
        if(!isBossDead && LookShootPlayer) coreLookAtObject(Target.transform);
        if (!isBossDead && LookShootPlayer && !shootAllowed) { StartCoroutine(coreShoot(1f)); shootAllowed = true; }
        // this property only happen once.
        if(!isBossDead && LookShootPlayer) coreLookAtObject(Target.transform);
        if (!isBossDead && LookAttackPlayer && !attackAllowed){ StartCoroutine(coreAttack(1f)); attackAllowed = true; }
        // animation status SMART AI CONDITIONS NOW
        if (!isBossDead && LookAtObject || LookAtBack) bossAnimator.SetBool("isWalking", true);
        else bossAnimator.SetBool("isWalking", false);
      //  if (!isBossDead && LookAtBack) bossAnimator.SetBool("isWalking", true);
      //  else bossAnimator.SetBool("isWalking", false);
      //  if (!isBossDead && LookAtFront) bossAnimator.SetBool("isWalking", true);
      //  else bossAnimator.SetBool("isWalking", false);
        if (!isBossDead && LookAttackPlayer) bossAnimator.SetBool("isAttacking", true);
        else bossAnimator.SetBool("isAttacking", false);
        if (!isBossDead && LookShootPlayer) bossAnimator.SetBool("isShooting", true);
        else bossAnimator.SetBool("isShooting", false);
    }
    public IEnumerator coreAttack(float perAttackSecond)
    {
        
        RaycastHit meleeHit;
        Ray meleeSight = new Ray(coreRayView.transform.position, coreRayView.transform.forward);
        Debug.DrawRay(coreRayView.transform.position, coreRayView.transform.forward, Color.red);
        Debug.Log("coreAttack");
        if (Physics.Raycast(meleeSight, out meleeHit, coreMeleeRange))
        {
            switch(meleeHit.collider.tag)
            {
                case "Player":
                    {
                        //send transform attack message.
                        meleeHit.transform.SendMessage("playerTakeDamage", 3f);
                        break;
                    }
                case "Ally":
                    {
                        meleeHit.transform.SendMessage("enemyTakeDamage", 3f);
                        break;
                    }
            }
        }
      
        yield return new WaitForSeconds(perAttackSecond);
        attackAllowed = false;
        StopCoroutine(coreAttack(perAttackSecond));
    }
    public IEnumerator coreShoot(float perShootSecond)
    {
        RaycastHit shootHit;
        Ray shootSight = new Ray(coreRayView.transform.position, coreRayView.transform.forward);
        Debug.DrawRay(coreRayView.transform.position, coreRayView.transform.forward, Color.blue);
        Debug.Log("Shooting");
        if (Physics.Raycast(shootSight, out shootHit, coreShootRange))
        {
            switch(shootHit.collider.tag)
            {
                case "Player":
                    {
                        shootHit.transform.SendMessage("playerTakeDamage", 3f);
                        break;
                    }
                case "Ally":
                    {
                        shootHit.transform.SendMessage("enemyTakeDamage", 3f);
                        break;
                    }
            }
        }

        yield return new WaitForSeconds(perShootSecond);
        shootAllowed = false;
        StopCoroutine(coreShoot(perShootSecond));
    }
    public void coreWalkForward()
    {
      //  bossAnimator.SetBool("isWalking", true);
        bossObject.transform.position += bossObject.transform.forward * Time.deltaTime * coreMovementSpeed;
    }
    public void coreLookAtObject(Transform Target)
    {

        Vector3 dir = Target.transform.position - bossObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(bossObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        bossObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    public void coreLookAtBack()
    {// walking true
     //   bossAnimator.SetBool("isWalking", true);
        Vector3 dir = coreBackAI.transform.position - bossObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(bossObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        bossObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }
    public void coreLookAtFront()
    {
        Vector3 dir = coreFrontAI.transform.position - bossObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(bossObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        bossObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnDrawGizmos()
    {
        // those are deteciton range fields.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bossObject.transform.position, playerDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bossObject.transform.position, corpseDetectionRange);
        //shooting attributes
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(bossObject.transform.position, coreDetectionRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(bossObject.transform.position, coreShootRange);
        Gizmos.DrawWireSphere(bossObject.transform.position, coreMeleeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coreBackAI.transform.position, coreBackRadius);
        Gizmos.DrawWireSphere(coreFrontAI.transform.position, coreBackRadius);
    }
}
