using UnityEngine;
using System.Collections;

public class MoveWithHand : MonoBehaviour {

    public GameObject palm;
    public float speed = 300;

    Vector3 palmInitPos;
    bool shouldMove;

    void Awake ()
    {
        shouldMove = false;
    }

	float GenerateMovementCoordinate(float c)
    {
        return c * Mathf.Abs(c) * Time.deltaTime * speed;
    }

	Vector3 GenerateMovementVector () {
        Vector3 diff = palm.transform.position - palmInitPos;
        Debug.Log(palmInitPos);
        diff.x = GenerateMovementCoordinate(diff.x);
        diff.y = GenerateMovementCoordinate(diff.y);
        diff.z = GenerateMovementCoordinate(diff.z);

        return diff;
    }

    void Move()
    {
        if (shouldMove)
        {
            Vector3 currPos = transform.position;
            transform.position = currPos - GenerateMovementVector();
        }
    }

    public void ToggleMoveMode()
    {
        shouldMove = !shouldMove;

        if (shouldMove)
        {
            palmInitPos = GameObject.Find("DataStore").GetComponent<InitPalmTransforms>().EnableIsMoving();
        } else
        {
            GameObject.Find("DataStore").GetComponent<InitPalmTransforms>().DisableIsMoving();
        }
        

        Debug.Log("Movement toggled!");
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}
}
