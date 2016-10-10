using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private float lerpSpeed;

    private GameObject currentRail;
    private PlayerHitboxHandler leftHitbox, rightHitbox;
    private static bool left, right;

    void Start () {
        currentRail = FindObjectOfType<RailHandler>().gameObject;
        leftHitbox = GameObject.Find("LeftHitbox").GetComponent<PlayerHitboxHandler>();
        rightHitbox = GameObject.Find("RightHitbox").GetComponent<PlayerHitboxHandler>();
    }

	void Update () {
        if (left || right)
            HandleRailSwitch();

        SetAllFlagsFalse();
	}

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentRail.transform.position + Vector3.up, Time.fixedDeltaTime * lerpSpeed);
    }

    private void HandleRailSwitch()
    {
        if (left)
        {
            List<GameObject> rails = leftHitbox.GetRails();
            GameObject nextRail = null;
            foreach (GameObject o in rails)
            {
                if(o != currentRail)
                {
                    if (nextRail == null)
                        nextRail = o;
                    else if (Vector3.Distance(transform.position, o.transform.position) < Vector3.Distance(transform.position, nextRail.transform.position))
                        nextRail = o;
                }   
            }
            if (nextRail != null)
                currentRail = nextRail;
        }
        else if (right)
        {
            List<GameObject> rails = rightHitbox.GetRails();
            GameObject nextRail = null;
            foreach (GameObject o in rails)
            {
                if (o != currentRail)
                {
                    if (nextRail == null)
                        nextRail = o;
                    else if (Vector3.Distance(transform.position, o.transform.position) < Vector3.Distance(transform.position, nextRail.transform.position))
                        nextRail = o;
                }
            }
            if (nextRail != null)
                currentRail = nextRail;
        }
    }

    public static void SetLeft()
    {
        left = true;
    }

    public static void SetRight()
    {
        right = true;
    }

    private void SetAllFlagsFalse()
    {
        left = false;
        right = false;
    }
}
