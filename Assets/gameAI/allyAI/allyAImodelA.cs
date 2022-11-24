using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class allyAImodelA : MonoBehaviour
{
    // Start is called before the first frame update
    //Additonal properties
    //Additonal properties
    [SerializeField] allyDataAI DATA_ALLYAI;
    //UI Prefabs that displays what type it is.
    public Transform itemDisplayUI;
    public GameObject displayUIObject;
    public Camera mainCamera;
    public Text allyText;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] float playerDetectionRange;
    [SerializeField] LayerMask playerMask;
    [SerializeField] bool isAIenemyDetected;
    [SerializeField] float AIenemyDetectionRange;
    [SerializeField] LayerMask AIenemyMask;
    [SerializeField] Transform allyObject;
    [Header("Enemy Preferences")]
    [SerializeField] float allyHealth;
    [SerializeField] Image allyHealthSprite;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool isDead;
    [SerializeField] Animator allyAnimator;
    // you have to make it better implementation.
    public Transform coreDetectionAI;
    // public Transform coreObjectAI;
    public float coreDetectionRangeAI;
    public float coreMeleeRangeAI;
    public float coreShootRangeAI;
    public float coreTurnSpeedAI;
    public float coreMoveSpeedAI;
    //seperation for Transform Postitions
    [Header("This Must be Filled")]
    public Transform coreBackAI;
    public Transform coreFrontAI;

    //Behaviour Status
    public bool LookAtObject;
    public bool LookAtBack;
    public bool LookAtFront;
    public bool LookShootPlayer;
    // shooting mechanics
    //public bool Shoot;
    //public bool ShootAllowed;
    // Melle Attack mechanics
    public bool attack;
    public bool attackAllowed;
    public float allyAttackTime;
    [SerializeField] Transform rayTransformAI;

    public bool killedConfirmed;
    public float backRadius;
    [SerializeField] int duplicatedEnemyNames;
    [SerializeField] string[] aiNames;
    // updates

    [SerializeField] bool ignoreUI;
    [SerializeField] Transform damageViewPosition;
    [SerializeField] int localDamageCount;

    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float FixedHealth;
    void Start()
    {
        //Create the array with you
        aiNames = new string[duplicatedEnemyNames];
        for (int i = 0; i < duplicatedEnemyNames; ++i)
        {
            Debug.Log("Created");
            aiNames[i] = DATA_ALLYAI.DATA_objectName + i;
        }
        // Check if the variable matches.
        for (int j = 0; j < duplicatedEnemyNames; ++j)
        {
            if (aiNames[j] == DATA_ALLYAI.DATA_LOCALNAME)
            {
                allyObject = gameObject.transform;
                allyObject.name = DATA_ALLYAI.DATA_LOCALNAME;
                mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
                itemDisplayUI = GameObject.Find(allyObject.name).transform.Find("Canvas").transform;

                damageViewPosition = GameObject.Find(allyObject.name).transform.Find("damageView").transform;
                displayUIObject = itemDisplayUI.gameObject;
                allyText = itemDisplayUI.transform.Find("Slider").Find("Text").GetComponent<Text>();
                updated_HealthUIObject = GameObject.Find(allyObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
                // more preferences
                playerDetectionRange = DATA_ALLYAI.DATA_playerDetectionRange;
                AIenemyDetectionRange = DATA_ALLYAI.DATA_AIenemyDetectionRange;
                allyHealth = DATA_ALLYAI.DATA_enemyHealth;
              
                updated_HealthUIObject.maxValue = allyHealth;
                updated_HealthUIObject.minValue = 0f;
                updated_HealthUIObject.value = allyHealth;

                coreDetectionRangeAI = DATA_ALLYAI.DATA_coreDetectionRangeAI;
                coreMeleeRangeAI = DATA_ALLYAI.DATA_coreMeleeRangeAI;
                coreTurnSpeedAI = DATA_ALLYAI.DATA_coreTurnSpeedAI;
                coreMoveSpeedAI = DATA_ALLYAI.DATA_coreMoveSpeedAI;
                coreShootRangeAI = DATA_ALLYAI.DATA_coreShootRangeAI;
                backRadius = DATA_ALLYAI.DATA_bckRadius;
                allyText.text = allyObject.name;
                FixedHealth = allyHealth;
                displayUIObject.SetActive(false);
            }
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckSphere(allyObject.transform.position - new Vector3(0, 0, 0), playerDetectionRange, playerMask);
        isAIenemyDetected = Physics.CheckSphere(allyObject.transform.position - new Vector3(0, 0, 0), AIenemyDetectionRange, AIenemyMask);

        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

        if (isPlayerDetected && !isDead)
        {
            // if player detected
        }
        else if (!isPlayerDetected && !isDead)
        {
            // if player is not detected
        }

        if (allyHealth <= 0 && !isDead)
        {

            //trigger , rigidbody disbale
            Rigidbody allyObjectRigidBody = allyObject.GetComponent<Rigidbody>();
            Collider allyObjectCollider = allyObject.GetComponent<Collider>();
            allyObjectRigidBody.isKinematic = true;
            allyObjectRigidBody.detectCollisions = false;
            allyObjectRigidBody.useGravity = false;
            allyObjectRigidBody.freezeRotation = true;
            allyObjectCollider.isTrigger = true;
            //UI PREFAB if disappear.
            Transform uiTransform = GameObject.Find(allyObject.name).transform.Find("Canvas").transform.Find("Slider").transform;
            Text uiTransformText = uiTransform.transform.Find("Text").transform.GetComponent<Text>();
            Image allyBackground = uiTransform.transform.Find("Background").GetComponent<Image>();
            //    Image enemyFillArea = uiTransform.transform.Find("FillArea").GetComponent<Image>();
            Image allyFill = uiTransform.Find("FillArea").transform.Find("Fill").transform.GetComponent<Image>();
            // Initiate reboot
            allyBackground.CrossFadeAlpha(0, 0.5f, false);
            // enemyFillArea.CrossFadeAlpha(0, 0.8f, false);
            uiTransformText.CrossFadeAlpha(0, 0.5f, false);
            allyFill.CrossFadeAlpha(0, 0.5f, false);

            audioSource.Play();
            ignoreUI = true;
            Debug.Log("dead");
           // Collider disableCollider = allyObject.transform.GetComponent<Collider>();
           // disableCollider.enabled = false;
            allyObject.transform.tag = "Untagged";
            allyObject.gameObject.layer = 0;
            allyAnimator.SetBool("isWalking", true);
            allyAnimator.SetBool("isAttacking", false);
            allyAnimator.SetBool("isDead", true);
            isDead = true;

        }

        // Old Detection system but it can detect transform target.
        if(!isDead)
        {
            getTargetWithTransform();
        }
    
        if (LookAtObject && !isDead)
        {
            coreLookAtObject();
            coreWalkForward();
        }
        if (LookAtFront && !isDead)
        {
            //  animatorAI.SetBool("idleToRun", true);
            coreLookAtFront();
        }
        if (LookAtBack && !isDead)
        {
            //allyAnimator.SetBool("isWalking", true);
            coreLookAtBack();
            coreWalkForward();
        }
        if (LookShootPlayer && !isDead)
        {
           // allyAnimator.SetBool("isAttacking", true);
            //allyAnimator.SetBool("isWalking", false);
            coreLookAtObject();
        }

        if (!attack && attackAllowed && !isDead)
        {
            
           // allyAnimator.SetBool("isWalking", false);
            StartCoroutine(enemyMeleeAttack(allyAttackTime));
            attack = true;
        }
        // for attack animation statement
       if(attack)
        {
            allyAnimator.SetBool("isAttacking", true);
        }
       else
        {
            allyAnimator.SetBool("isAttacking", false);
        }

    }

    public void enemyTakeDamage(float damageAmount)
    {
        localDamageCount++; // this is for local damage count so it doesnt merge.
        GameObject damageView = Instantiate(DATA_ALLYAI.Data_damageStatusObject, damageViewPosition);
        damageView.name = DATA_ALLYAI.DATA_LOCALNAME + "_" + "DamageView" + localDamageCount;
        damageViewPosition.DetachChildren();
        showDamageStatus damageScript = GameObject.Find(damageView.name).transform.GetComponent<showDamageStatus>();
        //damageScript.StartCoroutine(damageScript.destoryDamageStatus01(1f));
        Text damageDisplayAmount = GameObject.Find(damageView.name).transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        // damageDisplayAmount.color = Color.white; // in default damage
        int divided = ((int)FixedHealth / 2);

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
        else if (damageAmount <= FixedHealth)
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
        coreDetectionAI = coreBackAI.transform;
        ignoreUI = true;
        displayUIObject.SetActive(true);
        updated_HealthUIObject.value -= damageAmount;
        allyHealth -= damageAmount;
        if (allyHealth <= 0)
        {
            coreDetectionAI = coreBackAI.transform;
        }
    }
    IEnumerator enemyMeleeAttack(float second)
    {

        yield return new WaitForSeconds(second);
        getMeleeAttack();
        Debug.Log("ai attack");
        attack = false;
        StopCoroutine(enemyMeleeAttack(second));
    }
    public void getTargetWithTransform()
    {
        //   Debug.Log("Active");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // GameObject[] playersAndEnemies = GameObject.FindGameObjectsWithTag("playerAndEnemies")
        float coreShortestDistanceAI = Mathf.Infinity;
        GameObject nearest_object = null;
        // reset back.
       float distanceToBack = Vector3.Distance(allyObject.transform.position, coreBackAI.transform.position);
       // foreach (GameObject player in players)
       // {
       //     float distanceToObject = Vector3.Distance(allyObject.transform.position, player.transform.position);
       //     if (distanceToObject < coreShortestDistanceAI)
       //     {
       //
       //         coreShortestDistanceAI = distanceToObject;
       //         nearest_object = player;
       //     }
       // }

        foreach (GameObject enemy in enemies)
        {
            float distanceToObject = Vector3.Distance(allyObject.transform.position, enemy.transform.position);
            if (distanceToObject < coreShortestDistanceAI)
            {
       
                coreShortestDistanceAI = distanceToObject;
                nearest_object = enemy;
            }
        }
        // identifying the objects
        if (nearest_object != null && coreShortestDistanceAI <= coreDetectionRangeAI)
        {
            // Detected something
            coreDetectionAI = nearest_object.transform;
            LookAtObject = true;
            LookAtBack = false;
            LookAtFront = false;
            LookShootPlayer = false;
            //animatorAI.SetBool("idleToRun", true);
            allyAnimator.SetBool("isAttacking", false);
            // example for detecting short range you can modify it
            if (coreShortestDistanceAI <= coreShootRangeAI)
            {

                // shooting mechanics
                attackAllowed = true;
                LookShootPlayer = true;
                LookAtObject = false;
                if (coreShortestDistanceAI <= coreMeleeRangeAI)
                {
                    Debug.Log("Detects?");
                    LookShootPlayer = false;
                    LookAtObject = false;
                    LookAtBack = false;
                }
            }
            //coreWalkForward();
        }
        else
        {
            LookAtObject = false;
            // keyword 
            LookAtBack = true;
            attackAllowed = false;
            // keyword  Bugfix
            
            // coreDetectionAI = coreBackAI.transform;
           // allyAnimator.SetBool("isWalking", true);
            if (distanceToBack <= backRadius)
            {
                //Debug.Log("works?");
                // active lookat random ?
                allyAnimator.SetBool("isAttacking", false);
                allyAnimator.SetBool("isWalking", false);
                LookAtBack = false;
                LookAtFront = true;
            }
            coreDetectionAI = coreBackAI.transform;
        }


    }
    public void getMeleeAttack()
    {
        RaycastHit hitAI;

        Ray originAI = new Ray(rayTransformAI.transform.position, rayTransformAI.transform.forward);
        Debug.DrawRay(rayTransformAI.transform.position, rayTransformAI.transform.forward, Color.red);
        if (Physics.Raycast(originAI, out hitAI, coreShootRangeAI))
        {
            switch (hitAI.collider.tag)
            {
                case "Player":
                    {
                        hitAI.transform.SendMessage("playerTakeDamage", 3f);
                        break;
                    }
                case "Enemy":
                    {
                        float damage = Random.Range(10f, 2f);
                        hitAI.transform.SendMessage("enemyTakeDamage", damage);
                        break;
                    }
            }
        }
    }
    public void coreLookAtObject()
    {
       // allyAnimator.SetBool("isWalking", true);
        Vector3 dir = coreDetectionAI.transform.position - allyObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(allyObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeedAI).eulerAngles;
        allyObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    public void coreWalkForward()
    {
        allyAnimator.SetBool("isWalking", true);
        allyObject.transform.position += allyObject.transform.forward * Time.deltaTime * coreMoveSpeedAI;
    }
    public void coreLookAtBack()
    {
        allyAnimator.SetBool("isWalking", true);
        Vector3 dir = coreBackAI.transform.position - allyObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(allyObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeedAI).eulerAngles;
        allyObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }
    public void coreLookAtFront()
    {
        Vector3 dir = coreFrontAI.transform.position - allyObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(allyObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeedAI).eulerAngles;
        allyObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(allyObject.transform.position - new Vector3(0, 0, 0), playerDetectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(allyObject.transform.position - new Vector3(0, 0, 0), AIenemyDetectionRange);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(allyObject.transform.position, coreDetectionRangeAI);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(allyObject.transform.position, coreShootRangeAI);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(allyObject.transform.position, coreMeleeRangeAI);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(coreBackAI.transform.position, backRadius);
    }
}
