using System.Collections;
using Cinemachine;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform endPoint;
    // Prefab du canvas de loadingScreen avec l�animator d�attach�
    public Animator loadingScreen;

    private GameObject player;
    // Le component attach� � la main camera quand on utilise cinemachine
    private CinemachineBrain cameraBrain;

    private void Start()
    {
        // Raccourci vers le component Camera attach� � l�objet tagg� MainCamera
        var mainCamera = Camera.main;
        // S�il n�y a pas de camera trouv�e
        if (mainCamera == null)
            Debug.LogError("No main camera found! Did you tagged it correctly?");
        else
            cameraBrain = mainCamera.GetComponent<CinemachineBrain>();

        player = GameObject.FindWithTag("Player");
        if (player == null)
            Debug.LogError("Player object not found! Did you tagged it correctly?");
    }

    // Ne pas oublier de configurer les layers pour ne pas trigger avec n�importe quoi
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TeleportCoroutine());
    }

    private IEnumerator TeleportCoroutine()
    {
        // On r�cup�re la camera virtuelle qui est actuellement activ�e
        var activeCamera = cameraBrain.ActiveVirtualCamera;
        // On copie le prefab dans la sc�ne, il va automatiquement lancer l�animation d�apparition
        var loadingAnimator = Instantiate(loadingScreen);
        // On r�cup�re le temps en seconde que dure l�animation d�apparition
        var animTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;
        // On r�cup�re le moment o� on lance la coroutine (temps depuis le lancement du jeu en secondes)
        var startTime = Time.time;
        // On attend que le temps de jeu ait d�pass� le startTime + animTime
        // Si on lance cette coroutine � 15 secondes de jeu et que l�animation dure 1 seconde,
        // Alors on attend que le temps de jeu(Time.time) soit � 16 secondes (15+1) soit le temps du d�but + la dur�e
        yield return new WaitUntil(() => Time.time > startTime + animTime);
        // � ce moment l�, l�animation d�apparition est termin�e, l��cran de chargement est opaque
        // On peut donc t�l�porter l�avatar et la cam�ra de fa�on masqu�e
        // On calcule ainsi le vecteur qui repr�sente le d�placement de l�avatar 
        // Vecteur A moins Vecteur B = Vecteur qui va de B vers A 
        var teleportDelta = endPoint.position - player.transform.position;
        // On t�l�porte la cam�ra virtuelle active en lui sp�cifiant que c�est l�avatar qui se t�l�porte 
        // Ainsi que le vecteur repr�sentant le trajet parcouru
        activeCamera.OnTargetObjectWarped(player.transform, teleportDelta);
        // On t�l�porte notre avatar
        player.transform.position = endPoint.position;
        // On oriente l�avatar dans la direction du point de t�l�portation pour pas regarder le mur apr�s le d�placement
        player.transform.rotation = endPoint.rotation;
        // On lance l�animation de disparition de l��cran de chargement
        loadingAnimator.SetTrigger("Disparition");

        Physics.SyncTransforms();
    }
}
