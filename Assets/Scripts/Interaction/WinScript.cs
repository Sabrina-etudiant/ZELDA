using UnityEngine;
using UnityEngine.Events;

public class WinScript : MonoBehaviour
{
    public int dialogToWin;
    public UnityEvent<int> onTalking;

    SceneControl _sceneControl;
    private void Start()
    {
        _sceneControl = GetComponent<SceneControl>();
    }

    public void IncrementWin()
    {
        dialogToWin++;
        YouWin();
        onTalking?.Invoke(dialogToWin);
    }
    void YouWin()
    {
        if (dialogToWin == 4)
        {
            Invoke("WinScreen", 5);
            Debug.Log("won");
        }
    }
    public void WinScreen()
    {
        _sceneControl.ToWin();
        Debug.Log("win");
    }
}
