using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private float lerpSpeed;

    private GameObject currentRail;
    private GameObject cam;
    private static bool reachedEnd;
    private static bool left, right;

    void Start()
    {
        cam = GameObject.Find("VR Camera Holder");
    }

	void Update () {
        if (left || right)
            HandleRailSwitch();

        SetAllFlagsFalse();
	}

    void FixedUpdate()
    {
        if (!reachedEnd)
        {
            if (currentRail != null)
            {
                Vector3 newPosition = Vector3.Lerp(transform.position, currentRail.transform.position + Vector3.up, Time.fixedDeltaTime * lerpSpeed);
                transform.LookAt(newPosition);
                transform.position = newPosition;
                cam.transform.position = newPosition + Vector3.up;
            }
            else
            {
                LinkedListNode<GameObject> node = GameManager.GetRailList().First;
                GameObject closestRail = null;
                while (node != null)
                {
                    if (closestRail == null)
                    {
                        closestRail = node.Value;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, node.Value.transform.position) < Vector3.Distance(transform.position, closestRail.transform.position))
                            closestRail = node.Value;
                    }
                    node = node.Next;
                }

                currentRail = closestRail;
                currentRail.GetComponent<RailHandler>().SetIsPlayerRail(true);
            }
        }
    }

    private void HandleRailSwitch()
    {
        currentRail.GetComponent<RailHandler>().SetIsPlayerRail(false);

        if (left)
        {
            if (GameManager.GetRailList().Find(currentRail).Next != null)
                currentRail = GameManager.GetRailList().Find(currentRail).Next.Value;
            else
                currentRail = GameManager.GetRailList().First.Value;
        }
            
        else if (right)
        {
            if (GameManager.GetRailList().Find(currentRail).Previous != null)
                currentRail = GameManager.GetRailList().Find(currentRail).Previous.Value;
            else
                currentRail = GameManager.GetRailList().Last.Value;
        }

        currentRail.GetComponent<RailHandler>().SetIsPlayerRail(true);
    }

    public void SetCurrentRail(int index)
    {
        LinkedListNode<GameObject> list = GameManager.GetRailList().First;
        for(int i=0; i<index; i++)
        {
            list = list.Next;
        }

        currentRail = list.Value;
        currentRail.GetComponent<RailHandler>().SetIsPlayerRail(true);
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

    public static void SetReachedEnd(bool reachedEnd)
    {
        PlayerManager.reachedEnd = reachedEnd;
    }
}
