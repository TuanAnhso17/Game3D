using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(2); // Tải scene có index 1, đảm bảo bạn đã thêm scene vào Build Settings
    }

    // Hàm thoát game
    public void QuitGame()
    {
        Application.Quit(); // Thoát ứng dụng
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng play mode trong editor
#endif
    }
}
