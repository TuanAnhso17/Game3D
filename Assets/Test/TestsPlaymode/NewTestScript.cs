using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class NewTestScript
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load scene test ban đầu
        SceneManager.LoadScene("UI");
        yield return null; // đợi 1 frame để scene load
    }
    [UnityTest]
    public IEnumerator ButtonClick_ChangesScene()
    {
        // Tìm button trong scene

        GameObject btnobj = GameObject.Find("Canvas/PLAY");
        Assert.IsNotNull(btnobj, "Không tìm thấy GameObject tên 'PLAY'");
        Button button = btnobj.GetComponent<Button>();
        Assert.IsNotNull(button, "Không tìm thấy Button trong scene!");


        // Giả lập nhấn button
        button.onClick.Invoke();


        // Đợi một vài frame để scene kịp load
        yield return new WaitForSeconds(1f);


        // Kiểm tra xem scene đã đổi chưa
        Assert.AreEqual("Real", SceneManager.GetActiveScene().name);
    }


    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
