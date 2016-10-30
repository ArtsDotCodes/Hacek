using UnityEngine;
using System.Collections;

public class DrumHandler : MonoBehaviour {

    [SerializeField] private AudioClip[] drumClips;
    private AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
        source.loop = false;
    }
	
	void Update () {
	    if (!source.isPlaying)
        {
            source.clip = drumClips[Random.Range(0, drumClips.Length)];
            source.Play();
        }
	}
}
