using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public Vector3 playerVelocity;
    public  bool playerIsGrounded;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    public float rotationX;
    public float mouseY;

    public float lookSpeed = 2.0f;

    public float jumpMultiplier;
    public Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   

        playerIsGrounded = controller.isGrounded; 

        if(playerIsGrounded){
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
    

        if(Input.GetButtonDown("Jump") && playerIsGrounded){
            playerVelocity.y += jumpHeight * -jumpMultiplier * gravityValue;
        }

        if(!playerIsGrounded){
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -45.0f, 45.0f);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Ground"){
            print("Im Grounded");
        }
    }
}
