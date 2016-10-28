using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {
	
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            PlayerManager.SetLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            PlayerManager.SetRight();
        }
	}
}
