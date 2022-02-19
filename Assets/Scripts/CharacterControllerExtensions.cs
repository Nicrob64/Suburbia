using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterControllerExtensions
{
    public static void Warp(this CharacterController @this, Vector3 target)
    {
        @this.gameObject.transform.position = target;
        Physics.SyncTransforms();
    }

    public static void Warp(this CharacterController @this, Vector3 target, Vector3 euler)
    {
        @this.gameObject.transform.position = target;
        @this.gameObject.transform.eulerAngles = euler;
        Physics.SyncTransforms();
    }

    public static void WarpRotationOnly(this CharacterController @this, Vector3 euler)
    {
        @this.gameObject.transform.eulerAngles = euler;
        Physics.SyncTransforms();
    }

}