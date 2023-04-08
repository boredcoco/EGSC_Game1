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
    }

    public void SetIsGrounded()
    {
      isGrounded = true;
    }

    public void changeTag(string tag)
    {
      gameObject.tag = tag;
    }
}
