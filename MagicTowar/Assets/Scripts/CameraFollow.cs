using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // camera will follow this object
    public Transform Target;
    //camera transform
    public Transform camTransform;
    // offset between camera and target
    public Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        //Offset = camTransform.position - Target.position;
    }
    private void Update()
    {
/*        if (GameManager.Manager.isFinishLineCrossed)
        {
            Offset = new Vector3(-19, 5, -4);
            if(Target != null)
                Target = pm.Snow.transform;
        }*/
    }
    private void LateUpdate()
    {
        if(Target != null)
        {
            // update position
            Vector3 targetPosition = Target.position + new Vector3(Offset.x, Offset.y , Offset.z);
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

            // update rotation
            //transform.LookAt(Target);
        }
    }
}
