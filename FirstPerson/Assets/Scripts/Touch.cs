using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    [SerializeField]
    private AiPathfinder pathfinder;

    private void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Player":
                break;
            case "Block":
                pathfinder.Jump();
                break;
        }
    }
}
