using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    [SerializeField] private int moveSpeed;
    //[SerializeField] private int runSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private int jumpSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float defaultGround;


    private InputAction walkAction;
    private InputAction runAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    //private bool isWalking = false;
    //private bool isRuning = false;
    private bool ismoving = false;
    private bool isJumping = false;
    private float vSpeed;

    private Vector3 impact = Vector3.zero;

    private int playCount;

    void Awake()
    {
        walkAction = playerInput.actions["Walk"];
        runAction = playerInput.actions["Run"];
        jumpAction = playerInput.actions["Jump"];
        playCount = 0;
    }

    void Update()
    {
        //Inputs
        moveInput = walkAction.ReadValue<Vector2>();
        ismoving = moveInput != Vector2.zero;
        //isRuning = (int)runAction.ReadValue<float>() == 1;
        isJumping = (int)jumpAction.ReadValue<float>() == 1;
        //moving
        Vector3 move = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
        Vector3 lookAt = move;
        move *= moveSpeed;

        vSpeed += gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            vSpeed = defaultGround;
            if (isJumping)
            {
                vSpeed = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            }

        }

        move.y = vSpeed;

        if(impact!= Vector3.zero)
        {
            move +=  impact;
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.fixedDeltaTime);
        }

        controller.Move(move * Time.deltaTime);

        if (ismoving && moveInput.y != -1)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookAt, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        animator.SetBool("ismove", ismoving);
        animator.SetBool("isjump", isJumping);

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
     {
         if (hit.gameObject.tag=="Obstacle")
         {
             float force = hit.gameObject.GetComponent<ObstacleMotor>().GetHitForce();
             impact = force * hit.normal;
         }

        if (hit.gameObject.tag == "Victory")
        {
            if(!animator.GetBool("isvictory"))
            {
                animator.SetBool("isvictory", true);
                hit.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                hit.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
                hit.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                GameManager.instance.addLevel();
                GameManager.instance.Save();
            }
            
        }
        if (hit.gameObject.tag == "door")
        {
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(-hit.normal * 50, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Heart")
        { 
            other.gameObject.GetComponent<Heart>().Add();
            Hide(other.gameObject);
        }

        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.GetComponent<Coin>().Add();
            Hide(other.gameObject);
        }

        if (other.gameObject.tag == "Fail")
        {
            animator.SetBool("isdie", true);
            GameManager.instance.GameOver();
        }
    }

    private void Hide( GameObject o)
    {
        o.GetComponent<MeshRenderer>().enabled = false;
        o.GetComponent<BoxCollider>().enabled = false;
        AudioSource a = o.GetComponent<AudioSource>();
        AudioManager.instance.Play(a);
        StartCoroutine(GameManager.instance.DestroyObject(a));
    }

}
