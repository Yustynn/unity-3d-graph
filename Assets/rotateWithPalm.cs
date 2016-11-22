using UnityEngine;
using System.Collections;

public class rotateWithPalm : MonoBehaviour {
    public GameObject palm;

    bool shouldRotate;

	// Use this for initialization
	void Awake () {
        shouldRotate = false;
	}

    public void ToggleShouldRotate()
    {
        shouldRotate = !shouldRotate;
        Debug.Log("Called!");
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldRotate)
        {
            transform.rotation = palm.transform.rotation;
        }
	}
}
