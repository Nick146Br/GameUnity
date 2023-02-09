using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //Passou de fase;
    public bool isAccepted = false;
    // Collider Boneco
    Vector3 Cboneco;
    //Variaveis de Referencia
    PlayerInput playerInput;
    CharacterController characterController;
    public Animator animator;
    float spawnValue = 100f;
    int isWalkingHash;
    int isRunningHash;
    int isPushingHash;
    int isJumpingHash;
    int isSittingHash;
    int contSit = 0;
    int contStand = 0;
    int limite = 25;
    //Variaveis para comparar os valores do input com os valores do player
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 _cameraRelativeMovement;
    Vector3 _cameraRelativeRunMovement;
    
    bool isMovementPressed;
    bool isRunPressed;
    bool isPushPressed;
    bool isSittingPressed;
    // bool isInsideTheCircle = false;
    // bool isPickUp = false;

    // Variaveis do Pulo
    bool isJumpPressed = false;
    bool Colidiu = false;
    float initialJumpVelocity;

    [Header("Jump Parameters")]
    [SerializeField]float maxJumpHeight = 4.0f;
    [SerializeField]float maxJumpTime = 0.6f;
    bool isJumping = false;
    public bool isSitting = false;
    bool isJumpAnimating = false;
    bool allowSitting = false;
    bool allowStandUp = false;
    public bool isDead = false;
    // Gravidade
    float gravity = -9.8f;
    float groundedGravity = -.05f;

    float rotationFactorPerFrame = 15.0f;
    [Header("Movement Parameters")]
    [SerializeField] private float runMultiplier = 7.0f;
    [SerializeField] private float walkMultiplier = 5.0f;

    // PickUp Settings
    // [Header("Pickup Settings")]
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private float PickupRange = 0.7f;
    [SerializeField] private float forceMagnitude = 1f;
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    private Collider ColliderHeldObj;
    

    private GameObject WhereIsInside;

    void Awake(){
        // Cursor.visible = false;
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isPushingHash = Animator.StringToHash("isPushing");
        isJumpingHash = Animator.StringToHash("isJumping");
        isSittingHash = Animator.StringToHash("isSitting");

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
        
        playerInput.CharacterControls.SitDown.started += onSit;
        playerInput.CharacterControls.SitDown.performed += onSit;
        playerInput.CharacterControls.SitDown.canceled += onSit;

        setupJumpVariables();
    }

    void setupJumpVariables(){
        float timeToApex = maxJumpTime / 2;
        gravity = (-2*maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2*maxJumpHeight) / timeToApex;
    }

    void handleJump(){
        // bool isPushing = animator.GetBool(isPushingHash);
        // if(!isPushing){
            if(!isJumping && characterController.isGrounded && isJumpPressed && !isSitting){
                isJumping = true;
                animator.SetBool(isJumpingHash, true);
                isJumpAnimating = true;
                currentMovement.y = initialJumpVelocity * 0.5f;
                currentRunMovement.y = initialJumpVelocity * 0.5f; 
            }
            else if (!isJumpPressed && isJumping && characterController.isGrounded){
                isJumping = false;
            }
        // }
    }
    void handleSit(){
        if(allowSitting && contStand == limite && !isSitting && !isJumping && isSittingPressed){
            contSit = 0;
            animator.SetBool(isSittingHash, true);
            isSitting = true;
            WhereIsInside.SendMessage("SitHere", true, SendMessageOptions.DontRequireReceiver);
            
        }
        else if(allowStandUp && contSit == limite && isSitting && isSittingPressed){
            contStand = 0;
            animator.SetBool(isSittingHash, false);
            WhereIsInside.SendMessage("SitHere", false, SendMessageOptions.DontRequireReceiver);
            // allowSitting = false;
            isSitting = false;
        }
    }

    void onSit(InputAction.CallbackContext context){
        isSittingPressed = context.ReadValueAsButton();
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
        positionToLookAt.x = _cameraRelativeMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _cameraRelativeMovement.z;

        // rotacao atual do nosso personagem
        Quaternion currentRotation = transform.rotation;

        // cria uma nova rotacao baseado em que posicao o usuario comanda para ele ir
        if(isMovementPressed && !isSitting){
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
        if(!isSitting){
            animator.SetBool(isSittingHash, false);

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
            pickObj.SendMessage("SendoSegurada", true, SendMessageOptions.DontRequireReceiver);
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            Collider c = gameObject.GetComponent<Collider>();
            // Debug.Log(c);
            // c.transform.localScale
            // c.size = c.size + new Vector3(0, 0, 2f);
            // heldObjRB.isKinematic = true;
            // heldObjRB.detectCollisions = true;
            heldObjRB.drag = 10;
            heldObjRB.transform.root.parent = holdArea;
            // var filho = transform.Find("Ghost").GetChild(0).GetChild(0).transform.position;
            // c.transform.position = filho;
            heldObj = pickObj;
        }
    }
    void DropObject(){
        // heldObjRB.isKinematic = false;
        // heldObjRB.detectCollisions = false;
        heldObjRB.drag = 1;
        // c = GetComponent<Collider>();
        // c.size = Cboneco;
        Transform t = transform.Find("Ghost");
        t = t.GetChild(0);
        t.gameObject.SendMessage("SendoSegurada", false, SendMessageOptions.DontRequireReceiver);
        Colidiu = false;
        t.parent = null;
        heldObj = null;
    }

    void MoveObject(){
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f){
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
         
            heldObjRB.AddForce(moveDirection*forceMagnitude);
        }
    }
    // Update is called once per frame
    void OnTriggerStay(Collider other){
        string nome = other.name;
        if(nome == "InternalSphereJosephus"){
            WhereIsInside = other.gameObject;
            allowSitting = true;
            WhereIsInside.SendMessage("IsInside", true, SendMessageOptions.DontRequireReceiver);
            
        }
    }
    void OnTriggerExit(Collider other){
        string nome = other.name;
        if(nome == "InternalSphereJosephus"){
            WhereIsInside = other.gameObject;
          
            WhereIsInside.SendMessage("IsInside", false, SendMessageOptions.DontRequireReceiver);
         
        }
    }
    
    void Update()
    {
        if(!isDead){
            if(contSit < limite)contSit++;
            if(contStand < limite)contStand++;
            handleRotation();
            handleAnimation();
            // Drop();
            _cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
            _cameraRelativeRunMovement = ConvertToCameraSpace(currentRunMovement);
        
            if(!isSitting){
                if(isPushPressed){
                    if(heldObj == null){
                        // RaycastHit hit;
                        
                        Ray PickupRay = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);

                        if(Physics.Raycast(PickupRay, out RaycastHit hitInfo, PickupRange, PickupLayer)){
                            // Vector3 moveDirection = (holdArea.position - hit.transform.gameObject.transform.position);
                            // if(moveDirection.y <= 0.02f) 
                            PickupObject(hitInfo.transform.gameObject);
                        }     
                    } 
                    else{
                        
                        Ray PickupRay = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);

                        if(Colidiu && !Physics.Raycast(PickupRay, out RaycastHit hitInfo, 5f, PickupLayer)){
                            // Vector3 moveDirection = (holdArea.position - hit.transform.gameObject.transform.position);
                            // if(moveDirection.y <= 0.02f) 
                            DropObject();
                        } 
                        else MoveObject();
                    }
                }
                else{
                    if(heldObj != null){
                        DropObject();
                    }
                }

                if(isRunPressed && !isPushPressed){
                    characterController.Move(_cameraRelativeRunMovement * Time.deltaTime);
                }
                else{
                    characterController.Move(_cameraRelativeMovement * Time.deltaTime);
                }
            }
            handleGravity();
            handleJump();
            handleSit();
            allowSitting = false;
        }
        else animator.SetBool("isDead", true);
    }
    void LateUpdate(){
        if(transform.position.y < -spawnValue){
            transform.position = new Vector3(0f, 40f, 0f);            
        }
    }

    // void FixedUpdate(){
    //     if(CurrentObject){
    //         Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
    //         float DistanceToPoint = DirectionToPoint.magnitude;

    //         CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
            
    //     }
    // }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {

        float currentYValue = vectorToRotate.y;
        // get the forward and right directional vectors of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // remove the Y values to ignore upward/downward camera angles
        cameraForward.y = 0;
        cameraRight.y = 0 ;

        // re-normalize both vectors so they each have a magnitude of 1
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // rotate the X and Z VectorToRotate values to camera space
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        //the sum of both product is the Vector3 in camera space
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;


    }

    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }
    void OnDisable(){
        playerInput.CharacterControls.Disable();
    }

    void StandUp(bool flag){
        allowStandUp = flag;
        // Debug.Log(flag);
    }
    void CompleteTheLevel(bool flag){
        isAccepted = flag;
    }
    void Death(bool flag){
        isDead = flag;
        // Debug.Log(isDead);
    }
    void Colisao(bool flag){
        Colidiu = flag;
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
