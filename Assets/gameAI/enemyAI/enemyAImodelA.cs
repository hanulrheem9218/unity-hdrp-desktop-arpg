using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyAImodelA : MonoBehaviour
{
    // Start is called before the first frame update
    //Additonal properties
  
    //UI Prefabs that displays what type it is.
    public Transform itemDisplayUI;
    public GameObject displayUIObject;
    public Camera mainCamera;
    public Text npcText;


    // enemy Object
    [SerializeField] Transform enemyObject;
    [Header("Enemy Preferences")]
    public float enemyHealth;
    [SerializeField] Image enemyHealthSprite;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private bool isEnemyDead;
    [SerializeField] Animator enemyAnimator;
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
    //[SerializeField] isGoingBack

    //Behaviour Status
    public bool LookAtObject;
    //[SerializeField] bool LookAtObjectAndMoveNotAllowed;
    public bool LookAtBack;
    public bool LookAtFront;
    public bool LookShootPlayer;
    public bool LookAttackPlayer;
    private bool visionFull;
    // shooting mechanics
    //public bool Shoot;
    //public bool ShootAllowed;
    // Melle Attack mechanics
    public bool attack;
    public bool attackAllowed;
    public float enemyAttackTime;
    [SerializeField] Transform rayTransformAI;

    public bool killedConfirmed;
    [SerializeField] int duplicatedEnemyNames;
    [SerializeField] string[] aiNames;
    public float backRadius;

    //fix death motions
    //[SerializeField] GameObject currentObjectDeath;
    [SerializeField] bool ignoreUI;
    [SerializeField] Transform damageViewPosition;
    public GameObject damageStatusObject;
    [SerializeField] int localDamageCount;
    //[SerializeField] Canvas localCanvas;

    // Updated UI Health;
    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float FixedHealth;

    void Start()
    {
        //Create the array with you

        // Check if the variable matches.
        enemyObject = gameObject.transform;
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        itemDisplayUI = GameObject.Find(enemyObject.name).transform.Find("Canvas").transform.Find("Slider").transform;
       // localCanvas = GameObject.Find(enemyObject.name).transform.Find("Canvas").GetComponent<Canvas>();
        damageViewPosition = GameObject.Find(enemyObject.name).transform.Find("damageView").transform;
        displayUIObject = itemDisplayUI.gameObject;
        npcText = itemDisplayUI.transform.Find("Text").GetComponent<Text>();
        // more preferences
        updated_HealthUIObject = GameObject.Find(enemyObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
       
        updated_HealthUIObject.maxValue = enemyHealth;
        updated_HealthUIObject.minValue = 0f;
        updated_HealthUIObject.value = enemyHealth;
       
        npcText.text = enemyObject.name;
        FixedHealth = enemyHealth;
        displayUIObject.SetActive(false);
       
        InvokeRepeating(nameof(getTargetWithTransform), 0, 2f);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //interactiveDisplay();
        //Logic
        if (this.gameObject.activeInHierarchy &&!isEnemyDead && visionFull) { getTargetWithTransform(); interactiveDisplay(); }

        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookAtObject) { coreLookAtObject(); coreWalkForward(); }
        else LookAtObject = false;
        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookAtBack) { coreLookAtBack(); coreWalkForward(); }
        else LookAtBack = false;
        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookAtFront) { coreLookAtFront(); }
        else LookAtFront = false;
        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookShootPlayer) coreLookAtObject();
        else LookShootPlayer = false;
        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookAttackPlayer && !attackAllowed) { StartCoroutine(enemyMeleeAttack(enemyAttackTime)); attackAllowed = true; }
        else LookAttackPlayer = false;
        // animation status

        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookAttackPlayer) { enemyAnimator.SetBool("isAttacking", true); coreLookAtObject(); }
        else enemyAnimator.SetBool("isAttacking", false);

        if (this.gameObject.activeInHierarchy && !isEnemyDead && LookAtObject || !isEnemyDead && LookAtBack) enemyAnimator.SetBool("isWalking", true);
        else enemyAnimator.SetBool("isWalking", false);

        // if enemy dies.
        if (this.gameObject.activeInHierarchy && isEnemyDead) { enemyAnimator.SetBool("isDead", true);
        } else enemyAnimator.SetBool("isDead", false);
    }
    private void interactiveDisplay()
    {
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    private void deadWithAnimation()
    {
        // this is for temporary settings
        //trigger , rigidbody disbale
        isEnemyDead = true;
      
        //UI PREFAB if disappear.
      
        Transform uiTransform = GameObject.Find(enemyObject.name).transform.Find("Canvas").transform.Find("Slider").transform;
        Text uiTransformText = uiTransform.transform.Find("Text").transform.GetComponent<Text>();
        Image enemyBackground = uiTransform.transform.Find("Background").GetComponent<Image>();
        //    Image enemyFillArea = uiTransform.transform.Find("FillArea").GetComponent<Image>();
        Image enenyFill = uiTransform.Find("FillArea").transform.Find("Fill").transform.GetComponent<Image>();
        // Initiate reboot
        enemyBackground.CrossFadeAlpha(0, 0.5f, false);
        // enemyFillArea.CrossFadeAlpha(0, 0.8f, false);
        uiTransformText.CrossFadeAlpha(0, 0.5f, false);
        enenyFill.CrossFadeAlpha(0, 0.5f, false);

        Rigidbody enemyObjectRigidBody = enemyObject.GetComponent<Rigidbody>();
        Collider enemyObjectCollider = enemyObject.GetComponent<Collider>();
        enemyObjectRigidBody.isKinematic = true;
        enemyObjectRigidBody.detectCollisions = false;
        enemyObjectRigidBody.useGravity = false;
        enemyObjectRigidBody.freezeRotation = true;
        enemyObjectCollider.isTrigger = true;
        audioSource.Play();
        // send message a isDead(true) to the player.
        InputSystem player = FindObjectOfType<InputSystem>();
        player.isEnemyClicked = false;
        Debug.Log("dead");

        //  Collider disableCollider = enemyObject.transform.GetComponent<Collider>();
        //  disableCollider.enabled = false;
        enemyObject.transform.tag = "Untagged";
        enemyObject.gameObject.layer = 0;
        //Reset everything
        Invoke(nameof(stopAnimation), 4f);
        uiTransform.gameObject.SetActive(false);   
    }
    private void stopAnimation()
    {
        enemyAnimator.enabled = false;
    }
    public void enemyTakeDamage(float damageAmount)
    {
       
            localDamageCount++; // this is for local damage count so it doesnt merge.
            GameObject damageView = Instantiate(damageStatusObject, damageViewPosition);
            damageView.name = this.gameObject.name + "_" + "DamageView" + localDamageCount;
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
            else if(damageAmount <= 75f)
            {
                damageScript.StartCoroutine(damageScript.destoryDamageStatus02(1f));
                damageDisplayAmount.text = damageAmount.ToString("0");
                damageDisplayAmount.color = Color.yellow; // Mid Damage

            }
            else if(damageAmount <= FixedHealth)
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

            
            displayUIObject.SetActive(true);
            ignoreUI = true;
            updated_HealthUIObject.value -= damageAmount;
            enemyHealth -= damageAmount;

            if (enemyHealth <= 0)
            {
                deadWithAnimation();
                coreDetectionAI = coreBackAI.transform;
            }
            

        
    }
    IEnumerator enemyMeleeAttack(float second)
    {
        InputSystem checkPlayer = FindObjectOfType<InputSystem>();
        if(checkPlayer.isPlayerDead)
        {
            LookAttackPlayer = false;
        }
        yield return new WaitForSeconds(second);
        getMeleeAttack();
        Debug.Log("ai attack");
        attackAllowed = false;
        StopCoroutine(enemyMeleeAttack(second));
    }
    private void OnMouseEnter()
    {
        displayUIObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        if(!ignoreUI)
        {
            displayUIObject.SetActive(false);
        }
      
    }
    private void getTargetWithTransform()
    {
        if (this.gameObject.activeInHierarchy)
        {
            //   Debug.Log("Active");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");

            float coreShortestDistanceAI = Mathf.Infinity;
            GameObject nearest_object = null;
            // reset back.
            float distanceToBack = Vector3.Distance(enemyObject.transform.position, coreBackAI.transform.position);


            foreach (GameObject player in players)
            {
                float distanceToObject = Vector3.Distance(enemyObject.transform.position, player.transform.position);
                // Debug.Log(distanceToObject);
                if (distanceToObject < coreShortestDistanceAI)
                {

                    coreShortestDistanceAI = distanceToObject;
                    nearest_object = player;
                }
            }

            foreach (GameObject ally in allies)
            {
                float distanceToObject = Vector3.Distance(enemyObject.transform.position, ally.transform.position);
                //  Debug.Log(distanceToObject);
                if (distanceToObject < coreShortestDistanceAI)
                {

                    coreShortestDistanceAI = distanceToObject;
                    nearest_object = ally;
                }
            }
            // identifying the objects

            if (nearest_object != null && coreShortestDistanceAI <= coreDetectionRangeAI && !isEnemyDead)
            {
                // first Setup
                if (!visionFull)
                {
                    visionFull = true;
                    CancelInvoke(nameof(getTargetWithTransform));
                }
                // Detected something
                coreDetectionAI = nearest_object.transform;
                LookAttackPlayer = false;
                LookAtObject = true;
                LookShootPlayer = false;

                LookAtBack = false;
                LookAtFront = false;
                //animatorAI.SetBool("idleToRun", true);
                // example for detecting short range you can modify it
                if (coreShortestDistanceAI <= coreShootRangeAI && !isEnemyDead)
                {

                    // shooting mechanics
                    LookAttackPlayer = true;
                    LookAtObject = false;
                    LookShootPlayer = false;

                    LookAtBack = false;
                    LookAtFront = false;
                    if (coreShortestDistanceAI <= coreMeleeRangeAI && !isEnemyDead)
                    {
                        LookAttackPlayer = true;
                        LookAtObject = false;
                        LookShootPlayer = false;

                        LookAtBack = false;
                        LookAtFront = false;
                    }
                }
                //coreWalkForward();
            }
            else
            {
                LookAtObject = false;
                LookAtBack = true;
                if (!isEnemyDead && distanceToBack <= backRadius)
                {

                    // active lookat random ?
                    LookAtBack = false;
                    LookAtFront = true;
                }
                coreDetectionAI = coreBackAI.transform;
            }

        }
    }
    private void getMeleeAttack()
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
                case "Ally":
                    {
                        hitAI.transform.SendMessage("enemyTakeDamage", 3f);
                   

                        break;
                    }
            }
        }
    }
   private void coreLookAtObject()
    {
            Vector3 dir = coreDetectionAI.transform.position - enemyObject.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(enemyObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeedAI).eulerAngles;
            enemyObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void coreWalkForward()
    {
        enemyObject.transform.position += enemyObject.transform.forward * Time.deltaTime * coreMoveSpeedAI;
    }
    private void coreLookAtBack()
    {// walking true
        Vector3 dir = coreBackAI.transform.position - enemyObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(enemyObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeedAI).eulerAngles;
        enemyObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }
    private void coreLookAtFront()
    {
        Vector3 dir = coreFrontAI.transform.position - enemyObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(enemyObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeedAI).eulerAngles;
        enemyObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnDrawGizmos()
    {
   
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(enemyObject.transform.position, coreDetectionRangeAI);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyObject.transform.position, coreShootRangeAI);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(enemyObject.transform.position, coreMeleeRangeAI);


        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(coreBackAI.transform.position, backRadius);
    }
}
