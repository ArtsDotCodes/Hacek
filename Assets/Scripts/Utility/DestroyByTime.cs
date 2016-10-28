using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    [SerializeField]
    private float timeToDelete;

	void Update () {
        timeToDelete -= Time.deltaTime;
        if (timeToDelete <= 0.0f)
            Destroy(gameObject);
	}
}
