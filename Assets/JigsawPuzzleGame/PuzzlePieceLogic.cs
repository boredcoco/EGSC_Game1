using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceLogic : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float xForce = 0.01f;
    [SerializeField] private float yForce = 0.01f;

    private Vector3 originalMousePos;
    private Rigidbody2D rb;

    private void Start()
    {
      cam = Camera.main;
      rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
      originalMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
      /*
      Vector3 currentMousePos = Input.mousePosition;
      if (currentMousePos.x > originalMousePos.x)
      {
        transform.position = new Vector3(transform.position.x + xForce,
          transform.position.y, transform.position.z);
        Debug.Log("moving right");
      } else
      {
        transform.position = new Vector3(transform.position.x - xForce,
          transform.position.y, transform.position.z);
        Debug.Log("moving left");
      }

      if (currentMousePos.y > originalMousePos.y)
      {
        transform.position = new Vector3(transform.position.x,
          transform.position.y + yForce, transform.position.z);
        Debug.Log("moving up");
      } else {
        transform.position = new Vector3(transform.position.x,
          transform.position.y - yForce, transform.position.z);
        Debug.Log("moving down");
      }
      originalMousePos = currentMousePos;
      */

      // Sort out the cam to screen world point thing
      Vector3 point = new Vector3();
      // Event currentEvent = Event.current;
      Vector2 mousePos = new Vector2();

      mousePos.x = Input.mousePosition.x;
      mousePos.y = cam.pixelHeight - Input.mousePosition.y;
      point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
      point.z = 0;
      Debug.Log(point);

      float step = 0.1f;
      transform.position = Vector3.MoveTowards(transform.position, point, step);

    }
}
