using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class weatherSystem : MonoBehaviour
{
    // check if anything enters the data interactions.
    [SerializeField] VisualEffect vfxEffect;
    public bool isSnowEnvironmentEnabled;
    // Start is called before the first frame update
    void Start()
    {
        vfxEffect = gameObject.transform.GetComponent<VisualEffect>();
        if(!isSnowEnvironmentEnabled)
        {
            vfxEffect.Stop();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isSnowEnvironmentEnabled)
        {
            vfxEffect.Play();
        }
        else if (!isSnowEnvironmentEnabled)
        {
            vfxEffect.Reinit();
            vfxEffect.Stop();
        }
    }

}
