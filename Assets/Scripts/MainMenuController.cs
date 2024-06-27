using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public string _level1;
    public string _level2;
    public string _level3;
    private string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameLevel1(){
        SceneManager.LoadScene(_level1);
    }
    public void StartGameLevel2(){
        SceneManager.LoadScene(_level2);
    }
    public void StartGameLevel3(){
        SceneManager.LoadScene(_level3);
    }
    public void Exit(){
        Application.Quit();
    }
}
