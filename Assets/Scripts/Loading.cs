using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI loadingText;

    private string bundleUrl = "https://drive.google.com/uc?export=download&id=1U8dx0nw9nzJAhDwEDR4A2uAhi1k2OLxR";
    private string sceneToLoad = "Real"; // Tên scene chính

    void Start()
    {
        StartCoroutine(DownloadAndLoadBundle());
    }

    IEnumerator DownloadAndLoadBundle()
    {
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            www.SendWebRequest();

            while (!www.isDone)
            {
                progressBar.value = www.downloadProgress;
                loadingText.text = "Đang tải... " + (www.downloadProgress * 100).ToString("F0") + "%";
                yield return null;
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Lỗi tải AssetBundle: " + www.error);
                loadingText.text = "Lỗi tải dữ liệu!";
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                if (bundle != null)
                {
                    Debug.Log("Tải thành công AssetBundle!");
                    loadingText.text = "Hoàn tất!";

                    // Load các Prefabs từ AssetBundle
                    string[] assetNames = bundle.GetAllAssetNames();
                    foreach (string assetName in assetNames)
                    {
                        GameObject prefab = bundle.LoadAsset<GameObject>(assetName);
                        Instantiate(prefab);
                        Debug.Log("Đã load: " + assetName);
                    }

                    bundle.Unload(false);

                    // Đợi 1 giây rồi chuyển sang scene chính
                    yield return new WaitForSeconds(1);
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    Debug.LogError("Không thể tải dữ liệu từ AssetBundle");
                    loadingText.text = "Lỗi tải dữ liệu!";
                }
            }
        }
    }
}
