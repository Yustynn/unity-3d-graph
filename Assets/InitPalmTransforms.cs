using UnityEngine;
using System.Collections;

public class InitPalmTransforms : MonoBehaviour {

    public GameObject palm;

    Vector3 initPalmPos, initPalmEulerRot, clearVector;

    void Awake()
    {
        clearVector = new Vector3(9999, 9999, 9999);
        initPalmPos = initPalmEulerRot = clearVector;
    }
    
	public void SaveInfo () {
	    if (initPalmPos == clearVector)
        {
            initPalmPos = palm.transform.position;
            initPalmEulerRot = palm.transform.rotation.eulerAngles;

            Debug.Log("Palm transform info saved!");
        }
	}

    public void ClearInfo()
    {
        initPalmPos = initPalmEulerRot = clearVector;

        Debug.Log("Palm transform info cleared!");
    }

    public Vector3 getInitPalmPos()
    {
        return initPalmPos;
    }

    public Vector3 getInitPalmEulerRot()
    {
        return initPalmEulerRot;
    }

}
