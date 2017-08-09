using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Person_Controller : MonoBehaviour {
    CharacterController characterController;
    public Transform footprints;
    public float totalTime = 0;

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpSpeed = 20.0f;
    float verticalRotation = 0;
    public float upDownRange = 60.0f;
    float verticalVelocity = 0;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        if(characterController.isGrounded && Input.GetButton("Jump")){
            verticalVelocity = jumpSpeed;
        }
        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;
        characterController.Move(speed * Time.deltaTime);

		totalTime += Time.deltaTime;
        if (totalTime > 1)
        {
            //Instantiate(footprints, GetComponent<Transform>().position, footprints.rotation);
            footprints.transform.rotation = GetComponent<Transform>().rotation;
            footprints.transform.rotation = footprints.transform.rotation * Quaternion.Euler(90, 0, 0);
            Instantiate(footprints, new Vector3(GetComponent<Transform>().position.x, 0.16f, GetComponent<Transform>().position.z), footprints.rotation);
            totalTime = 0;
        }
	}
}
