using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private float lerpSpeed;

    private GameObject currentRail;
    private GameObject cam;
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
        if(currentRail != null)
        {
            Vector3 oldPosition = transform.position;
            Vector3 newPosition = Vector3.Lerp(transform.position, currentRail.transform.position + Vector3.up, Time.fixedDeltaTime * lerpSpeed);
            transform.LookAt(newPosition);
            transform.position = newPosition;
            cam.transform.position = newPosition + Vector3.up;
        }
        else
        {
            LinkedListNode<GameObject> node = GameManager.GetRailList().First;
            GameObject closestRail = null;
            while(node != null)
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
        }
    }

    private void HandleRailSwitch()
    {        
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
           
    }

    /*
    private void HandleRailSwitchOld()
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
    */

    public void SetCurrentRail(int index)
    {
        LinkedListNode<GameObject> list = GameManager.GetRailList().First;
        for(int i=0; i<index; i++)
        {
            list = list.Next;
        }

        currentRail = list.Value;
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
