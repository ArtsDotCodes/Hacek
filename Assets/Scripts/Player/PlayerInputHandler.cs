using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

    [SerializeField]
    private SteamVR_TrackedController controller1, controller2;

    void Start()
    {
        controller1.PadClicked += HandlePadClick;
        controller2.PadClicked += HandlePadClick;
    }
	
	void Update () {
        if (GameManager.debugMode)
        {
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

    private void HandlePadClick(object sender, ClickedEventArgs e)
    {
        if(e.padX < 0.0f)
            PlayerManager.SetLeft();
        else
            PlayerManager.SetRight();
    }
}
