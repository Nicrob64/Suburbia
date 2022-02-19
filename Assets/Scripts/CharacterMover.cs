using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = .75f;
    private Animator anim;
    public Vector3 directionOffset = new Vector3(1, 1, 1);
    private AudioSource audioo;
    public AudioClip walkClip;
    float timeSinceLastNoise = 0, breakBetweenSounds = 0.4f;
    public float frequencyVariance = 0.01f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        audioo = gameObject.GetComponent<AudioSource>();
        audioo.clip = walkClip;
        audioo.volume = 0.1f;
    }

    void Update()
    {
        if (!controller.enabled) {
            anim.SetFloat("Speed", 0);
            return; 
        }


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move.Scale(directionOffset);
        move.Normalize();
        controller.Move(move * Time.deltaTime * playerSpeed);
        anim.SetFloat("Speed", move.magnitude);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;

            timeSinceLastNoise += Time.deltaTime;
            if (timeSinceLastNoise > breakBetweenSounds)
            {
                float dist = Random.Range(-frequencyVariance, frequencyVariance);
                audioo.pitch = 1.0f + dist;
                audioo.Play();
                timeSinceLastNoise = 0;
            }
        }
        else
        {
            timeSinceLastNoise = 0;
        }
    }
}
