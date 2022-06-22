using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuHandler : MonoBehaviour
{
    public Text nameText;
    public Text bestScoreText;

    public void Start()
    {
        if (Persistent.Instance)
        {
            bestScoreText.text = Persistent.Instance.GetDecsription(false);
        }
    }

    public void StartGame()
    {
        Persistent.Instance.InitCurrentPlayer(nameText.text);

        SceneManager.LoadScene("main");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
