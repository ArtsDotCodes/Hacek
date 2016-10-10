using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    [SerializeField] private Vector3 rotations;
    [SerializeField] private float speed;

    void FixedUpdate()
    {
        transform.Rotate(rotations * Time.fixedDeltaTime * speed);
    }
}
