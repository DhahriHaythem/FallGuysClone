using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            int myIndex = transform.GetSiblingIndex();
            Teleporter teleporter = transform.parent.gameObject.GetComponent<Teleporter>();
            teleporter.SetIndex(myIndex);
        }
    }
}
