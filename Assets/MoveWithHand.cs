using UnityEngine;
using System.Collections;

// for now, I've conflated rotation and movement a bit (temporary)

public class MoveWithHand : MonoBehaviour {

    public GameObject palm;
    public float speed = 10;

    Vector3 palmInitPos;
    bool shouldMove;

    void Awake ()
    {
        shouldMove = false;
    }

	// Use this for initialization
	Vector3 GenerateMovementVector () {

        Vector3 diff = palm.transform.position - palmInitPos;
        diff.x = -diff.x * Time.deltaTime * speed;
        diff.y = diff.y * Time.deltaTime * speed;
        diff.z = -diff.z * Time.deltaTime * speed;

        return diff;
    }

    void Move()
    {
        if (shouldMove)
        {
            Vector3 currPos = transform.position;
            transform.position = currPos + GenerateMovementVector();
        }
    }

    public void ToggleMoveMode()
    {
        palmInitPos = palm.transform.position;
        shouldMove = !shouldMove;

        Debug.Log("Movement toggled!");
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}
}
