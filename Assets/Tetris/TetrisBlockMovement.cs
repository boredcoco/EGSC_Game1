using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisBlockMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Rigidbody rb;

    // snap to positions
    private float xOffset = 0.2f;
    private float zOffset = 0.2f;

    private ParentController parentController;

    private int moveDownByAmount = 0;

    // check if game over
    private GameObject stopPosition;

    private void Start()
    {
      rb = GetComponent<Rigidbody>();

      if (transform.parent.gameObject != null)
      {
        parentController = transform.parent.gameObject.GetComponent<ParentController>();
      }

      stopPosition = GameObject.Find("StopPosition");
    }

    private void FixedUpdate()
    {
      // downward movement
      Vector3 force = new Vector3(0, -1 * movementSpeed, 0);
      rb.AddForce(force, ForceMode.VelocityChange);
    }

    public void HandleSnap()
    {
      Vector3 roundedPos = new Vector3(Mathf.Round(transform.position.x) + xOffset,
                            transform.position.y, Mathf.Round(transform.position.z) + zOffset);
      transform.position = roundedPos;

      if (transform.position.y > stopPosition.transform.position.y)
      {
        SceneManager.LoadScene("GameOver");
      }
    }

    public void changeTag(string tag)
    {
      gameObject.tag = tag;
    }

    public void increaseMoveDownAmount()
    {
      moveDownByAmount += 1;
    }

    public void moveBlockDownwards()
    {
      transform.position -= new Vector3(0, 0.9f * moveDownByAmount, 0);
      moveDownByAmount = 0;
    }

    public void moveBlock(Vector3 dir)
    {
      transform.position += dir;
    }
}
