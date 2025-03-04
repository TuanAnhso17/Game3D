using UnityEngine;

public class HideMouse : MonoBehaviour
{
    private bool isCursorLocked = true; // Ban đầu chuột hiển thị

    void Start()
    {
        SetCursorState(); // Đặt trạng thái chuột ban đầu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked; // Đảo trạng thái chuột
            SetCursorState();
        }
    }

    void SetCursorState()
    {
        Cursor.visible = !isCursorLocked; // Khi khóa, chuột ẩn; khi mở, chuột hiện
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

