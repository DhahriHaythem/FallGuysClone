using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform start;
    private int teleporterIndex;

    public void SetIndex(int index)
    {
        teleporterIndex = index;
        int randomIndex = Random.Range(0, transform.childCount);
        if (teleporterIndex == randomIndex)
        {
            var controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
            controller.transform.position = start.position;
            controller.transform.rotation = start.rotation;
            controller.enabled = true;
        }
    }

}
