using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private float lerpSpeed;
    [SerializeField] private float flySpeed;

    private GameObject currentRail;
    private GameObject cam;
    private static bool reachedEnd;
    private static bool left, right, down, up;
    
    //audio stuff
    [SerializeField] private AudioClip[] railSounds;
    private AudioSource source;
    private AudioClip currentRailSound;
    private float railSoundTimer;
    private int railSoundIndex;

    void Start()
    {
        cam = GameObject.Find("VR Camera Holder");

        source = GetComponent<AudioSource>();
        currentRailSound = railSounds[0];
        railSoundIndex = 0;

        source.loop = true;
        source.clip = currentRailSound;
        source.Play();
    }

	void Update ()
    {
        if (left || right && !reachedEnd)
        {
            HandleRailSwitch();
            SetAllFlagsFalse();
        }    

        HandleAudio();
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

            if (railSoundIndex == railSounds.Length - 1)
                railSoundIndex = 0;
            else
                ++railSoundIndex;
        }

        else if (right)
        {
            if (GameManager.GetRailList().Find(currentRail).Previous != null)
                currentRail = GameManager.GetRailList().Find(currentRail).Previous.Value;
            else
                currentRail = GameManager.GetRailList().Last.Value;

            if (railSoundIndex == 0)
                railSoundIndex = railSounds.Length - 1;
            else
                --railSoundIndex;
        }

        currentRail.GetComponent<RailHandler>().SetIsPlayerRail(true);

        currentRailSound = railSounds[railSoundIndex];
        source.clip = currentRailSound;
        if (railSoundTimer > currentRailSound.length)
            railSoundTimer -= currentRailSound.length;
        source.Play();
        source.time = railSoundTimer;
    }

    private void HandleAudio()
    {
        //Rail sounds
        if (!reachedEnd)
        {
            railSoundTimer += Time.deltaTime;
            if (railSoundTimer > currentRailSound.length)
                railSoundTimer -= currentRailSound.length;
        }
        else if(source != null)
        {
            Destroy(source);
        }
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
        else
        {
            HandleFreeCam();
        }
    }

    private void HandleFreeCam()
    {
        if (up)
            transform.position += SteamVR_Render.Top().transform.forward * (Time.fixedDeltaTime * flySpeed);
        else if (down)
            transform.position += -SteamVR_Render.Top().transform.forward * (Time.fixedDeltaTime * flySpeed);
        else if (left)
            transform.position += Vector3.Cross(SteamVR_Render.Top().transform.forward, Vector3.up) * (Time.fixedDeltaTime * flySpeed);
        else if (right)
            transform.position += -Vector3.Cross(SteamVR_Render.Top().transform.forward, Vector3.up) * (Time.fixedDeltaTime * flySpeed);

        cam.transform.position = transform.position + Vector3.up;
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

    public static void SetDown()
    {
        down = true;
    }

    public static void SetUp()
    {
        up = true;
    }

    public void SetAllFlagsFalse()
    {
        left = false;
        right = false;
        up = false;
        down = false;
    }

    public static void SetReachedEnd(bool reachedEnd)
    {
        PlayerManager.reachedEnd = reachedEnd;
    }
}
