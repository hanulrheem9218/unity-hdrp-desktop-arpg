using UnityEngine;
using System.Collections;

public class SightOut : MonoBehaviour
{

    // --------------------------------------------------
    // Variables:

    [SerializeField] private GameObject itemActivatorObject;
    [SerializeField] private SightSystem activationScript;
    [SerializeField] private bool isUpdateLocation;
    // --------------------------------------------------

    void Start()
    {
        itemActivatorObject = FindObjectOfType<SightSystem>().gameObject;
        activationScript = FindObjectOfType<SightSystem>();

        StartCoroutine("AddToList");
        if (isUpdateLocation)
        {
            InvokeRepeating(nameof(updateLocation), 1f, 2f);
        }
    }

    private void updateLocation()
    {
        if (this.gameObject.activeInHierarchy)
        {
            foreach (SightIn newPos in activationScript.sightIns)
            {
                if (newPos.item.gameObject.name == this.gameObject.name)
                {
                    newPos.itemPos = transform.position;
                }
            }
        }
    }
    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("added to location");
        activationScript.sightIns.Add(new SightIn { item = this.gameObject, itemPos = transform.position });
 
    }
}