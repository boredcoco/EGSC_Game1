using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody rb;

    private bool isGrounded = false;

    // snap to positions
    private float xOffset = 0.2f;
    private float zOffset = 0.2f;

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
    }

    public void SetIsGrounded()
    {
      Vector3 roundedPos = new Vector3(Mathf.Round(transform.position.x) + xOffset,
                            transform.position.y, Mathf.Round(transform.position.z) + zOffset);
      transform.position = roundedPos;
      isGrounded = true;
    }

    public void changeTag(string tag)
    {
      gameObject.tag = tag;
    }

    public void moveBlock(Vector3 dir)
    {
      rb.MovePosition(transform.position + dir);
    }
}
