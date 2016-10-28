using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITimer : MonoBehaviour {

    private Text text;
    private float time, realTime;

	void Start () {
        text = GetComponent<Text>();
	}

	void Update () {
        time += Time.deltaTime * GameManager.timeScaleFactor;
        realTime += Time.deltaTime;
        text.text = time.ToString() + '\n' + (realTime / 60.0f).ToString() + "min " + (realTime % 60.0f).ToString() + "sec";
	}
}
