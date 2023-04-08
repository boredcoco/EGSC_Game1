using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    private Rigidbody[] childrenRbs;
    private TetrisBlockMovement[] childMovements;
    private TetrisBlockCollisionHandler[] childCollisionHandlers;
    private TetrisBlockSpawner tetrisBlockSpawner;

    private bool isGrounded = false;

    [SerializeField] private float force = 1f;

    private void Start()
    {
      childrenRbs = GetComponentsInChildren<Rigidbody>();
      childMovements = GetComponentsInChildren<TetrisBlockMovement>();
      childCollisionHandlers = GetComponentsInChildren<TetrisBlockCollisionHandler>();

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

/*
    private void Update()
    {
      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX * force, 0, 0);

      if (!isGrounded)
      {
        foreach(TetrisBlockCollisionHandler cHandler in childCollisionHandlers)
        {
          if (cHandler.checkIfWillHitSideWall(directionX))
          {
            goto HandleZ;
          }
        }
        foreach(TetrisBlockMovement childMovement in childMovements)
        {
          childMovement.moveBlock(directionX);
        }
      }

      HandleZ:
      // Move along z-axis
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 directionZ = new Vector3(0, 0, operationZ * force);

      if (!isGrounded)
      {
        foreach(TetrisBlockCollisionHandler cHandler in childCollisionHandlers)
        {
          if (cHandler.checkIfWillHitSideWall(directionZ))
          {
            return;
          }
        }
        foreach(TetrisBlockMovement childMovement in childMovements)
        {
          childMovement.moveBlock(directionZ);
        }
      }
    }
    */


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
