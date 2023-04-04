using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody rb;

    private bool isGrounded = false;

    // snap to positions
    private float[] standardXPositions = {-1.38f, -0.38f, 0.62f};
    private float[] standardZPositions = {-1.208f, -0.208f, 0.792f};

    private ParentController parentController;

    private void Start()
    {
      rb = GetComponent<Rigidbody>();

      if (transform.parent.gameObject != null)
      {
        parentController = transform.parent.gameObject.GetComponent<ParentController>();
      }
    }

    private void FixedUpdate()
    {
      // downward movement
      Vector3 force = new Vector3(0, -1 * movementSpeed, 0);
      rb.AddForce(force, ForceMode.VelocityChange);

      if (isGrounded)
      {
        return;
      }
      // Move along x-axis
      /*
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX, 0, 0);
      if (parentController != null && !parentController.checkIfAnyCollideX())
      {
        transform.position = transform.position + directionX;
      }
      */

      // Move along z-axis
      /*
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 directionZ = new Vector3(0, 0, operationZ);
      if (parentController != null && !parentController.checkIfAnyCollideZ())
      {
        transform.position = transform.position + directionZ;
      }
      */
    }

    public void SetIsGrounded()
    {
      isGrounded = true;
    }

    public void SnapToNearestPosition()
    {
      float closestDist = Mathf.Infinity;
      float nearestX = standardXPositions[0];
      float nearestZ = standardXPositions[0];

      foreach(float xPos in standardXPositions)
      {
        foreach(float zPos in standardZPositions)
        {
          Vector3 comparison = new Vector3(xPos, transform.position.y, zPos);
          if (Vector3.Distance(transform.position, comparison) < closestDist)
          {
            closestDist = Vector3.Distance(transform.position, comparison);
            nearestX = xPos;
            nearestZ = zPos;
          }
        }
      }

      Vector3 newPos = new Vector3(nearestX, transform.position.y, nearestZ);
      transform.position = newPos;
    }

    public void changeTag(string tag)
    {
      gameObject.tag = tag;
    }
}
