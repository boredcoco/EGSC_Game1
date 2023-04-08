using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    private Rigidbody[] childrenRbs;
    private TetrisBlockMovement[] childMovements;
    private TetrisBlockSpawner tetrisBlockSpawner;

    private bool isGrounded = false;

    [SerializeField] private float force = 1f;

    private void Start()
    {
      childrenRbs = GetComponentsInChildren<Rigidbody>();
      childMovements = GetComponentsInChildren<TetrisBlockMovement>();

      // find the block spawner
      GameObject blockSpawner = GameObject.FindWithTag("BlockSpawner");
      if (blockSpawner != null)
      {
        tetrisBlockSpawner = blockSpawner.GetComponent<TetrisBlockSpawner>();
      }

    }

    private void FixedUpdate()
    {
      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX * force, 0, 0);

      if (!isGrounded)
      {
        foreach(Rigidbody rb in childrenRbs)
        {
          rb.AddForce(directionX, ForceMode.VelocityChange);
        }
      }

      // Move along z-axis
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 directionZ = new Vector3(0, 0, operationZ * force);

      if (!isGrounded)
      {
        foreach(Rigidbody rb in childrenRbs)
        {
          rb.AddForce(directionZ, ForceMode.VelocityChange);
        }
      }
    }

    public void FreezeAllChildrenPositions()
    {
      foreach(Rigidbody rb in childrenRbs)
      {
         rb.constraints = RigidbodyConstraints.FreezePosition;
         rb.isKinematic = true;
      }
      foreach(TetrisBlockMovement childMovement in childMovements)
      {
         childMovement.SetIsGrounded();
         childMovement.changeTag("Floor");
      }
      isGrounded = true;
      tetrisBlockSpawner.ClearCurrentBlock();
    }

    public void RotateAlongX()
    {
      if (isGrounded)
      {
        return;
      }

      Quaternion rotation = Quaternion.Euler(90, 0, 0);
      foreach(Rigidbody rb in childrenRbs)
      {
        // rb.MoveRotation(rotation);
        rb.rotation = rb.rotation * rotation;
      }
    }

    public void RotateAlongY()
    {
      if (isGrounded)
      {
        return;
      }

      Quaternion rotation = Quaternion.Euler(0, 90, 0);
      foreach(Rigidbody rb in childrenRbs)
      {
        // rb.MoveRotation(rotation);
        rb.rotation = rb.rotation * rotation;
      }
    }

    public void RotateAlongZ()
    {
      if (isGrounded)
      {
        return;
      }

      Quaternion rotation = Quaternion.Euler(0, 0, 90);
      foreach(Rigidbody rb in childrenRbs)
      {
        // rb.MoveRotation(rotation);
        rb.rotation = rb.rotation * rotation;
      }
    }

}
