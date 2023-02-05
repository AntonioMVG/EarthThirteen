using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Cheats : MonoBehaviour
{
    public GameObject panelCheats;
    public TMP_InputField inputCheats;

    [Header("Particles")]
    public GameObject explosionParticle;

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
                // TODO: More cheats
                // Truco para quemar los arboles y fastidiar la polucion
                // Truco para hacer llover
                // Truco para hacer caer un meteorito y destruir lo construido segun la zona
                // Truco para convertir a parte de la población en zombie
                    case "endofdays":
                        foreach (Transform zone in City.instance.containers.transform)
                        {
                            foreach (Transform child in zone)
                            {
                                Destroy(child.gameObject);
                            }
                            GameObject particles = Instantiate(explosionParticle, City.instance.containers.transform.position, Quaternion.identity);
                            Destroy(particles, 0.2f);
                        }
                        City.instance.buildings.Clear();
                        City.instance.curPopulation = 0;
                        City.instance.maxPopulation = 0;
                        City.instance.curJobs = 0;
                        City.instance.maxJobs = 0;
                        City.instance.curFood = 0;
                        City.instance.polution = 0;
                        inputCheats.text = string.Empty;
                        panelCheats.SetActive(false);
                        break;
            }
        }
    }
}
