using UnityEngine;
using System.Collections;

public class DestroyByTrailTime : MonoBehaviour {

    private Vector3 originalScale;
    private float timeToDelete, trailTime;
    private bool markedForDeletion;

    void Start()
    {
        originalScale = transform.localScale;
        trailTime = GetComponent<TrailRenderer>().time;
        timeToDelete = trailTime;
    }

	void Update () {
        if (markedForDeletion)
        {
            timeToDelete -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, (trailTime+0.000001f - timeToDelete) / trailTime);
            if (timeToDelete <= 0.0f)
            {
                GameManager.GetRailList().Remove(gameObject);
                Destroy(gameObject);
            }    
        }
	}

    public void markForDeletion()
    {
        markedForDeletion = true;
    }
}
