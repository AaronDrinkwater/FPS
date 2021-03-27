using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField]
    private float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        //locks the mouses position to within the viewport of the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; //decreases x rotation by the mouse's Y position
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamps so you cant rotate in 360 degrees

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); //applys xrotation to rotating in the game
        playerBody.Rotate(Vector3.up * mouseX); //rotates along the x axis
    }
}
