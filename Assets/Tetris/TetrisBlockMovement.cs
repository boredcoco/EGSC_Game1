using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody rb;

    private bool isGrounded = false;

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

    private void Update()
    {
      if (isGrounded)
      {
        return;
      }
      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX, 0, 0);
      if (!checkBoundsWhenMove(directionX, transform.position))
      {
        transform.position = transform.position + directionX;
      }

      // Move along z-axis
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 directionZ = new Vector3(0, 0, operationZ);
      if (!checkBoundsWhenMove(directionZ, transform.position))
      {
        transform.position = transform.position + directionZ;
      }
    }

    public void SetIsGrounded()
    {
      isGrounded = true;
    }

    private bool checkBoundsWhenMove(Vector3 direction, Vector3 position)
    {
      RaycastHit hit;
      if (Physics.Raycast(position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
      {
          if (hit.distance < 1f)
          {
            return true;
          }
          return false;
      }
      else
      {
          return false;
      }
    }
}
