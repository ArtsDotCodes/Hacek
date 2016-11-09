using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour {

    public Transform pivot;
    public float speed;

	void Start () {
        pivot = GameObject.Find("Pivot").transform;
	}
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W))
            transform.position += pivot.forward * speed * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position -= pivot.forward * speed * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.Cross(pivot.forward, Vector3.up) * Time.fixedDeltaTime * speed;
        if (Input.GetKey(KeyCode.D))
            transform.position -= Vector3.Cross(pivot.forward, Vector3.up) * Time.fixedDeltaTime * speed;
    }
}
