using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference đến Text UI object để hiển thị thời gian
    public float startTime; // Thời điểm bắt đầu đếm
    private bool isRunning; // Biến đánh dấu xem bộ đếm thời gian có đang chạy hay không
    public int minutes;
    private void Start()
    {
        // Khởi tạo các giá trị ban đầu
        startTime = Time.time;
        isRunning = true;

        // Bắt đầu coroutine để đếm thời gian
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (isRunning)
        {
            // Tính thời gian đã trôi qua
            float elapsedTime = Time.time - startTime;

            // Hiển thị thời gian dưới dạng phút:giây trong Text UI
            minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = "Time: " + timeString;

            // Đợi 1 giây trước khi cập nhật lại thời gian
            yield return new WaitForSeconds(1f);
        }
    }
}
