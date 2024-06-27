using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DummieCounter : MonoBehaviour
{
    public MenuHandler menuHandler;
    public TextMeshProUGUI counterText; // Reference đến TextMeshProUGUI để hiển thị số lượng
    public int dummies;
    public int initiaZombie;
    private void Start()
    {
        // Ban đầu, cập nhật số lượng GameObject có tag là "Dummie"
        UpdateCounter();
        initiaZombie = GameObject.FindGameObjectsWithTag("Dummie").Length;
    }

    private void UpdateCounter()
    {
        // Đếm số lượng GameObject có tag là "Dummie"
        dummies = GameObject.FindGameObjectsWithTag("Dummie").Length;
        // Cập nhật TextMeshProUGUI để hiển thị số lượng
        counterText.text = "Zombies: " + dummies.ToString();
    }

    // Hàm này sẽ được gọi mỗi khi có thay đổi trong Scene
    private void Update()
    {
        if(dummies <= 0){
            menuHandler.WinGame();
        }else{
            // Kiểm tra xem có cần cập nhật số lượng không
            UpdateCounter();
        }
    }
}
