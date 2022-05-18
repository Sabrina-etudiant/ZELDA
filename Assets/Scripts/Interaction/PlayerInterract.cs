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
            return "Dégage, c'est pas ta maison fils de...";
        if (NPCID == 1)
            return "On peut devenir amis que si tu me donne ton argent";
        if (NPCID == 2)
            return "Sort un peu et fait toi des amis au lieu de rien faire";
        if (NPCID == 3)
            return "Me parle pas. Regarde les texures de ma maison";
        return "L'ID N'est pas reconnue";
    }
}
