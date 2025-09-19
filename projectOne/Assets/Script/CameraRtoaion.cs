using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // Blocca il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Ottiene il movimento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calcola la rotazione sull'asse X (verticale), limitandola tra -90 e +90 gradi
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Applica la rotazione alla camera (solo su X per evitare rotazioni strane)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Ruota il corpo del player (o oggetto genitore) sull'asse Y
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
