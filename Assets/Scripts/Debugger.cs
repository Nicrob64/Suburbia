using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public Dialogue dlg;
    public Camera cam;
    public Easings.Functions easeType = Easings.Functions.QuadraticEaseInOut;
    public Transform start, end;
    public float time = 3.0f;
    bool animating = false;
    float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Hnmnmn");
            dlg.PlayLine("Wow I cant believe this is working what the hell bro what the heck is going on");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            dlg.PlayLine("Wow I cant believ");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            dlg.PlayLine("Wow I cant believe this is working what the hell bro what the heck is going onWow I cant believe this is working what the hell bro what the heck is going onWow I cant believe this is working what the hell bro what the heck is going onWow I cant believe this is working what the hell bro what the heck is going on");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            cam.gameObject.SetActive(true);
            if (Camera.main != null && Camera.main != cam) { 
                Camera.main.gameObject.SetActive(false);
            }
            cam.transform.position = start.position;
            cam.transform.rotation = start.rotation;
            animating = true;
            currentTime = 0;
        }


        if (animating)
        {
            currentTime += Time.deltaTime;
            float pc = Easings.Interpolate(currentTime / time, easeType);
            Vector3 pos = Vector3.Lerp(start.position, end.position, pc);
            Vector3 rot = Vector3.Lerp(start.eulerAngles, end.eulerAngles, pc);
            cam.transform.position = pos;
            cam.transform.eulerAngles = rot;
            if(pc >= 1)
            {
                animating = false;
            }
        }

    }
}
