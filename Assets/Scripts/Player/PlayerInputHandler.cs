using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PlayerManager.SetLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            PlayerManager.SetRight();
        }
	}
}
