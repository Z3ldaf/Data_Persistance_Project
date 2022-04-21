using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;

    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<MeshRenderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                renderer.material.color = Color.green;
                break;
            case 2:
                renderer.material.color = Color.yellow;
                break;
            case 5:
                renderer.material.color = Color.blue;
                break;
            default:
                renderer.material.color = Color.red;
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);

        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}
