using UnityEngine;
using System.Collections;

public class ForwardMover : MonoBehaviour {

    [SerializeField] private float speed;

    void FixedUpdate()
    {
        transform.Translate(transform.forward * Time.fixedDeltaTime * speed, Space.World);
    }
}
