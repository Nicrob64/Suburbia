using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCarLoop : MonoBehaviour
{
    public Transform loopStart;
    public Transform loopEnd;
    public float carSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 m = transform.position;
        m.x += Time.deltaTime * carSpeed;
        
        if(m.x < loopEnd.position.x)
        {
            float diff = m.x - loopEnd.position.x;
            m.x = loopStart.position.x + diff;
        }

        transform.position = m;
    }
}
