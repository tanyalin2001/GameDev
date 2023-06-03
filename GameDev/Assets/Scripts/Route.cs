using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childNodeList = new List<Transform>();

    // a callback function that is called by the editor whenever the scene view is being repainted
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillNodes();
        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if (i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNodeList.Clear();
        childObjects = GetComponentsInChildren<Transform>();
        foreach(Transform child in childObjects)
        {
            // avoid adding parent object (route) to childNodeList
            if(child != this.transform)
            {
                childNodeList.Add(child);
            }
        }
    }
}
