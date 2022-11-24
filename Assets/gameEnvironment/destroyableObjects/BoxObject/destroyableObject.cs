using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class destroyableObject : MonoBehaviour
{
    [SerializeField] GameObject currentObject;
    [SerializeField] GameObject effectObject;
    [SerializeField] BoxCollider currentCollider;

    // Start is called before the first frame update
    void Start()
    {
        // delete current game object.
        currentObject = gameObject;
        effectObject = gameObject.transform.Find("effect").gameObject;
        currentCollider = gameObject.transform.GetComponent<BoxCollider>();
        effectObject.SetActive(false);
        
    }

    // Update is called once per frame
    public void fracturedObject()
    {
        Invoke(nameof(activeFracturedObject), 0.1f);
        Invoke(nameof(disableFractureObject), 2f);
    }
    public void activeFracturedObject()
    {
        // showing object compoennt.
        Collider currentCollider = currentObject.GetComponent<Collider>();
        MeshRenderer renderer = currentObject.GetComponent<MeshRenderer>();
        renderer.enabled = false;
        currentCollider.enabled = false;
        effectObject.SetActive(true);

        MeshCollider[] allComponents = effectObject.GetComponentsInChildren<MeshCollider>();
        Rigidbody[] rigidComponents = effectObject.GetComponentsInChildren<Rigidbody>();
        foreach(MeshCollider col in allComponents)
        {
            col.enabled = true;
        }
        foreach(Rigidbody rigid in rigidComponents)
        {
            rigid.isKinematic = false;
        }
        BoxCollider mainCollider = currentObject.transform.GetComponent<BoxCollider>();
        mainCollider.enabled = true;
        mainCollider.isTrigger = true;
        Rigidbody mainRigidBody = currentObject.transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = false;
        
        
    }
    public void disableFractureObject()
    {
        MeshCollider[] allComponents = effectObject.GetComponentsInChildren<MeshCollider>();
        Rigidbody[] rigidComponents = effectObject.GetComponentsInChildren<Rigidbody>();
        foreach(MeshCollider col in allComponents)
        {
            col.enabled = false;
        }

        foreach(Rigidbody rigid in rigidComponents)
        {
            rigid.isKinematic = true;
        }
        BoxCollider mainCollider = currentObject.transform.GetComponent<BoxCollider>();
        mainCollider.enabled = true;
        Rigidbody mainRigidBody = currentObject.transform.GetComponent<Rigidbody>();
        mainRigidBody.isKinematic = true;
       // mainRigidBody.detectCollisions = false;
     //   Destroy(currentObject);
    }

}
