using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzlePieceLogic : MonoBehaviour
{
    private Camera cam;

    private Vector3 rightPosition;
    private bool inRightPosition = false;

    private LogicManager logicManager;
    private string pieceId;

    // related to layering
    private SortingGroup sortingGroup;
    private int initialOrder = 0;

    private void Start()
    {
      cam = Camera.main;
      rightPosition = transform.position;
      logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();

      // Set the random position
      transform.position = logicManager.GenerateRandomPoint();

      // Set the piece unique id
      pieceId = Random.Range(0f, 1000000000f).ToString();

      // get child
      sortingGroup = GetComponent<SortingGroup>();
      initialOrder = sortingGroup.sortingOrder;
    }

    private void Update()
    {
      if (Vector3.Distance(transform.position, rightPosition) < 0.5f)
      {
        transform.position = rightPosition;
        inRightPosition = true;
      } else
      {
        inRightPosition = false;
      }
      logicManager.HandlePieceUpdate(pieceId, inRightPosition);
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

      // Change order in layer
      sortingGroup.sortingOrder = 20;
    }

    private void OnMouseUp()
    {
      // reset the order
      sortingGroup.sortingOrder = initialOrder;
    }
}
