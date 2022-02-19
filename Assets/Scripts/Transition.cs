using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    public interface TransitionCallback
    {
        void OnTransitionComplete();
    }

    Transform toMove;
    TransitionSO data;
    bool animating = false;
    bool reverse = false;
    float time = 0;
    public TransitionCallback callback;

    void Update()
    {
        if (animating) {
            time += Time.deltaTime;
            float pc = Easings.Interpolate(time / data.duration, data.easeType);

            Vector3 pos = Vector3.Lerp(reverse ? data.toPos : data.fromPos, reverse ? data.fromPos : data.toPos, pc);
            Vector3 rot = Vector3.Lerp(reverse ? data.toRot : data.fromRot, reverse ? data.fromRot : data.toRot, pc);
            Vector3 orig = toMove.position;
            Vector3 origR = toMove.eulerAngles;


            switch (data.type)
            {
                case TransitionSO.TransitionType.X:
                    pos.y = orig.y;
                    pos.z = orig.z;
                    toMove.position = pos;
                    break;
                case TransitionSO.TransitionType.Y:
                    pos.x = orig.x;
                    pos.z = orig.z;
                    toMove.position = pos;
                    break;
                case TransitionSO.TransitionType.Z:
                    pos.y = orig.y;
                    pos.x = orig.x;
                    toMove.position = pos;
                    break;
                case TransitionSO.TransitionType.ROTATION:
                    toMove.eulerAngles = rot;
                    break;
                case TransitionSO.TransitionType.ALL:
                    toMove.position = pos;
                    toMove.eulerAngles = rot;
                    break;
                case TransitionSO.TransitionType.RX:
                    rot.z = origR.z;
                    rot.y = origR.y;
                    toMove.eulerAngles = rot;
                    break;
                case TransitionSO.TransitionType.RY:
                    rot.z = origR.z;
                    rot.x = origR.x;
                    toMove.eulerAngles = rot;
                    break;
                case TransitionSO.TransitionType.RZ:
                    rot.x = origR.x;
                    rot.y = origR.y;
                    toMove.eulerAngles = rot;
                    break;
            }

            if (pc >= 1)
            {
                animating = false;
                if(callback != null)
                {
                    callback.OnTransitionComplete();
                }
            }
        }
    }


    public void DoTransition(Transform t, TransitionSO so, bool reverse = false)
    {
        data = so;
        toMove = t;
        time = 0;
        animating = true;
        this.reverse = reverse;
    }
}
