using UnityEngine;
using System.Collections;

public class resetPosPerTime : MonoBehaviour
{
    public float timeInterval = 0.1f;

    private Vector3 initPos;
    private Quaternion initRot;

    // Use this for initialization  
    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
        Invoke("resetPos", timeInterval);
    }

    void resetPos()
    {
        transform.position = initPos;
        transform.rotation = initRot;
        Invoke("resetPos", timeInterval);
    }
}
