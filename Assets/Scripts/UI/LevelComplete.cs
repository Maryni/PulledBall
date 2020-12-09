using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    //Script is un usable
    [SerializeField] private GameObject panel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(true);
        }
    }
}
