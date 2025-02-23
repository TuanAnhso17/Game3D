using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    public Texture2D crosshairTexture; // Hình ảnh tâm ngắm
    public float crosshairSize = 30f;  // Kích thước

    void OnGUI()
    {
        if (crosshairTexture != null)
        {
            float x = (Screen.width - crosshairSize) / 2;
            float y = (Screen.height - crosshairSize) / 2;
            GUI.DrawTexture(new Rect(x, y, crosshairSize, crosshairSize), crosshairTexture);
        }
    }
}
