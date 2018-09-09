using UnityEngine;
using System.Collections;
using System;

public class SmartSuitSender : MonoBehaviour
{
    private WebSocket w;
    public String uri = "ws://127.0.0.1:3000";

    IEnumerator Start()
    {
        w = new WebSocket(new Uri(uri));
        yield return StartCoroutine(w.Connect());
    }

    private void Update()
    {
        TraverseHierarchy(this.gameObject.transform);
    }

    void TraverseHierarchy(Transform root)
    {
        foreach (Transform child in root)
        {
            float px = child.transform.position.x;
            float py = child.transform.position.y;
            float pz = child.transform.position.z;
            float rx = child.transform.rotation.x;
            float ry = child.transform.rotation.y;
            float rz = child.transform.rotation.z;
            String msg = "{";
            msg += "\"name\":\"" + child.name + "\",";
            msg += "\"px\":" + px + ",";
            msg += "\"py\":" + py + ",";
            msg += "\"pz\":" + pz + ",";
            msg += "\"rx\":" + rx + ",";
            msg += "\"ry\":" + ry + ",";
            msg += "\"rz\":" + rz;
            msg += "}";
            //Debug.Log(msg);
            w.SendString(msg);
            TraverseHierarchy(child);
        }
    }
}
