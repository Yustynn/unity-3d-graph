using UnityEngine;
using System.Collections;

public class MatchPalmRotation : MonoBehaviour {

    public GameObject palm;

    bool shouldRotate;
    Vector3 initCubeEuler, initPalmEulerRot;

	// Use this for initialization
	void Awake () {
        shouldRotate = false;
	}

    public void ToggleRotationMode()
    {
        shouldRotate = !shouldRotate;
        initPalmEulerRot = GameObject.Find("DataStore").GetComponent<InitPalmTransforms>().getInitPalmEulerRot();
        initCubeEuler = transform.rotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldRotate)
        {
            Vector3 diff = palm.transform.rotation.eulerAngles - initPalmEulerRot;
            transform.rotation = Quaternion.Euler(initCubeEuler + diff);
        }
	}
}
