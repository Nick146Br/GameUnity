using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //Variaveis de Referencia
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isPushingHash;
    //Variaveis para comparar os valores do input com os valores do player
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    
    bool isMovementPressed;
    bool isRunPressed;
    bool isPushPressed;
    bool isPickUp = false;

    float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 5.0f;
    float walkMultiplier = 1.5f;

    Rigidbody sphere = null; 
    
    [SerializeField] private float forceMagnitude = 150.0f;

    [SerializeField] Transform holdArea;

    void Awake(){
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isPushingHash = Animator.StringToHash("isPushing");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        
        playerInput.CharacterControls.Push.started += onPush;
        playerInput.CharacterControls.Push.canceled += onPush;
        playerInput.CharacterControls.Push.performed += onPush;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Run.performed += onRun;
    }

    void onRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }

    void onPush(InputAction.CallbackContext context){
        isPushPressed = context.ReadValueAsButton();
    }

    void handleRotation(){
        Vector3 positionToLookAt;
        // mudar para a posicao que o personagem deveria olhar
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        // rotacao atual do nosso personagem
        Quaternion currentRotation = transform.rotation;

        // cria uma nova rotacao baseado em que posicao o usuario comanda para ele ir
        if(isMovementPressed){
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void onMovementInput(InputAction.CallbackContext context){
        currentMovementInput = context.ReadValue<Vector2>();
        
        currentMovement.x = currentMovementInput.x * walkMultiplier;
        currentMovement.z = currentMovementInput.y * walkMultiplier;
        
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void Drop(){
        if(!isPushPressed && isPickUp){
            sphere.transform.parent = null;
            sphere.isKinematic = false; 
            isPickUp = false;
        }
    }
    
    
    void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit!= null){
            Rigidbody rigidbody = hit.collider.attachedRigidbody;
            if(rigidbody != null && isPushPressed && !isPickUp){
                Debug.Log(rigidbody.name);
                if(rigidbody.name == "Sphere"){
                    sphere = rigidbody;
                    isPickUp = true;
                    rigidbody.transform.parent = holdArea;
                    rigidbody.isKinematic = true; 
                    Vector3 moveDirection = (holdArea.position - rigidbody.transform.position);
                    rigidbody.AddForce(moveDirection * forceMagnitude);
                }
            }
        }
    }

    void handleAnimation(){
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isPushing = animator.GetBool(isPushingHash);

        // Setar aqui o colisor aqui - Precisa do colisor pra empurrar algo

        if(isPushPressed && !isPushing){
            animator.SetBool(isPushingHash, true);
            animator.SetBool(isRunningHash, false);
        }

        else if(!isPushPressed && isPushing){
            animator.SetBool(isPushingHash, false);
        }

        if(isMovementPressed && !isWalking){
            animator.SetBool(isWalkingHash, true);
        }

        else if(!isMovementPressed && isWalking){
            animator.SetBool(isWalkingHash, false);
        }
        
        if((!isPushPressed && isMovementPressed && isRunPressed) && !isRunning){
            animator.SetBool(isRunningHash, true);
        }

        else if((isPushPressed || !isMovementPressed || !isRunPressed) && isRunning){
            animator.SetBool(isRunningHash, false);
        }
    }
    

    void handleGravity(){
        if(characterController.isGrounded){
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else{
            float gravity = -9.8f;
            currentMovement.y = gravity;
            currentRunMovement.y = gravity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        handleGravity();
        Drop();
        
        if(isRunPressed && !isPushPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else{
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }
    void OnDisable(){
        playerInput.CharacterControls.Disable();
    }
}