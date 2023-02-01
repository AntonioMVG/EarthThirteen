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
        }
    }

    public void CheatMode(string cheat)
    {
        if (!(string.IsNullOrWhiteSpace(inputCheats.text) || string.IsNullOrEmpty(inputCheats.text)))
        {
            switch (inputCheats.text)
            {
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
                case "thanos":
                    foreach (Transform child in City.instance.housesContainer.GetComponentInParent<Transform>())
                    {
                        Destroy(child.gameObject);
                    }
                    foreach (Transform child in City.instance.factoriesContainer.GetComponentInParent<Transform>())
                    {
                        Destroy(child.gameObject);
                    }
                    foreach (Transform child in City.instance.farmsContainer.GetComponentInParent<Transform>())
                    {
                        Destroy(child.gameObject);
                    }
                    foreach (Transform child in City.instance.roadsContainer.GetComponentInParent<Transform>())
                    {
                        Destroy(child.gameObject);
                    }
                    foreach (Transform child in City.instance.treesContainer.GetComponentInParent<Transform>())
                    {
                        Destroy(child.gameObject);
                    }
                    inputCheats.text = string.Empty;
                    panelCheats.SetActive(false);
                    break;
            }
        }
    }
}
