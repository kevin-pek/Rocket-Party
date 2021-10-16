using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject following;

    void LateUpdate () {
        transform.position = new Vector3(following.transform.position.x, following.transform.position.y, -10);
    }
}
