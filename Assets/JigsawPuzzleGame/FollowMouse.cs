using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
      cam = Camera.main;
    }

    private void Update()
    {
      Vector3 point = new Vector3();
      // Event currentEvent = Event.current;
      Vector2 mousePos = new Vector2();

      mousePos.x = Input.mousePosition.x;
      mousePos.y = Input.mousePosition.y;
      point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + 10));
      point.z = 0;
      Debug.Log(point);

      transform.position = point;
    }
}
