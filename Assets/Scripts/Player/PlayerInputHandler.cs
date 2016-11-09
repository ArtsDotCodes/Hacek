using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour {

    [SerializeField]
    private SteamVR_TrackedController controller1, controller2;

    void Start()
    {
        controller1.PadClicked += HandlePadClick;
        controller2.PadClicked += HandlePadClick;
        controller1.PadUnclicked += HandlePadUnclick;
        controller2.PadUnclicked += HandlePadUnclick;
    }
	
	void Update () {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                PlayerManager.SetLeft();
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                PlayerManager.SetRight();
            }
            if (Input.GetKey(KeyCode.W))
        {
            PlayerManager.SetUp();
        }
            if (Input.GetKey(KeyCode.S))
        {
            PlayerManager.SetDown();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
	}

    private void HandlePadClick(object sender, ClickedEventArgs e)
    {
        if(Mathf.Abs(e.padX) > Mathf.Abs(e.padY))
        {
            if (e.padX < 0.0f)
                PlayerManager.SetLeft();
            else
                PlayerManager.SetRight();
        }
        else
        {
            if (e.padY < 0.0f)
                PlayerManager.SetDown();
            else
                PlayerManager.SetUp();
        }
    }

    private void HandlePadUnclick(object sender, ClickedEventArgs e)
    {
        FindObjectOfType<PlayerManager>().SetAllFlagsFalse();
    }
}
