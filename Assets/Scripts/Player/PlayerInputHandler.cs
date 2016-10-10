using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerManager.SetLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerManager.SetRight();
        }
	}
}
