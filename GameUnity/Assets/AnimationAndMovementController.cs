using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //Variaveis de Referencia
    PlayerInput playerInput;
    string nome = "Jammo_LowPoly";
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isPushingHash;
    int isJumpingHash;
    //Variaveis para comparar os valores do input com os valores do player
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    
    bool isMovementPressed;
    bool isRunPressed;
    bool isPushPressed;
    // bool isPickUp = false;

    // Variaveis do Pulo
    bool isJumpPressed = false;
    float initialJumpVelocity;

    [Header("Jump Parameters")]
    [SerializeField]float maxJumpHeight = 4.0f;
    [SerializeField]float maxJumpTime = 0.6f;
    bool isJumping = false;
    bool isJumpAnimating = false;
    // Gravidade
    float gravity = -9.8f;
    float groundedGravity = -.05f;

    float rotationFactorPerFrame = 15.0f;
    [Header("Movement Parameters")]
    [SerializeField] private float runMultiplier = 7.0f;
    [SerializeField] private float walkMultiplier = 5.0f;

    
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 1.2f;
    [SerializeField] private float forceMagnitude = 150.0f;


    void Awake(){
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isPushingHash = Animator.StringToHash("isPushing");
        isJumpingHash = Animator.StringToHash("isJumping");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        
        playerInput.CharacterControls.Push.started += onPush;
        playerInput.CharacterControls.Push.canceled += onPush;
        playerInput.CharacterControls.Push.performed += onPush;
        
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;
        playerInput.CharacterControls.Jump.performed += onJump;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Run.performed += onRun;

        setupJumpVariables();
    }

    void setupJumpVariables(){
        float timeToApex = maxJumpTime / 2;
        gravity = (-2*maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2*maxJumpHeight) / timeToApex;
    }

    void handleJump(){
        bool isPushing = animator.GetBool(isPushingHash);
        if(!isPushing){
            if(!isJumping && characterController.isGrounded && isJumpPressed){
                isJumping = true;
                animator.SetBool(isJumpingHash, true);
                isJumpAnimating = true;
                currentMovement.y = initialJumpVelocity * 0.5f;
                currentRunMovement.y = initialJumpVelocity * 0.5f; 
            }
            else if (!isJumpPressed && isJumping && characterController.isGrounded){
                isJumping = false;
            }
        }
    }

    void onJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
        // Debug.Log(isJumpPressed);
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

        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;
        float previousYVelocity;
        float newYVelocity;
        float nextYVelocity;

        if(characterController.isGrounded){
            if(isJumpAnimating){
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else if (isFalling){
            
            previousYVelocity = currentMovement.y;
            newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
        else{
            previousYVelocity = currentMovement.y;
            newYVelocity = currentMovement.y + (gravity*Time.deltaTime);
            nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
    }

    void PickupObject(GameObject pickObj){
        if(pickObj.GetComponent<Rigidbody>()){
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            
            heldObjRB.isKinematic = true;
            heldObjRB.drag = 10;
            heldObjRB.transform.root.parent = holdArea;
            heldObj = pickObj;
        }
    }
    void DropObject(){
        heldObjRB.isKinematic = false;
        heldObjRB.drag = 1;
        // heldObjRB.contraints = RigidbodyConstraints.None;
        Transform t = transform.Find("Ghost");
        t = t.GetChild(0);
        // Debug.Log(t);
        t.parent = null;
        heldObj = null;
    }

    void MoveObject(){
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f){
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            // Debug.Log(moveDirection);
            heldObjRB.AddForce(moveDirection*forceMagnitude);
        }
    }
    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        // Drop();
        
        if(isPushPressed){
            if(heldObj == null){
                RaycastHit hit;
                
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange)){
                    Vector3 moveDirection = (holdArea.position - hit.transform.gameObject.transform.position);
                    if(moveDirection.y <= 0.02f) PickupObject(hit.transform.gameObject);
                }     
            } 
            else{
                MoveObject();
            }
        }
        else{
            if(heldObj != null){
                DropObject();
            }
        }

        
        if(isRunPressed && !isPushPressed){
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else{
            characterController.Move(currentMovement * Time.deltaTime);
        }

        handleGravity();
        handleJump();
    }

    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }
    void OnDisable(){
        playerInput.CharacterControls.Disable();
    }
}

/*-----------------------------------------------------------------------------------------------------------------------------*/
 // void Drop(){
    //     if(!isPushPressed && isPickUp){
    //         sphere.transform.parent = null;
    //         sphere.isKinematic = false; 
    //         isPickUp = false;
    //     }
    // }
    
    
    // void OnControllerColliderHit(ControllerColliderHit hit){
    //     if(hit!= null){
    //         Rigidbody rigidbody = hit.collider.attachedRigidbody;
    //         if(rigidbody != null && isPushPressed && !isPickUp){
    //             // Debug.Log(rigidbody.name);
    //             // if(rigidbody.name == "Sphere"){
    //                 sphere = rigidbody;
    //                 isPickUp = true;

    //                 // Vector3 pos = new Vector3(currentMovement.x, 0.0f, currentMovement.z);
    //                 // rigidbody.MovePosition(pos);
    //                 // rigidbody.position = transform.position;

    //                 // Vector3 positionToLookAt;

    //                 // mudar para a posicao que o personagem deveria olhar
    //                 // positionToLookAt.x = currentMovement.x;
    //                 // positionToLookAt.y = 0.0f;
    //                 // positionToLookAt.z = currentMovement.z;
    //                 // Quaternion currentRotation = transform.rotation;

    //                 // // cria uma nova rotacao baseado em que posicao o usuario comanda para ele ir
                    
    //                 // Quaternion targetRotati>>on = Quaternion.LookRotation(positionToLookAt);
    //                 // rigidbody.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
                    
    //                 rigidbody.transform.parent = holdArea;
    //                 rigidbody.isKinematic = true;
                    
    //                 // Debug.Log(holdArea.position)

    //                 Vector3 moveDirection = (holdArea.position - rigidbody.transform.position);
                    
    //                 rigidbody.AddForce(moveDirection * forceMagnitude);

    //                 holdArea.position = transform.position + (transform.forward*2);
    //                 holdArea.rotation = transform.rotation;
    //                 // Debug.Log(transform.position);
                    
    //             // }
    //         }
    //     }
    // }
