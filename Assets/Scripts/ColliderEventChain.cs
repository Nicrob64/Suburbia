using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEventChain : MonoBehaviour
{
    public interface ColliderEventChainCallback
    {
        void DoOnTriggerEnter(Collider other);
    }

    public ColliderEventChainCallback callback;

    private void OnTriggerEnter(Collider other)
    {
        if(callback != null)
        {
            callback.DoOnTriggerEnter(other);
        }
    }
}
