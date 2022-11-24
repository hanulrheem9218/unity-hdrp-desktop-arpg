using UnityEngine;
using System.Collections;

public class DisableIfFarAway : MonoBehaviour
{

    // --------------------------------------------------
    // Variables:

    [SerializeField] private GameObject itemActivatorObject;
    [SerializeField] private ItemActivator activationScript;

    // --------------------------------------------------

    void Start()
    {
        itemActivatorObject = FindObjectOfType<ItemActivator>().gameObject;
        activationScript = FindObjectOfType<ItemActivator>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        activationScript.activatorItems.Add(new ActivatorItem { item = this.gameObject, itemPos = transform.position });
    }
}