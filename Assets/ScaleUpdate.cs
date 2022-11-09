using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpdate : MonoBehaviour

{
    public Transform HeadPosition;
    public Transform CameraPosition;
    public Transform Armature;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //    int accumulator = 0;

    //    float diff = (CameraPosition.position - HeadPosition.position).magnitude;
    //    if(diff >= 0.001f)
    //    {
    //        for (float diff2 = (CameraPosition.position - HeadPosition.position).magnitude; diff2 > 0.001; Armature.localScale+= new Vector3(0.5f, 0.5f, 0.5f)) {
    //            accumulator++;
    //            if(accumulator == 10)
    //            {
    //                break;
    //            }
    //        }

    //    }
    //    else if (diff <= -0.001f)
    //    {
    //        for (float diff2 = -(CameraPosition.position - HeadPosition.position).magnitude; diff2 > 0.001; Armature.localScale -= new Vector3(0.5f, 0.5f, 0.5f)) {
    //            accumulator++;
    //            if (accumulator == 10)
    //            {
    //                break;
    //            }
    //        }


    //    }
        
    }
}
