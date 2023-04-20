using System;
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

    private HashSet<int> layersCleared = new HashSet<int>();

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

/*
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
    */

    private void Update()
    {
      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX, 0, 0);

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
      Vector3 directionZ = new Vector3(0, 0, operationZ);

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


    public void FreezeAllChildrenPositions()
    {
      foreach(TetrisBlockMovement childMovement in childMovements)
      {
        if (childMovement != null)
        {
          childMovement.HandleSnap();
          childMovement.changeTag("Floor");
        }
      }
      foreach(Rigidbody rb in childrenRbs)
      {
        if (rb != null)
        {
          // rb.constraints = RigidbodyConstraints.FreezePosition;
          rb.isKinematic = true;
          clearLayer(rb.gameObject);
        }
      }

      isGrounded = true;
      tetrisBlockSpawner.ClearCurrentBlock();
      tetrisBlockSpawner.SetClearedLayers(layersCleared);

      // detach children and destroy parent
      transform.DetachChildren();
      Destroy(gameObject);
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
        rb.rotation = rb.rotation * rotation;
      }
    }

    private void clearLayer(GameObject block)
    {
        Vector3 center = block.transform.TransformPoint(Vector3.zero);
        float radius = 3;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        // filter for ideal yCoordinate
        float layer = block.transform.TransformPoint(Vector3.zero).y;
        Collider[] hitCollidersWithSameLayer = Array.FindAll(hitColliders,
          hitCollider => Mathf.Abs(
            hitCollider.gameObject.transform.TransformPoint(Vector3.zero).y - layer) < 0.1f
            && hitCollider.gameObject.tag == "Floor"
            && hitCollider.gameObject.name != "Floor");


        if (hitCollidersWithSameLayer.Length < 9)
        {
          return;
        }

        // clear layer
        foreach (Collider hitCollider in hitCollidersWithSameLayer)
        {
            hitCollider.gameObject.SetActive(false);
        }
        layersCleared.Add((int) Mathf.Round(layer));
    }

}
