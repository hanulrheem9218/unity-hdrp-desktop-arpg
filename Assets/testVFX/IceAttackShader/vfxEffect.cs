using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class vfxEffect : MonoBehaviour
{
    public VisualEffect testEffect;
    public bool isEffectEnabled;
    void Start()
    {
        testEffect = gameObject.transform.GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (isEffectEnabled) testEffect.Play();
       // else if (!isEffectEnabled)
       // {
       //    // testEffect.Reinit();
       //     testEffect.Stop();
       // }
    }
    public void showEffect()
    {
        if (isEffectEnabled) testEffect.Play();
        else if (!isEffectEnabled)
        {
            // testEffect.Reinit();
            testEffect.Stop();
        }
    }
}
