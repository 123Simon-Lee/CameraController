using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float zoomSpeed = 5f; // �����ٶ�
    public float minZoom = 1f;   // ��С����ֵ
    public float maxZoom = 10f;  // �������ֵ

    void Update()
    {
        HandleMouseScrollWheel();
        HandleMouseLook();
        HandleKeyboardMovement();
    }
    void HandleMouseScrollWheel()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // ֻ�ڹ��ֱ�����ʱִ�����Ų���
        if (scrollWheel != 0f)
        {
            // ��ȡ�����ǰ���ķ���
            Vector3 forwardDirection = transform.TransformDirection(Vector3.forward);

            // ���������λ��
            transform.position = transform.position + forwardDirection * scrollWheel * zoomSpeed;
        }
    }
    void HandleMouseLook()
    {
        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the camera based on mouse movement
            transform.Rotate(Vector3.up * mouseX * rotationSpeed);
            transform.Rotate(Vector3.left * mouseY * rotationSpeed);

            // Limit the vertical rotation to prevent flipping
            float xRotation = transform.eulerAngles.x;
            if (xRotation > 180f)
                xRotation -= 360f;

            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.eulerAngles = new Vector3(xRotation, transform.eulerAngles.y, 0f);
        }
        
    }

    void HandleKeyboardMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ��ȡ�������ǰ�������ҷ���
        Vector3 cameraForward = transform.forward;
        Vector3 cameraRight = transform.right;
        // �������뷽��������������ռ���ƶ�����
        Vector3 moveDirection = (cameraForward.normalized * vertical + cameraRight.normalized * horizontal).normalized;

        // �ƶ����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
