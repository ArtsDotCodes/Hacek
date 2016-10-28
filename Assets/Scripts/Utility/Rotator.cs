using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    [SerializeField] private Vector3 rotations;
    [SerializeField] private float speed;
    private float acceleration;

    void FixedUpdate()
    {
        speed += acceleration * Time.fixedDeltaTime;
        transform.Rotate(rotations * Time.fixedDeltaTime * speed);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setAcceleration(float acceleration)
    {
        this.acceleration = acceleration;
    }
}
