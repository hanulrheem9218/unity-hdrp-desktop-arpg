using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npcBehaviours : MonoBehaviour
{
    public IEnumerator moveRandomly(float seconds, bool randomMove, int randomMovingRange)
    {

        //random Geneartor.

        float randomY = Random.Range(-randomMovingRange, randomMovingRange);

        // accessing new transform data


        this.gameObject.transform.rotation = Quaternion.Euler(0f, randomY, 0f);
        //Invoke(nameof(moveRandomly), 3f);
        // change y random values.
        yield return new WaitForSeconds(seconds);
        randomMove = false;
        StopCoroutine(moveRandomly(seconds, randomMove, randomMovingRange));
        Debug.Log("h");



    }

    public void enemyTakeDamage(float damageAmount, int localDamageCount,
        GameObject damageViewObject, Transform damageViewPosition, 
        int fixedHealth, Transform coreTarget, bool ignoreUI, GameObject displayUIObject,
        Transform Target, Slider updated_HealthUIObject, float npcHealth, bool isnpcDead, Transform npcObject, Animator npcAnimator, CapsuleCollider mainCollider, Rigidbody mainRigidBody)
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
            ActiveRagdoll(npcAnimator,mainCollider,mainRigidBody,npcObject);
        }
    }
    public void coreWalkForward(Transform npcObject, float coreMovementSpeed)
    {
        //  npcAnimator.SetBool("isWalking", true);
        npcObject.transform.position += npcObject.transform.forward * Time.deltaTime * coreMovementSpeed;
    }
    public void coreLookAtObject(Transform Target, Transform npcObject, float coreTurnSpeed)
    {

        Vector3 dir = Target.transform.position - npcObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(npcObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        npcObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    public void coreLookAtBack(Transform npcObject, Transform coreTarget, float coreTurnSpeed)
    {// walking true
     //   npcAnimator.SetBool("isWalking", true);
        Vector3 dir = coreTarget.transform.position - npcObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(npcObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        npcObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }
    public void coreLookAtFront(Transform coreFrontAI, Transform npcObject, float coreTurnSpeed)
    {
        Vector3 dir = coreFrontAI.transform.position - npcObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(npcObject.rotation, lookRotation, Time.deltaTime * coreTurnSpeed).eulerAngles;
        npcObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    public void ActiveRagdoll(Animator npcAnimator, CapsuleCollider mainCollider, Rigidbody mainRigidBody, Transform npcObject)
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
    public void DestoryWithEffect(Transform npcObject)
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
    public void DeactiveRagdoll(Transform npcObject, CapsuleCollider mainCollider, Rigidbody mainRigidBody)
    {

        Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;

        ragDollColliders = npcObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = npcObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = true;
        }
        mainCollider = GameObject.Find(npcObject.name).transform.GetComponent<CapsuleCollider>();
        mainCollider.enabled = true;
        mainRigidBody = GameObject.Find(npcObject.name).transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = false;
    }
    public void IdentifyCoreTargets(Transform[] coreTargetPath, Transform coreTarget, int TargetMaxPath)
    {
        coreTargetPath = new Transform[GameObject.FindGameObjectWithTag("TargetPoints").transform.childCount];
        for (int i = 0; i < coreTargetPath.Length; i++)
        {
            coreTargetPath[i] = GameObject.FindGameObjectWithTag("TargetPoints").transform.GetChild(i);
        }
        coreTarget = coreTargetPath[0];
        TargetMaxPath = coreTargetPath.Length;
    }
    public void getTargetWithNextTransform(Transform coreTarget, Transform[] coreTargetPath, bool npcFollowNext, int TargetIndex)
    {
        if (!npcFollowNext)
        {
            coreTarget = coreTargetPath[TargetIndex];
            ++TargetIndex;
        }
    }
    public void getTargetWithPreviousTransform(Transform coreTarget, Transform[] coreTargetPath, int TargetIndex, bool npcFollowPrevious)
    {
        if (!npcFollowPrevious)
        {

            --TargetIndex;
            coreTarget = coreTargetPath[TargetIndex];
        }
    }

    public void interactiveDisplay(Transform itemDisplayUI, Transform mainCamera)
    {
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    public void getTargetWithTransform(Transform npcObject, Transform coreTarget, Transform Target, Transform[] coreTargetPath,
        float coreDetectionRange, float coreShootRange, float coreMeleeRange, float coreBackRadius, float coreMovementSpeed, float seconds,
        int TargetIndex, int TargetMaxPath, int randomMovingRange,
        bool LookAttackPlayer, bool LookAtObject, bool LookShootPlayer, bool LookAtBack, bool LookAtFront, bool isnpcDead, bool initiateFollowPath, bool randomMove, bool npcFollowNext, bool npcFollowReset, bool npcFollowPrevious)
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
                    coreWalkForward(npcObject,coreMovementSpeed);
                    if (!randomMove)
                    {
                        coreMovementSpeed = 0.3f;
                        randomMove = true;

                        StartCoroutine(moveRandomly(seconds, randomMove, randomMovingRange));
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
                        getTargetWithNextTransform(coreTarget,coreTargetPath, npcFollowNext, TargetIndex);
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


                        getTargetWithPreviousTransform(coreTarget,coreTargetPath,TargetIndex,npcFollowPrevious);
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