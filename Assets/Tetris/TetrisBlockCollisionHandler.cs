using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockCollisionHandler : MonoBehaviour
{
    private TetrisBlockMovement movementScript;
    private GameObject parent = null;
    private ParentController parentController = null;

    private void Start()
    {
      movementScript = GetComponent<TetrisBlockMovement>();
      if (transform.parent != null)
      {
        parent = transform.parent.gameObject;
        parentController = parent.GetComponent<ParentController>();
      }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (parentController != null && checkIfFloorIsBelow())
        {
          parentController.FreezeAllChildrenPositions();
        }
    }

    private bool checkIfFloorIsBelow()
    {
      RaycastHit hit;
      if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
      {
          if (hit.distance < 0.5f)
          {
            return true;
          }
          return false;
      }
      return false;
    }
}
