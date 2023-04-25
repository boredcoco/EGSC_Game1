using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockCollisionHandler : MonoBehaviour
{
    private TetrisBlockMovement movementScript;
    private GameObject parent = null;
    private ParentController parentController = null;

    private Rigidbody rb;

    private void Start()
    {
      rb = GetComponent<Rigidbody>();
      movementScript = GetComponent<TetrisBlockMovement>();

      if (transform.parent != null)
      {
        parent = transform.parent.gameObject;
        parentController = parent.GetComponent<ParentController>();
      }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (parentController != null && checkIfFloorIsBelow() && collision.gameObject.tag == "Floor")
        {
          parentController.FreezeAllChildrenPositions();
        }
    }

    public bool checkIfFloorIsBelow()
    {
      // Calculate absolute direction based on current rotateion
      Vector3 worldDirection = transform.TransformDirection(Vector3.down);
      Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
      Vector3 absoluteDirection = inverseRotation * worldDirection;

      RaycastHit hit;
      if (Physics.Raycast(transform.position, absoluteDirection, out hit, Mathf.Infinity))
      {
          if (hit.distance < 0.5f && hit.transform.parent != gameObject.transform.parent)
          {
            return true;
          }
          return false;
      }
      return false;
    }

    public bool checkIfWillHitSideWall(Vector3 dir)
    {
      // Calculate absolute direction based on current rotateion
      Vector3 worldDirection = transform.TransformDirection(dir);
      Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
      Vector3 absoluteDirection = inverseRotation * worldDirection;

      RaycastHit hit;
      if (Physics.Raycast(transform.position, absoluteDirection, out hit, Mathf.Infinity))
      {
          if (hit.distance < 1f && hit.transform.parent != gameObject.transform.parent)
          {
            return true;
          }
          return false;
      }
      return false;
    }

    public void moveDownwardByOne()
    {
      rb.MovePosition(rb.position + new Vector3(0, -1, 0));
    }

}
