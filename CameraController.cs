using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float zoomSpeed = 5f; // 缩放速度
    public float minZoom = 1f;   // 最小缩放值
    public float maxZoom = 10f;  // 最大缩放值

    void Update()
    {
        HandleMouseScrollWheel();
        HandleMouseLook();
        HandleKeyboardMovement();
    }
    void HandleMouseScrollWheel()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // 只在滚轮被滚动时执行缩放操作
        if (scrollWheel != 0f)
        {
            // 获取相机正前方的方向
            Vector3 forwardDirection = transform.TransformDirection(Vector3.forward);

            // 更新摄像机位置
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

        // 获取相机的正前方和正右方向
        Vector3 cameraForward = transform.forward;
        Vector3 cameraRight = transform.right;
        // 根据输入方向计算相机在相机空间的移动方向
        Vector3 moveDirection = (cameraForward.normalized * vertical + cameraRight.normalized * horizontal).normalized;

        // 移动相机
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
