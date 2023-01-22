using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFov : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AnimationClip fovc = new AnimationClip();
        AnimationCurve curve = AnimationCurve.Linear(0.0f, 60.0f, 10.0f, 90.0f);
        fovc.SetCurve("", typeof(Camera), "field of view", curve);
        fovc.legacy = true;
       
        GetComponent<Animation>().AddClip(fovc, "HandAnimation");

        GetComponent<Animation>().Play("HandAnimation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
