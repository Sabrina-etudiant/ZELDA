using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInterract : MonoBehaviour
{
    public TextMeshProUGUI dialogueBox;
    public Canvas dialogBackground;
    DialogNumber NPCDialogTimes;

    bool isInRange=false;
    int NPCID;

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;
        if (!isInRange)
            return;
        dialogBackground.planeDistance = 1;
        GetComponent<TypewriterEffect>().Write(ChangeDialogueOnNPC(), dialogueBox);
        NPCDialogTimes.IncrementDialog();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
        NPCID = other.GetComponentInParent<NPCIdentifier>().ID;
        NPCDialogTimes = other.GetComponentInParent<DialogNumber>();
    }
    private void OnTriggerExit(Collider cld)
    {
        isInRange = false;
    }
    public string ChangeDialogueOnNPC()
    {
        if (NPCID == 0)
            return "Je suis le roi du monde ";
        if (NPCID == 1)
            return "Supercalifragilisticexpialidocious";
        if (NPCID == 2)
            return "Tu connais la différence entre toi et moi, MOI je suis magnifique!!!";
        if (NPCID == 3)
            return "C'est à moi que tu parle";
        return "L'ID N'est pas reconnue";
    }
}
