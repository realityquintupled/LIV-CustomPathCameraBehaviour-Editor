using UnityEngine;

public class CameraController : MonoBehaviour {
    public float translateSpeed = 5f;
    public float rotateSpeed = 5f;

    private void Start() {
        
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if(Input.GetMouseButtonUp(1)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButton(1)) {
            if (Input.GetKey(KeyCode.W))
                transform.position += transform.forward * Time.deltaTime * translateSpeed;
            if (Input.GetKey(KeyCode.S))
                transform.position -= transform.forward * Time.deltaTime * translateSpeed;
            if (Input.GetKey(KeyCode.D))
                transform.position += transform.right * Time.deltaTime * translateSpeed;
            if (Input.GetKey(KeyCode.A))
                transform.position -= transform.right * Time.deltaTime * translateSpeed;
            if (Input.GetKey(KeyCode.E))
                transform.position += transform.up * Time.deltaTime * translateSpeed;
            if (Input.GetKey(KeyCode.Q))
                transform.position -= transform.up * Time.deltaTime * translateSpeed;
            transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, Space.World);
        }
    }
}
