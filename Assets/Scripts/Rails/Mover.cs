using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    [SerializeField] private Vector3 move;
    [SerializeField] private float speed;
	
	void FixedUpdate () {
        transform.Translate(move * Time.fixedDeltaTime * speed);
	}
}
