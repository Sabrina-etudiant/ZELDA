using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector] public Vector2 stickDirection;
    Vector3 moveDirectionLat;
    Vector3 moveDirection;
    Vector3 startGravity;
    Vector3 updateGravity;
    Vector3 lastVelocity;

    CharacterController controller;

    public float playerSpeed;
    [HideInInspector] public bool isSprint = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startGravity = Physics.gravity;

    }
    public void OnMovePlayer(InputAction.CallbackContext obj)
    {
        stickDirection = obj.ReadValue<Vector2>();
    }
    public void OnSprintPlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            isSprint = true;
        else
            isSprint = false;
    }
    private void Update()
    {
        updateGravity = GravityVector();
    }
    private void FixedUpdate()
    {
        if (isSprint)
            moveDirectionLat = new Vector3(stickDirection.x, 0f, stickDirection.y) * playerSpeed*2;
        else
            moveDirectionLat = new Vector3(stickDirection.x, 0f, stickDirection.y) * playerSpeed;

        moveDirection = moveDirectionLat + updateGravity;
        lastVelocity = moveDirection; 
        controller.Move(moveDirection * Time.deltaTime);
    }
    private Vector3 GravityVector()
    {
        if(!controller.isGrounded)
        {
            //Calcul de la gravité si le player est dans les airs
            var gravVectorAir = lastVelocity +startGravity * Time.deltaTime*2;
            gravVectorAir.x = 0;
            gravVectorAir.z = 0;
            return gravVectorAir;
        }
        else
        {
            //Calcul de la gravité si le player est sur le sol
            var gravVector = lastVelocity + startGravity * Time.deltaTime * 0.2f;
            gravVector.x = 0;
            gravVector.z = 0;
            return gravVector;
        }

    }
}
