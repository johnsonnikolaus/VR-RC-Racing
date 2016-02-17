using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
}
