using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject controlSetupWin;
    public GameObject mainMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlSetup()
    {
        controlSetupWin.SetActive(true);
        mainMenu.SetActive(false);
    }

}
