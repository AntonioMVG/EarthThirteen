using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class SaveController : MonoBehaviour
{
    public async void SaveGame()
    {
        await ZSerialize.SaveScene();
    }

    public async void LoadGame()
    {
        await ZSerialize.LoadScene();
    }
}
