using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogNumber : MonoBehaviour
{
    public int dialogNumber;
    public UnityEvent onFirstDialog;

    public void IncrementDialog()
    {
        dialogNumber++;
        if (dialogNumber == 1)
        {
            onFirstDialog.Invoke();
        }
    }
}