using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour{

    private Button restartButton;

    void Awake(){
        restartButton = GetComponent<Button>();
    }

    void Start(){
        restartButton = GameObject.Find("restart").GetComponent<Button>();
        restartButton.onClick.AddListener(OnRestartClick);
    }

    public void OnRestartClick(){
        SceneManager.LoadScene("main");
    }
}
