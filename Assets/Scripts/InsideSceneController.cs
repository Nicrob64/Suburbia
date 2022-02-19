using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideSceneController : MonoBehaviour, InteractionListener.InteractionListenerCallback, ConversationPlayer.ConversationCallback, GoblinMover.GoblinMoverCallback
{
    public InteractionPoint initialInteraction;


    public List<Transform> spawnPoints;
    public List<Camera> roomCameras;
    public InteractionListener interactionListener;
    public CharacterController character;

    Camera currentCamera;

    public ConversationPlayer convoPlayer;
    Conversation convo;

    public AudioSource toilet;
    public Transform pooppoint;

    public Transform couchPoint;
    public GameObject tvScreen;
    public TransitionSO camMoveTv;
    Transition transition;

    public List<TransitionSO> bedCam;

    public Animator goblinAnimator;

    public InteractionPoint goblinPoint;
    public InteractionPoint bathroomMirror;
    public InteractionPoint bedroomMirror;
    public InteractionPoint couchInteractionPoint;
    public InteractionPoint tvPoint;
    public GameObject bigCake;
    public MeshRenderer couch;
    public Material newCouchColour;

    public TransitionSO barrelRoll;

    public GameObject superRoach;
    public GameObject superRoachBox;
    public Transform roachTarget;

    public GameObject warpDoor1;
    public GameObject warpDoor2;

    public GameObject glitchDoor;
    public Quitter quitter;

    int currentRoom = 0;
    int numloops = 0;

    bool[] roomsVisited = {false, false, false, false};


    public GoblinMover goblinMover;
    public List<Transform> goblinPath;

    public Transform rangeHood;
    public TransitionSO rangeTransition;

    public void ConversationFinished(string id)
    {

        character.enabled = true;

        if (id == "goblin_intro")
        {
            goblinPoint.NextStage();
            bathroomMirror.NextStage();
            bedroomMirror.NextStage();
        }
        if (id == "check_e")
        {
            goblinPoint.NextStage();
        }
        if (id == "goblin_e_checked")
        {
            goblinPoint.NextStage();
        }
        if (id == "tv_time")
        {
            couchInteractionPoint.NextStage();
            tvPoint.NextStage();
        }
        if (id == "goblin_what_now")
        {
            goblinPoint.gameObject.SetActive(false);
            goblinMover.DoMove("path_0", goblinPath[0].position);
        }

        if (id == "goblin_this_will_do")
        {
            goblinPoint.NextStage();
            goblinMover.gameObject.transform.position = goblinPath[2].position;
            goblinMover.transform.eulerAngles = goblinPath[2].eulerAngles;
        }
        if (id == "super_roach")
        {
            goblinPoint.NextStage();
            goblinMover.DoMove("roach_2", goblinPath[4].position);
        }

        if (id == "goblin_credits")
        {
            goblinMover.gameObject.SetActive(false);
        }

        if (id == "going_to_credits")
        {
            character.Warp(spawnPoints[13].position, spawnPoints[13].eulerAngles);
            for (int i = 0; i < roomCameras.Count; i++)
            {
                roomCameras[i].gameObject.SetActive(i == 7);
            }
        }

        if (id == "leaving_game_completely")
        {
            character.gameObject.SetActive(false);
            warpDoor2.SetActive(false);
            quitter.gameObject.SetActive(true);
        }


    }

    public void DoEvent(Conversation.ConversationLine line, bool start)
    {
        if(convo.id == "toilet_time")
        {
            if(line.character == 0)
            {
                if (start)
                {
                    character.Warp(pooppoint.position, pooppoint.eulerAngles);
                    character.gameObject.GetComponent<Animator>().SetBool("IsSitting", true);
                    line.active = true;
                }
            }
            if(line.character == 1)
            {
                if (start)
                {
                    toilet.Play();
                    line.active = true;
                }
            }
            if(line.character == 2)
            {
                if (start)
                {
                    character.gameObject.GetComponent<Animator>().SetBool("IsSitting", false);
                    line.active = true;
                }
            }
        }


        if(convo.id == "tv_time")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    character.Warp(couchPoint.position, couchPoint.eulerAngles);
                    character.gameObject.GetComponent<Animator>().SetBool("IsSitting", true);
                    line.active = true;
                    transition.DoTransition(currentCamera.transform, camMoveTv);
                }
            }
            if (line.character == 1)
            {
                if (start)
                {
                    line.active = true;
                    tvScreen.SetActive(true);
                }
                if (!start)
                {
                    tvScreen.SetActive(false);
                    transition.DoTransition(currentCamera.transform, camMoveTv, true);
                }
            }
            if (line.character == 2)
            {
                if (start)
                {
                    character.gameObject.GetComponent<Animator>().SetBool("IsSitting", false);
                    line.active = true;
                }
            }
        }



        if (convo.id == "bed_time")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    transition.DoTransition(currentCamera.transform, bedCam[0]);
                }
            }
            if (line.character == 1)
            {
                if (start)
                {
                    line.active = true;
                    transition.DoTransition(currentCamera.transform, bedCam[1]);
                }
            }
        }


        if (convo.id == "goblin_intro")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    goblinAnimator.SetTrigger("Attack");
                }
            }
            if (line.character == 1)
            {
                if (start)
                {
                    line.active = true;
                    interactionListener.ForceEnable(true);
                }
                else
                {
                    interactionListener.ForceEnable(false);
                }
            }
        }


        if (convo.id == "check_e")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    interactionListener.ForceEnable(true);
                }
            }
            if (line.character == 1)
            {
                if (start)
                {
                    line.active = true;
                    interactionListener.ForceEnable(false);
                }
            }
        }


        if (convo.id == "goblin_e_checked")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    couch.material = newCouchColour;
                }
            }
            if (line.character == 1)
            {
                if (start)
                {
                    line.active = true;
                    bigCake.SetActive(true);
                }
            }
            if (line.character == 2)
            {
                if (start)
                {
                    line.active = true;
                    superRoach.SetActive(true);
                }
                else
                {
                    superRoachBox.SetActive(false);
                }
            }
        }


        if (convo.id == "super_roach")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    superRoachBox.SetActive(true);
                }
                else
                {
                    superRoach.SetActive(false);
                }
            }
            if (line.character == 1)
            {
                if (start)
                {
                    line.active = true;
                    goblinMover.DoMove("roach_pos", roachTarget.position);
                }
            }
        }


        if (convo.id == "goblin_this_will_do")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    warpDoor1.SetActive(true);
                }
            }
           
        }


        if (convo.id == "goblin_credits")
        {
            if (line.character == 0)
            {
                if (!start)
                {
                    warpDoor2.SetActive(true);
                }
                else
                {
                    line.active = true;
                    goblinMover.DoMove("path_2", goblinPath[3].position);
                }
            }

        }

        if (convo.id == "oven")
        {
            if (line.character == 0)
            {
                if (start)
                {
                    line.active = true;
                    transition.DoTransition(rangeHood, rangeTransition);
                }
            }

        }

    }

    public void DoInteraction(InteractionPoint point)
    {

        if (point.interaction.type == InteractionSO.InteractionType.RoomTransition)
        {
            for (int i = 0; i < roomCameras.Count; i++)
            {
                roomCameras[i].gameObject.SetActive(point.interaction.roomEnd == i);
                if(point.interaction.roomEnd == i)
                {
                    interactionListener.currentCamera = roomCameras[i];
                    currentCamera = roomCameras[i];
                }
            }
            currentRoom = point.interaction.roomEnd;
            if(currentRoom > 2)
            {
                if (!roomsVisited[currentRoom - 3]) {
                    roomsVisited[currentRoom - 3] = true;
                    bool test = true;
                    for (int i = 0; i < 3; i++) {
                        test = test && roomsVisited[i];
                    }
                    if (test)
                    {
                        goblinMover.gameObject.SetActive(true);
                    }
                }
            }
            CharacterControllerExtensions.Warp(character, spawnPoints[point.interaction.spawnpoint].position);
            if (point.interaction.id != null && point.interaction.id == "hall_loop")
            {
                numloops++;
                switch (numloops)
                {
                    case 1:
                        convoPlayer.PlaySingleLine(0, "Woah this is a long hallway...      ");
                        break;
                    case 2:
                        convoPlayer.PlaySingleLine(0, "What the heck?     ");
                        break;
                    case 3:
                        convoPlayer.PlaySingleLine(0, "What is going on?       ");
                        break;
                    case 4:
                        convoPlayer.PlaySingleLine(0, "Why do I keep going this way?       ");
                        break;
                    case 5:
                        glitchDoor.SetActive(false);
                        convoPlayer.PlaySingleLine(0, "Huh?? Where did the door go?       ");
                        break;
                }
                //DoSomethingRandom();
            }
        }

        if(point.interaction.type == InteractionSO.InteractionType.Dialogue)
        {

            convo = JsonUtility.FromJson<Conversation>(point.interaction.dialogue.text);
            character.enabled = false;
            convoPlayer.PlayConversation(convo);

            if(point.interaction.id == "oven")
            {
                point.gameObject.SetActive(false);
            }
        }

        if(point.interaction.type == InteractionSO.InteractionType.Event)
        {
            DoInteractionEvent(point);
        }
    }


    void DoInteractionEvent(InteractionPoint p)
    {
        if(p.interaction.id == "light")
        {
            p.affectedObject.SetActive(!p.affectedObject.activeInHierarchy);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        interactionListener.callback = this;
        convoPlayer.callback = this;
        transition = gameObject.GetComponent<Transition>();
        
        foreach(Camera cam in roomCameras)
        {
            if (cam.gameObject.activeInHierarchy)
            {
                currentCamera = cam;
                interactionListener.currentCamera = cam;
                break;
            }
        }
        goblinMover.callback = this;
        DoInteraction(initialInteraction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoSomethingRandom()
    {
        float val = Random.Range(0.0f, 1.0f);
        if(currentRoom == 1 || currentRoom == 2)
        {
            if(val < 0.5f)
            {
                transition.DoTransition(currentCamera.transform, barrelRoll);
            }
        }
    }

    public void IMadeIt(string id)
    {
        if(id == "path_0")
        {
            goblinMover.gameObject.transform.position = spawnPoints[8].position;
            goblinMover.DoMove("path_1", goblinPath[1].position);
        }
        if (id == "path_1")
        {
            goblinPoint.gameObject.SetActive(true);
            goblinPoint.NextStage();
        }
    }
}
