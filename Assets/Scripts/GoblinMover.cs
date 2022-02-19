using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMover : MonoBehaviour
{

    public interface GoblinMoverCallback
    {
        void IMadeIt(string id);
    }

    public float playerSpeed = .65f;
    private Animator anim;
    private AudioSource audioo;
    public AudioClip walkClip;
    float timeSinceLastNoise = 0, breakBetweenSounds = 0.5f;
    public float frequencyVariance = 0.01f;
    public Vector3 target;
    public float minDistance = 0.1f;
    bool move = false;
    string moveId;
    public GoblinMoverCallback callback;
    public float pitchOffset = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        audioo = gameObject.GetComponent<AudioSource>();
        audioo.clip = walkClip;
        audioo.volume = 0.1f;
        audioo.pitch = pitchOffset;
    }

    public void DoMove(string id, Vector3 target) {
        this.target = target;
        this.moveId = id;
        move = true;
        anim.SetFloat("Speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (Vector3.Distance(transform.position, target) < 0.1)
            {
                move = false;
                if(callback != null)
                {
                    anim.SetFloat("Speed", 0);
                    callback.IMadeIt(moveId);
                }
            }
            Vector3 dir = target - transform.position;
            dir.Normalize();
            transform.position += (dir * Time.deltaTime * playerSpeed);


            if (dir != Vector3.zero)
            {
                gameObject.transform.forward = dir;

                timeSinceLastNoise += Time.deltaTime;
                if (timeSinceLastNoise > breakBetweenSounds)
                {
                    float dist = Random.Range(-frequencyVariance, frequencyVariance);
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
}
