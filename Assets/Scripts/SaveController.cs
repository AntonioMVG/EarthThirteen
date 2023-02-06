using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class SaveController : MonoBehaviour
{
    private void Start()
    {
        if(!PlayGame.instance.newGame)
        {
            LoadGame();
        }
    }
    public async void SaveGame()
    {
        await ZSerialize.SaveScene();
    }

    public async void LoadGame()
    {
        await ZSerialize.LoadScene();
    }
}
