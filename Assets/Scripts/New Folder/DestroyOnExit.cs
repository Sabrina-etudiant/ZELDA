using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{

    // Se lance quand on quitte un �tat d�animation
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex)
    {

        // On d�truit le gameObject attach� � l�animator qu�on modifie (donc le canvas du loading screen)
        Destroy(animator.gameObject);
    }
}

