using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public float StartSpeed;
    public float Speed;

    private void Update()
    {
        transform.LookAt(target.transform);

        if (Vector3.Distance(transform.position, target.transform.position) > 0.4f) //Was 0.6!
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.transform.position) > 0.65f)
        {
            Speed = StartSpeed * 1.5f;
        }
        else
        {
            Speed = StartSpeed;
        }

    }
}
