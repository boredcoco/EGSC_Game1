using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceLogic : MonoBehaviour
{
    private Camera cam;

    private Vector3 rightPosition;

    private Vector3 originalMousePos;

    private LogicManager logicManager;

    private void Start()
    {
      cam = Camera.main;
      rightPosition = transform.position;
      logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
    }

    private void OnMouseDown()
    {
      originalMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
      // Sort out the cam to screen world point thing
      Vector3 point = new Vector3();
      Vector2 mousePos = new Vector2();

      mousePos.x = Input.mousePosition.x;
      mousePos.y = Input.mousePosition.y;
      point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + 10));
      point.z = 0;

      if (logicManager.IsWithinBounds(point))
      {
        transform.position = point;
      }
    }
}
