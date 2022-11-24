using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
public class enemyShooterAI : MonoBehaviour
{
    //Boss AI DATA SECTION

    // core items
    [Header("you must add this settings")]
    [SerializeField] shooterDataAI DATA_shooter;
    [SerializeField] Transform coreBackAI;
    [SerializeField] Transform coreFrontAI;
    [SerializeField] float coreBackRadius;
    [SerializeField] Transform Target;
    [SerializeField] Transform coreRayAttack;
    [SerializeField] Transform coreRayShoot;

    // Trasnform Detection SECTION
    public Transform shooterObject;
    [SerializeField] float playerDetectionRange;
    [SerializeField] bool isPlayerDetected;
    

    // if Corpse founded.

    [SerializeField] float corpseDetectionRange;
    [SerializeField] bool isCorpseDetected;
    [SerializeField] LayerMask corpseMask;

    // UI SECTION
    [SerializeField] bool ignoredUI;
    [SerializeField] Transform damageViewPosition;

    [SerializeField] float shooterHealth;
    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float fixedHealth;
    [SerializeField] float localDamageCount;
    [SerializeField] Transform itemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] Text shooterText;
    // displaying the gameObject
    [SerializeField] GameObject displayUIObject;
    // Range field properties STATUS properties DETECTION, SHOOT, MELEE
    [SerializeField] float coreDetectionRange;
    [SerializeField] float coreShootRange;
    [SerializeField] float coreMeleeRange;
    [SerializeField] float coreMovementSpeed;
    [SerializeField] float coreTurnSpeed;

    [SerializeField] float rayShootRange;
    //Animation Section 
    [SerializeField] Animator shooterAnimator;
    // Start is called before the first frame update


    //Additional attributes.
    [SerializeField] bool isDead;

    //behaviour scripts
    [SerializeField] bool LookAtObject;
    [SerializeField] bool LookAtBack;
    [SerializeField] bool LookAtFront;
    [SerializeField] bool LookShootPlayer;
    [SerializeField] bool LookAttackPlayer;
    [SerializeField] bool LookReloading;
    // is it allowed ? relates to animation status.
    [SerializeField] bool attackAllowed;
    [SerializeField] bool shootAllowed;
    [SerializeField] bool reloadAllowed;

    //enemy shooter damage clips
    [SerializeField] float shootPerRate;
    [SerializeField] float attackPerRate;
    [SerializeField] float reloadPerRate;
    [SerializeField] int clipCounts;
    [SerializeField] int bulletCounts;
    [SerializeField] int totalBullets;
    [SerializeField] float spread;
    //Test outputs

    //SYSTEM VFX
    [SerializeField] GameObject gunClipsPrefab;
    [SerializeField] GameObject gunBulletPrefab;
    [SerializeField] Transform gunClipPosition;
    [SerializeField] Transform gunBulletPosition;

    [SerializeField] LineRenderer gunSmokeRailPrefab;
    [SerializeField] float shooterDamage;

    //Finding duplicated name systems.
    [SerializeField] int duplicatedEnemyNames;
    [SerializeField] string[] aiNames;

    [SerializeField] CapsuleCollider mainCollider;
    [SerializeField] Rigidbody mainRigidBody;

    // new System implemented.
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask variableMask;
    public LayerMask targets;
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        // system preferecne.
        GameObject smokeObject = Resources.Load("ShootPrefab/smokeRailPrefab") as GameObject;
        gunSmokeRailPrefab = smokeObject.GetComponent<LineRenderer>();

        gunClipsPrefab = Resources.Load("ShootPrefab/Clip") as GameObject;
        gunBulletPrefab = Resources.Load("ShootPrefab/BulletShell") as GameObject;
        aiNames = new string[duplicatedEnemyNames];
        for (int i = 0; i < duplicatedEnemyNames; ++i)
        {
            Debug.Log("Created");
            aiNames[i] = DATA_shooter.DATA_objectName + i;
        }
        // Check if the variable matches.
        for (int j = 0; j < duplicatedEnemyNames; ++j)
        {
            if (aiNames[j] == DATA_shooter.DATA_LOCALNAME)
            {
                shooterObject = gameObject.transform;
                shooterObject.name = DATA_shooter.DATA_LOCALNAME;
                mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform;
                itemDisplayUI = GameObject.Find(shooterObject.name).transform.Find("Canvas").transform;

                damageViewPosition = GameObject.Find(shooterObject.name).transform.Find("damageView").transform;
                displayUIObject = itemDisplayUI.gameObject;
                shooterText = itemDisplayUI.transform.Find("Slider").Find("Text").GetComponent<Text>();
                updated_HealthUIObject = GameObject.Find(shooterObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
                // more preferences
                playerDetectionRange = DATA_shooter.DATA_playerDetectionRange;
                coreDetectionRange = DATA_shooter.DATA_AIenemyDetectionRange;
                shooterHealth = DATA_shooter.DATA_enemyHealth;

                updated_HealthUIObject.maxValue = shooterHealth;
                updated_HealthUIObject.minValue = 0f;
                updated_HealthUIObject.value = shooterHealth;

                coreDetectionRange = DATA_shooter.DATA_coreDetectionRangeAI;
                coreMeleeRange = DATA_shooter.DATA_coreMeleeRangeAI;
                coreTurnSpeed = DATA_shooter.DATA_coreTurnSpeedAI;
                coreMovementSpeed = DATA_shooter.DATA_coreMoveSpeedAI;
                coreShootRange = DATA_shooter.DATA_coreShootRangeAI;
                coreBackRadius = DATA_shooter.DATA_backRadius;
                shooterText.text = shooterObject.name;
                fixedHealth = shooterHealth;
                shooterAnimator = GameObject.Find(shooterObject.name).transform.Find("Soldier_demo").transform.GetComponent<Animator>();
                //  mainCollider = GameObject.Find(shooterObject.name).transform.GetComponent<CapsuleCollider>();
                //  mainCollider.enabled = true;
                displayUIObject.SetActive(false);
              // IdentifyCoreTargets();
                DeactiveRagdoll();
            }
        }






      // shooterObject = gameObject.transform;
      // mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
      // itemDisplayUI = GameObject.Find(shooterObject.name).transform.Find("Canvas").transform.Find("Slider").transform;
      // damageViewPosition = GameObject.Find(shooterObject.name).transform.Find("damageView").transform;
      // displayUIObject = itemDisplayUI.gameObject;
      // bossText = itemDisplayUI.transform.Find("Text").GetComponent<Text>();

        // more preferences
       // updated_HealthUIObject = GameObject.Find(shooterObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
       // bossText.text = shooterObject.name;
       // displayUIObject.SetActive(false);
    }
    public void interactiveDisplay()
    {
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    public void ActiveRagdoll()
    {
        shooterAnimator.enabled = false;
        mainCollider.enabled = false;
        // bug fixes.
        mainRigidBody = GameObject.Find(shooterObject.name).transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = true;
        mainRigidBody.detectCollisions = false;
        Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;

        ragDollColliders = shooterObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = shooterObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;

        }
        foreach (Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = false;

        }
        Invoke("DestoryWithEffect", 5f);
    }
    public void DestoryWithEffect()
    {
        //  Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;
        ragDollRigidBodys = shooterObject.transform.GetComponentsInChildren<Rigidbody>();
        //  ragDollColliders = shooterObject.transform.GetComponentsInChildren<Collider>();
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

        ragDollColliders = shooterObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = shooterObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = true;
        }
        mainCollider = GameObject.Find(shooterObject.name).transform.GetComponent<CapsuleCollider>();
        mainCollider.enabled = true;
        mainRigidBody = GameObject.Find(shooterObject.name).transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = false;
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void getTargetWithTransform()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject[] corpses = GameObject.FindGameObjectsWithTag("Corpse");

        //Get final distance for the gameobject.
        float coreShortestDistanceAI = Mathf.Infinity;
        GameObject nearest_object = null;

        float distanceToBack = Vector3.Distance(shooterObject.transform.position, coreBackAI.transform.position);

        // those are enemies, with types
        // this is for the player object detections.
        // NEW SYSTEM This Mechanism needs to be rechanged.
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(shooterObject.transform.position, viewRadius, targets);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTaget = (target.position - transform.position).normalized;
            if(Vector3.Angle(shooterObject.transform.forward, dirToTaget) < viewAngle /2)
            {
                float dstToTarget = Vector3.Distance(shooterObject.transform.position, target.position);
                if(dstToTarget < coreShortestDistanceAI)
                {
                    coreShortestDistanceAI = dstToTarget;
                   // nearest_object = targetsInViewRadius[0].gameObject;
                }

                if(!Physics.Raycast(transform.position,dirToTaget,dstToTarget,variableMask))
                {
                    
                    visibleTargets.Add(target);
                    nearest_object = targetsInViewRadius[0].gameObject;
                }
            }
        }

        // identifying the objects

        if (nearest_object != null && coreShortestDistanceAI <= coreDetectionRange)
        {
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
                //Shooting new mechanics
                if (!LookReloading) { LookShootPlayer = true; }
                if (bulletCounts <= 0) { LookReloading = true; }
                else LookReloading = false;
                LookAtBack = false;
                LookAtFront = false;
                if (coreShortestDistanceAI <= coreMeleeRange)
                {
                    //Target = nearest_object.transform;
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
            LookShootPlayer = false;
            if (!isDead && distanceToBack <= coreBackRadius)
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
        if (!ignoredUI)
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
        if (!isDead) getTargetWithTransform();
        // Old detection system but it can 
        if (!isDead && LookAtObject) { coreLookAtObject(Target.transform); coreWalkForward(); }
        if (!isDead && LookAtBack) { coreLookAtBack(); coreWalkForward(); }
        if (!isDead && LookAtFront) { coreLookAtFront(); }
        // Attacking status.
       
        // we need to check the bullet counts
        if (!isDead && LookShootPlayer && !shootAllowed && bulletCounts != 0) { StartCoroutine(coreShoot(shootPerRate)); shootAllowed = true; }
        // this property only happen once.
        if (!isDead && LookReloading && !reloadAllowed && bulletCounts <= 0) { StartCoroutine(coreReload(reloadPerRate)); reloadAllowed = true; };
        if (!isDead && LookShootPlayer) coreLookAtObject(Target.transform);
        if (!isDead && LookAttackPlayer) coreLookAtObject(Target.transform);
        if (!isDead && LookAttackPlayer && !attackAllowed) { StartCoroutine(coreAttack(attackPerRate)); attackAllowed = true; }
        // animation status SMART AI CONDITIONS NOW
        if (!isDead && LookAtObject || LookAtBack) shooterAnimator.SetBool("isWalking", true);
        else shooterAnimator.SetBool("isWalking", false);
        //  if (!isBossDead && LookAtBack) bossAnimator.SetBool("isWalking", true);
        //  else bossAnimator.SetBool("isWalking", false);
        //  if (!isBossDead && LookAtFront) bossAnimator.SetBool("isWalking", true);
        //  else bossAnimator.SetBool("isWalking", false);
        if (!isDead && LookReloading) shooterAnimator.SetBool("isReloading", true);
        else shooterAnimator.SetBool("isReloading", false);
        if (!isDead && LookAttackPlayer) { shooterAnimator.SetBool("isAttacking", true);}
        else shooterAnimator.SetBool("isAttacking", false);
        if (!isDead && LookShootPlayer) shooterAnimator.SetBool("isShooting", true);
        else shooterAnimator.SetBool("isShooting", false);
    }
    public IEnumerator coreAttack(float perAttackSecond)
    {

        RaycastHit meleeHit;
        Ray meleeSight = new Ray(coreRayAttack.transform.position, coreRayAttack.transform.forward);
        Debug.DrawRay(coreRayAttack.transform.position, coreRayAttack.transform.forward, Color.red);
        Debug.Log("coreAttack");
        if (Physics.Raycast(meleeSight, out meleeHit, coreMeleeRange))
        {
            switch (meleeHit.collider.tag)
            {
                case "Player":
                    {
                        //send transform attack message.
                        meleeHit.transform.SendMessage("playerTakeDamage", 100f);
                        break;
                    }
                case "Ally":
                    {
                        meleeHit.transform.SendMessage("enemyTakeDamage", 100f);
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
        Ray shootSight = new Ray(coreRayShoot.transform.position, coreRayShoot.transform.forward);
        Debug.DrawRay(coreRayShoot.transform.position, coreRayShoot.transform.forward * rayShootRange, Color.blue);
        Debug.Log("Shooting");
        if (Physics.Raycast(shootSight, out shootHit, rayShootRange))
        {
            switch (shootHit.collider.tag)
            {
                case "Player":
                    {
                        shootHit.transform.SendMessage("playerTakeDamage", shooterDamage);
                        LineShootEffect(shootHit);
                        break;
                    }
                case "Ally":
                    {
                        shootHit.transform.SendMessage("enemyTakeDamage", shooterDamage);
                        LineShootEffect(shootHit);
                        break;
                    }
                case "Untagged":
                    {
                        LineShootEffect(shootHit);
                        break;
                    }
                case "BARRIER":
                    {
                        shootHit.transform.SendMessage("enemyTakeDamage", shooterDamage);
                        LineShootEffect(shootHit);
                        break;
                    }
                // Experimental
                case "Enemy":
                    {
                        shootHit.transform.SendMessage("enemyTakeDamage", shooterDamage);
                        LineShootEffect(shootHit);
                        break;
                    }
            }
        }
        //reset Trasnforms
        coreRayShoot.transform.localEulerAngles = Vector3.zero;
        // implement random-Shooting
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        // access Quaternion your script is different from other ones
        // Quaternion shootSpread = coreRayShoot.transform.rotation;
        // shootSpread += new Quaternion(x, y, 0, 0).eulerAngles;
        coreRayShoot.transform.rotation *= Quaternion.Euler(x, y, 0f);
        //   coreRayShoot.transform.position = shootSpread;
        // SYSTEM VFX
        GameObject gunBullet = (GameObject)Instantiate(gunBulletPrefab, gunBulletPosition);
        gunBullet.name = gameObject.name +"Shell" + bulletCounts;
        gunBulletPosition.DetachChildren();
        Rigidbody gunBulletRigid = GameObject.Find(gunBullet.name).GetComponent<Rigidbody>();
        int random = Random.Range(30, 50);
        gunBulletRigid.AddForce(shooterObject.transform.up * random + shooterObject.transform.right * random);
        StartCoroutine(gunBulletDelete(gunBullet.name, 4f));
        bulletCounts--;
        yield return new WaitForSeconds(perShootSecond);
        shootAllowed = false;
        StopCoroutine(coreShoot(perShootSecond));
    }
    public IEnumerator coreReload(float perReloadSecond)
    {
      
        yield return new WaitForSeconds(perReloadSecond);
        if (clipCounts > 0)
        {
            bulletCounts = totalBullets;
            clipCounts--;
            GameObject gunClip = (GameObject)Instantiate(gunClipsPrefab, gunClipPosition);
            gunClip.name = gameObject.name + "Clip" + clipCounts;
            gunClipPosition.DetachChildren();
            StartCoroutine(gunClipDelete(gunClip.name, 4f));
            //SYSTEM VFX
        }
        Debug.Log("Reload happen once");
        reloadAllowed = false;
        StopCoroutine(coreReload(perReloadSecond));
      
    }
    public IEnumerator gunClipDelete(string weaponDataGun, float deleteSeconds)
    {
        GameObject currentWeaponDataGun = GameObject.Find(weaponDataGun).gameObject;
        yield return new WaitForSeconds(deleteSeconds);
        Destroy(currentWeaponDataGun);
        StopCoroutine(gunClipDelete(weaponDataGun, deleteSeconds));
    }
    public IEnumerator gunBulletDelete(string weaponDataGun, float deleteSeconds)
    {
        GameObject currentWeaponDataGun = GameObject.Find(weaponDataGun).gameObject;
        yield return new WaitForSeconds(deleteSeconds);
        Destroy(currentWeaponDataGun);
        StopCoroutine(gunBulletDelete(weaponDataGun, deleteSeconds));
    }

    public void LineShootEffect(RaycastHit hit)
    {
        LineRenderer gunShootEffect = (LineRenderer)Instantiate(gunSmokeRailPrefab, coreRayShoot.position, coreRayShoot.rotation);
        gunShootEffect.name = gameObject.name + "smokeRail" + bulletCounts;
        AudioSource gunAudioSound = gunShootEffect.GetComponent<AudioSource>();
        gunAudioSound.Play();
        gunShootEffect.transform.LookAt(hit.point);
        StartCoroutine(effectAlphaBlind(gunShootEffect, 0.01f, coreRayShoot, hit));
        StartCoroutine(effectDelete(gunShootEffect.name, 2f));
    }
    public void enemyTakeDamage(float damageAmount)
    {
        localDamageCount++; // this is for local damage count so it doesnt merge.
        GameObject damageView = Instantiate(DATA_shooter.DATA_damageStatusObject, damageViewPosition);
        damageView.name = DATA_shooter.DATA_LOCALNAME + "_" + "DamageView" + localDamageCount;
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
        Target = coreBackAI.transform;
        ignoredUI = true;
        displayUIObject.SetActive(true);
        updated_HealthUIObject.value -= damageAmount;
        shooterHealth -= damageAmount;
        if (shooterHealth <= 0)
        {
            Target = coreBackAI.transform;
            isDead = true;
            ignoredUI = false;
            shooterObject.tag = "Untagged";
            displayUIObject.SetActive(false);
            ActiveRagdoll();
            // compoennets
            AudioSource death = gameObject.transform.GetComponent<AudioSource>();
            death.Play();
        }
    }
    public IEnumerator effectDelete(string weaponDataGun, float deleteSeconds)
    {
        GameObject currentGun = GameObject.Find(weaponDataGun).gameObject;
        yield return new WaitForSeconds(deleteSeconds);
        // Debug.Log(currentGun);
        Destroy(currentGun);
        StopCoroutine(effectDelete(weaponDataGun, deleteSeconds));

    }
    public IEnumerator effectAlphaBlind(LineRenderer effectShader, float effectBlindCount, Transform startPosition, RaycastHit endPosition)
    {
        //  effectShader.material = new Material(Shader.Find("Mobile/Particles/Additive"));

        float AlphaSpeed = 6f; //5 
        float alpha = 1f; //1
        //Transform effectTransform = effectShader.transform;
        effectShader.GetComponent<LineRenderer>();
        // effectShader.SetWidth(0,100f);
       // effectTransform.LookAt(endPosition.point);
        effectShader.SetPosition(0, Vector3.zero);
        // if its positive
        if(endPosition.transform.position.z <= 0f)
        {
            effectShader.SetPosition(1, new Vector3(0, 0, -endPosition.transform.position.z));
        } //if its negative
        else if(endPosition.transform.position.z >= -1f)
        {
            effectShader.SetPosition(1, new Vector3(0, 0, endPosition.transform.position.z));
        }
       
        for (int i = 0; i <= 30; i++)
        {
            alpha -= Time.deltaTime * AlphaSpeed;
            //Debug.Log(alpha);
            Color startColor = Color.white;
            Color endColor = Color.black;
            startColor.a = alpha;
            endColor.a = alpha;
            effectShader.SetColors(endColor, startColor);
            yield return new WaitForSeconds(effectBlindCount);
            if (i == 30)
            {
                alpha = 1f;
                //    Debug.Log("done");
               
            }
        }
        StopCoroutine(effectAlphaBlind(effectShader, effectBlindCount, startPosition, endPosition));
    }
    public void coreWalkForward()
    {
        //  bossAnimator.SetBool("isWalking", true);
        shooterObject.transform.position += shooterObject.transform.forward * Time.deltaTime * coreMovementSpeed;
    }
    public void coreLookAtObject(Transform Target)
    {

        Vector3 dir = Target.transform.position - shooterObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(shooterObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        shooterObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    public void coreLookAtBack()
    {// walking true
     //   bossAnimator.SetBool("isWalking", true);
        Vector3 dir = coreBackAI.transform.position - shooterObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(shooterObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        shooterObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }
    public void coreLookAtFront()
    {
        Vector3 dir = coreFrontAI.transform.position - shooterObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(shooterObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        shooterObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void OnDrawGizmos()
    {
        // those are deteciton range fields.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shooterObject.transform.position, playerDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(shooterObject.transform.position, corpseDetectionRange);
        //shooting attributes
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(shooterObject.transform.position, coreDetectionRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shooterObject.transform.position, coreShootRange);
        Gizmos.DrawWireSphere(shooterObject.transform.position, coreMeleeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coreBackAI.transform.position, coreBackRadius);
        Gizmos.DrawWireSphere(coreFrontAI.transform.position, coreBackRadius);
    }
}
