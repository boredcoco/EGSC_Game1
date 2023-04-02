using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody rb;

    private void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
      // downward movement
      Vector3 force = new Vector3(0, -1 * movementSpeed, 0);
      rb.AddForce(force, ForceMode.VelocityChange);
    }
}
