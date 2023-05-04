using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisBlockMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody rb;

    private ParentController parentController;

    private int moveDownByAmount = 0;

    // check for grounding
    private bool isGrounded = false;

    // check if game over
    private GameObject stopPosition;

    // snap to positions (hardcoded)
    private float xOffset = -0.2f;
    private float zOffset = -0.2f;

    // speed of downward movement
    [SerializeField] private float downwardSpeed = 0.5f;

    private void Start()
    {
      rb = GetComponent<Rigidbody>();

      if (transform.parent.gameObject != null)
      {
        parentController = transform.parent.gameObject.GetComponent<ParentController>();
      }

      stopPosition = GameObject.Find("StopPosition");
    }

/*
    private void FixedUpdate()
    {
      // downward movement
      Vector3 force = new Vector3(0, -10000f, 0);
      rb.AddForce(force, ForceMode.VelocityChange);
    }
    */

    private void Update()
    {
      if (isGrounded)
      {
        return;
      }
      // Calculate absolute direction based on current rotateion
      Vector3 worldDirection = transform.TransformDirection(Vector3.down);
      Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
      Vector3 absoluteDirection = inverseRotation * worldDirection;

      transform.position += absoluteDirection * Time.deltaTime * downwardSpeed;
    }

    public void CheckIndividualPositions()
    {
      if (transform.position.y > stopPosition.transform.position.y)
      {
        SceneManager.LoadScene("GameOver");
      }
    }

    public void HandleSnap()
    {
      // handle offset on movement
      Vector3 roundedPos = new Vector3(Mathf.Round(transform.position.x) - xOffset,
                            transform.position.y, Mathf.Round(transform.position.z) - zOffset);
      transform.position = roundedPos;
    }

    public void changeTag(string tag)
    {
      isGrounded = true;
      gameObject.tag = tag;
    }

    public void moveBlock(Vector3 dir)
    {
      // Calculate absolute direction based on current rotateion
      Vector3 worldDirection = transform.TransformDirection(dir);
      Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
      Vector3 absoluteDirection = inverseRotation * worldDirection;

      transform.position += absoluteDirection;
    }

    public void placeFast(Vector3 destination)
    {
      // find nearest floor tile
      // change local transform to that location
      transform.position = destination;
    }

}
