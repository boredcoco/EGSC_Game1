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

    private HashSet<int> layersCleared = new HashSet<int>();

    // for rotation
    private Quaternion currentRotation = Quaternion.identity;

    // for position snapping
    [SerializeField] private GameObject rootElement;
    private Vector3 lastPos;
    private float[] xPositions = {0.2f, -0.8f, -1.8f};
    private float[] zPositions = {-0.8f, -1.8f, -2.8f};

    private void Start()
    {
      lastPos = rootElement.transform.position;
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

    private void Update()
    {
      if (isGrounded)
      {
        return;
      }
      // handle the case where space is pressed to drop
      if (Input.GetKeyDown(KeyCode.Space))
      {
        float currentSmallest = Mathf.Infinity;
        foreach(TetrisBlockCollisionHandler cHandler in childCollisionHandlers)
        {
          float res = cHandler.getClosestBottomCoordinate();
          currentSmallest = res < currentSmallest ? res : currentSmallest;
        }
        foreach(TetrisBlockMovement childMovement in childMovements)
        {
          childMovement.moveBlock(new Vector3(0, -currentSmallest, 0));
        }
        FreezeAllChildrenPositions();
        return;
      }
      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX, 0, 0);

      // Move along z-axis
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 directionZ = new Vector3(0, 0, operationZ);

      foreach(TetrisBlockCollisionHandler cHandler in childCollisionHandlers)
      {
        if (cHandler.checkIfFloorIsBelow())
        {
          return;
        }
        if (cHandler.checkIfWillHitSideWall(directionX))
        {
          directionX = new Vector3(0, 0, 0);
        }
        if (cHandler.checkIfWillHitSideWall(directionZ))
        {
          directionZ = new Vector3(0, 0, 0);
        }
      }
      foreach(TetrisBlockMovement childMovement in childMovements)
      {
        childMovement.moveBlock(directionX);
        childMovement.moveBlock(directionZ);
      }

    }


    private void FixedUpdate()
    {
      if (currentRotation == Quaternion.identity)
      {
        return;
      }

      foreach(TetrisBlockCollisionHandler collisionHandler in childCollisionHandlers)
      {
        if (collisionHandler.checkIfFloorIsBelow())
        {
          currentRotation = Quaternion.identity;
          return;
        }
      }
      lastPos = rootElement.transform.position;

      // handle rotation
      foreach(Rigidbody rb in childrenRbs)
      {
        rb.rotation *= currentRotation;
        // rb.MoveRotation(currentRotation);
      }
      currentRotation = Quaternion.identity;
    }

    private void LateUpdate()
    {
      Vector3 rootElemPos = findIdealPos(rootElement.transform.position);
      rootElement.transform.position = rootElemPos;
      // Vector3 rootElemPos = new Vector3(-0.8f, rootElement.transform.position.y, -1.8f);

/*
      foreach(Rigidbody rb in childrenRbs)
      {
        rb.transform.position = findIdealPos(rb.transform.position);
        // rb.transform.position = handleSnap(rb.transform.position);
      }
      */
    }

    public void FreezeAllChildrenPositions()
    {
      foreach(TetrisBlockMovement childMovement in childMovements)
      {
        if (childMovement != null)
        {
          childMovement.CheckIndividualPositions();
          childMovement.changeTag("Floor");
        }
      }
      foreach(Rigidbody rb in childrenRbs)
      {
        if (rb != null)
        {
          // rb.transform.position = findIdealPos(rb.transform.position);
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

    public void RotateAlong(Quaternion rotation)
    {
      if (isGrounded)
      {
        return;
      }
      currentRotation *= rotation;
    }

    private void clearLayer(GameObject block)
    {
        Vector3 center = block.transform.TransformPoint(Vector3.zero);
        float radius = 10f;
        // Collider[] hitColliders = Physics.OverlapSphere(center, radius);
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
            Destroy(hitCollider.gameObject);
            // hitCollider.gameObject.SetActive(false);
        }
        layersCleared.Add((int) Mathf.Round(layer));

        // update score
        tetrisBlockSpawner.UpdateScore();
    }

    private Vector3 findIdealPos(Vector3 currentPos)
    {
      Vector3 currentVector = new Vector3(xPositions[0], currentPos.y, zPositions[0]);
      float currentLowest = Vector3.Distance(new Vector3(xPositions[0], currentPos.y, zPositions[0]), currentPos);

      foreach(float xPos in xPositions)
      {
        foreach(float zPos in zPositions)
        {
          float distBetween = Vector3.Distance(new Vector3(xPos, currentPos.y, zPos), currentPos);
          if (distBetween < currentLowest)
          {
            currentVector = new Vector3(xPos, currentPos.y, zPos);
            currentLowest = distBetween;
          }
        }
      }

      return currentVector;
    }

    public Vector3 handleSnap(Vector3 currentPos)
    {
      Vector3 roundedPos = new Vector3(Mathf.Round(currentPos.x) + 0.2f,
                            currentPos.y, Mathf.Round(currentPos.z) + 0.2f);
      return roundedPos;
    }

}
