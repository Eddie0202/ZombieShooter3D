using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Slider progressBar; // Reference đến UI Slider để hiển thị tiến trình tải
    public string sceneToLoad; // Tên của scene cần tải
    public GameObject loadingScreen; // Reference đến GameObject chứa màn hình loader

    public void LoadScene()
    {
        Time.timeScale = 0f;
        // Hiển thị màn hình loader
        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        // Tải scene bằng cách sử dụng coroutine
        StartCoroutine(LoadSceneAsync());
        
    }

    IEnumerator LoadSceneAsync()
    {
        // Tạo một AsyncOperation để tải scene không đồng bộ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Lặp lại cho đến khi scene đã được tải hoàn toàn
        while (!asyncOperation.isDone)
        {
            // Cập nhật giá trị của progress bar dựa trên tiến trình tải
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Giới hạn giá trị từ 0 đến 1
            progressBar.value = progress;

            yield return null;
        }
    }
}
