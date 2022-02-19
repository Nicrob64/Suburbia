using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionListener : MonoBehaviour
{
    public interface InteractionListenerCallback
    {
        void DoInteraction(InteractionPoint interaction);
    }

    public InteractionListenerCallback callback;
    InteractionPoint currentInteraction;
    public GameObject interactionIcon;
    public Camera currentCamera;
    bool dontDisable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<CharacterController>().enabled)
        {
            if (!dontDisable)
            {
                interactionIcon.SetActive(false);
            }
            return;
        }

        Transform t = currentCamera.transform;
        interactionIcon.transform.LookAt(t);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentInteraction != null)
            {
                callback.DoInteraction(currentInteraction);
                currentInteraction = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       InteractionPoint point = other.gameObject.GetComponent<InteractionPoint>();
       if(point != null)
        {
            currentInteraction = point;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractionPoint point = other.gameObject.GetComponent<InteractionPoint>();
        if (point != null)
        {
            interactionIcon.SetActive(false);
            currentInteraction = null;
        }
        
    }

    public void ForceEnable(bool force)
    {
        interactionIcon.SetActive(force);
        dontDisable = force;
    }
}
