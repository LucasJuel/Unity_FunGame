using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public Vector3 playerVelocity;
    private bool playerIsGrounded;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float rotationX;
    public float mouseY;
    Vector3 moveDirection;
    private float lookSpeed = 2.0f;
    private bool canMove = true;
    public Camera playerCam;

    public BoxCollider boxCol;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(playerIsGrounded && playerVelocity.y < 0){
            playerVelocity.y = 0f;
        }

        if(canMove){
            moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            //print(moveDirection);
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);
        }
    
        if(Input.GetButtonDown("Jump") && playerIsGrounded){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        
        playerVelocity.y += gravityValue * Time.deltaTime;


        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -45.0f, 45.0f);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerStay(Collider col){
        if(col.CompareTag("Ground")){
            print("Im colliding");
            playerIsGrounded = true;
        }else{
            playerIsGrounded = false;
        }
        
    }
}
