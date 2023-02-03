using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cheats : MonoBehaviour
{
    public GameObject panelCheats;
    public TMP_InputField inputCheats;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            panelCheats.SetActive(true);
            inputCheats.Select();
        }
    }

    public void CheatMode(string cheat)
    {
        if (!(string.IsNullOrWhiteSpace(inputCheats.text) || string.IsNullOrEmpty(inputCheats.text)))
        {
            switch (inputCheats.text)
            {
                case "montgomeryburns":
                    City.instance.money += 100000;
                    inputCheats.text = string.Empty;
                    panelCheats.SetActive(false);
                    break;
                case "goldman":
                    City.instance.money += 5000;
                    inputCheats.text = string.Empty;
                    panelCheats.SetActive(false);
                    break;
                case "pepperonipizza":
                    City.instance.curFood += 100;
                    inputCheats.text = string.Empty;
                    panelCheats.SetActive(false);
                    break;
                case "nicolatesla":
                    City.instance.polution += 100;
                    inputCheats.text = string.Empty;
                    panelCheats.SetActive(false);
                    break;
                case "endofdays":
                    foreach (var zone in City.instance.containers.transform)
                    {
                        foreach (Transform child in City.instance.containers.transform.GetComponentInChildren<Transform>())
                        {
                            Destroy(child.gameObject);
                        }
                    }
                    inputCheats.text = string.Empty;
                    panelCheats.SetActive(false);
                    break;
                // TODO: More cheats
                // Truco para quemar los arboles y fastidiar la polucion
                // Truco para hacer llover
                // Truco para hacer caer un meteorito y destruir lo construido segun la zona
            }
        }
    }
}
