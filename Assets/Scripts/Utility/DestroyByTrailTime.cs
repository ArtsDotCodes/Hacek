using UnityEngine;
using System.Collections;

public class DestroyByTrailTime : MonoBehaviour {

    private float timeToDelete;
    private bool markedForDeletion;

    void Start()
    {
        timeToDelete = GetComponent<TrailRenderer>().time;
    }

	void Update () {
        if (markedForDeletion)
        {
            timeToDelete -= Time.deltaTime;
            if (timeToDelete <= 0.0f)
                Destroy(gameObject);
        }
	}

    public void markForDeletion()
    {
        markedForDeletion = true;
    }
}
