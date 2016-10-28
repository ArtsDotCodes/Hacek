using UnityEngine;
using System.Collections;

public class ChooseStartTime : MonoBehaviour {

    [SerializeField]
    private float startTime;

	void Start () {
        GetComponent<AudioSource>().time = startTime;
	}
}
