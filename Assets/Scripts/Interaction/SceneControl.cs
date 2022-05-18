using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void ToGameplay()
    {
        SceneManager.LoadScene(0);
    }
    public void ToWin()
    {
        SceneManager.LoadScene(1);
    }
    public void ToQuit()
    {
        Application.Quit();
    }
}
