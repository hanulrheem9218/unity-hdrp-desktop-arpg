
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SightSystem : MonoBehaviour
{

    // --------------------------------------------------
    // Variables:

    [SerializeField]
    private int distanceFromPlayer;
    [SerializeField]
    private GameObject playerObject;

    public List<SightIn> sightIns;

    // --------------------------------------------------

    void Start()
    {
        playerObject = GameObject.Find("player");
        sightIns = new List<SightIn>();

        StartCoroutine("CheckActivation");
    }

    IEnumerator CheckActivation()
    {
        List<SightIn> removeList = new List<SightIn>();

        if (sightIns.Count > 0)
        {
            foreach (SightIn item in sightIns)
            {
                if (Vector3.Distance(playerObject.transform.position, item.itemPos) > distanceFromPlayer)
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(false);
                    }
                }
                else
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(true);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (SightIn item in removeList)
            {
                sightIns.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine("CheckActivation");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(playerObject.transform.position, distanceFromPlayer);
    }
}

public class SightIn
{
    public GameObject item;
    public Vector3 itemPos;
}

