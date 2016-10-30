using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private float lerpSpeed;

    private GameObject currentRail;
    private GameObject cam;
    private static bool reachedEnd;
    private static bool left, right;
    
    //audio stuff
    [SerializeField] private AudioClip[] railSounds;
    private AudioSource source;
    private AudioClip currentRailSound;
    private float railSoundTimer;
    private int railSoundIndex;

    private enum SoundState {sparse, medium, dense};
    private SoundState soundState;

    void Start()
    {
        cam = GameObject.Find("VR Camera Holder");

        source = GetComponent<AudioSource>();
        currentRailSound = railSounds[0];
        railSoundIndex = 0;

        source.loop = true;
        source.clip = currentRailSound;
        source.Play();

        soundState = SoundState.sparse;
    }

	void Update ()
    {
        if (left || right)
            HandleRailSwitch();

        HandleAudio();

        SetAllFlagsFalse();
	}

    private void HandleAudio()
    {
        //Rail sounds
        railSoundTimer += Time.deltaTime;
        if (railSoundTimer > currentRailSound.length)
            railSoundTimer -= currentRailSound.length;

        //Ambient mountain sounds
        /*
        if (!source2.isPlaying)
        {
            if(soundState == SoundState.dense)
            {
                source2.clip = denseSounds[Random.Range(0, denseSounds.Length)];
                source2.Play();
            }
            else if(soundState == SoundState.medium)
            {
                source2.clip = mediumSounds[Random.Range(0, mediumSounds.Length)];
                source2.Play();
            }
            else
            {
                source2.clip = sparseSounds[Random.Range(0, sparseSounds.Length)];
                source2.Play();
            }
        }

        if(transform.position.y >= denseThreshold && soundState != SoundState.dense)
        {
            soundState = SoundState.dense;
            source2.clip = denseSounds[Random.Range(0, denseSounds.Length)];
            source2.Play();
        }
        else if(transform.position.y >= mediumThreshold && transform.position.y < denseThreshold && soundState != SoundState.medium)
        {
            soundState = SoundState.medium;
            source2.clip = mediumSounds[Random.Range(0, mediumSounds.Length)];
            source2.Play();
        }
        else if(transform.position.y < mediumThreshold && soundState != SoundState.sparse)
        {
            soundState = SoundState.sparse;
            source2.clip = sparseSounds[Random.Range(0, sparseSounds.Length)];
            source2.Play();
        }
        */
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
                railSoundIndex = railSounds.Length-1;
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
