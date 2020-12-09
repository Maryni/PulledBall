using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPortal : MonoBehaviour
{
    [SerializeField] private Transform point; //Portal
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject gameObjectPoint; // Point from scene hierarhy
    [SerializeField] private GameObject panel;

    public void Move(){ Moving();  }

    private void Moving()
    {
        gameObjectPoint.SetActive(false);
        navMeshAgent.destination = point.position;
        panel.SetActive(true);
    }
}
