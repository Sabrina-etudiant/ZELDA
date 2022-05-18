using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    Canvas background;

    [SerializeField] float textSpeed;
    [HideInInspector] public float dialogLenght;
    [HideInInspector] public int textIndex = 0;

    float trueTextSpeed = 0;

    private void Start()
    {
        background = GetComponent<PlayerInterract>().dialogBackground;
    }
    public void Write(string messageToWrite, TMP_Text textLabel)
    {
        StartCoroutine(TypeCoroutine(messageToWrite, textLabel));
        dialogLenght = messageToWrite.Length;
    }
    public IEnumerator TypeCoroutine(string messageToWrite, TMP_Text textLabel)
    {
        while (messageToWrite.Length > textIndex)
        {
            trueTextSpeed += Time.deltaTime * textSpeed;
            Debug.Log(trueTextSpeed);
            textIndex = Mathf.FloorToInt(trueTextSpeed);
            textIndex = Mathf.Clamp(textIndex, 0, messageToWrite.Length);
            textLabel.text = messageToWrite.Substring(0, textIndex);
            yield return null;

        }
        textLabel.text = messageToWrite;
        if (messageToWrite.Length <= textIndex)
        {
            yield return new WaitForSeconds(5);
            background.planeDistance = 100;
            trueTextSpeed = 0;
            StopCoroutine(TypeCoroutine(messageToWrite, textLabel));
        }
    }
}
