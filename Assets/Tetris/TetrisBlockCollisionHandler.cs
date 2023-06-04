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
      // change these conditions, especially the second one, shoudl only have one checkfloor func
        if (parentController != null
        && (checkIfFloorIsBelow() || checkIfFloorIsBelowRayCast())
        && collision.gameObject.tag == "Floor")
        {
          parentController.FreezeAllChildrenPositions();
        }
    }


    public bool checkIfFloorIsBelowRayCast()
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


    public bool checkIfFloorIsBelow()
    {
      // Calculate absolute direction based on current rotateion
      Vector3 worldDirection = transform.TransformDirection(Vector3.down);
      Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
      Vector3 absoluteDirection = inverseRotation * worldDirection;

      RaycastHit hit;
      bool hitDetect = Physics.BoxCast(rb.transform.position, rb.transform.localScale / 2, absoluteDirection, out hit, rb.rotation, Mathf.Infinity);

      if (hitDetect)
      {
          if (hit.distance < 1f && hit.transform.parent != gameObject.transform.parent)
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
      bool hitDetect = Physics.BoxCast(transform.position, transform.localScale / 2, absoluteDirection, out hit, transform.rotation, Mathf.Infinity);

      if (hitDetect)
      {
          if (hit.distance < 0.95f && hit.transform.parent != gameObject.transform.parent)
          {
            return true;
          }
          return false;
      }
      return false;
    }

    public float getClosestBottomCoordinate()
    {
      // Calculate absolute direction based on current rotateion
      Vector3 worldDirection = transform.TransformDirection(Vector3.down);
      Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
      Vector3 absoluteDirection = inverseRotation * worldDirection;

      RaycastHit hit;
      bool hitDetect = Physics.BoxCast(transform.position, transform.localScale / 2, absoluteDirection, out hit, transform.rotation, Mathf.Infinity);

      if (hitDetect)
      {
          if (hit.transform.gameObject.name == "Floor") {
            return transform.position.y - hit.transform.position.y - (0.5f);
          }
          if (hit.transform.parent != gameObject.transform.parent)
          {
            return transform.position.y - hit.transform.position.y - 1;
          }
          return Mathf.Infinity;
      }
      return Mathf.Infinity;
    }

    public void moveDownwardByOne()
    {
      rb.MovePosition(rb.position + new Vector3(0, -1, 0));
    }

}
