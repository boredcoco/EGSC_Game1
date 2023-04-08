using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerClearing : MonoBehaviour
{
    private int index = 0;
    private GameObject[] objectsOnGround = new GameObject[9];

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag != "Floor")
      {
        return;
      }
      objectsOnGround[index] = collision.gameObject;
      index += 1;

      if (index >= objectsOnGround.Length)
      {
        for (int i = 0; i < objectsOnGround.Length; i++)
        {
          Destroy(objectsOnGround[i]);
          objectsOnGround[i] = null;
        }
      }
    }

}
