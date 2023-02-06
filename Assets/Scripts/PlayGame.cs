using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public static PlayGame instance;
    public bool newGame;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    public void StartGame(bool isNew)
    {
        newGame = isNew;
        SceneManager.LoadScene("City");
    }
}
