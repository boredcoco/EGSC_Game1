using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    private Rigidbody[] childrenRbs;

    private void Start()
    {
      childrenRbs = GetComponentsInChildren<Rigidbody>();
    }

    public void FreezeAllChildrenPositions()
    {
      foreach(Rigidbody rb in childrenRbs)
      {
         rb.constraints = RigidbodyConstraints.FreezePosition;
      }
    }
}
