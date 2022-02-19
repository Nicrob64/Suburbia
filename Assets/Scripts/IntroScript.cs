using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour, ConversationPlayer.ConversationCallback, ColliderEventChain.ColliderEventChainCallback, Transition.TransitionCallback, InteractionListener.InteractionListenerCallback
{

   
    public List<TextAsset> conversations;
    List<Conversation> convos = new List<Conversation>();

    public List<MeshRenderer> glitchMesh;
    public Material glitchMat;
    Material[] oldMats;
    public Collider sphere;
    Conversation.ConversationLine currentLine;
    MeshRenderer currentGlitchMesh;

    public ConversationPlayer convoPlayer;
    public ColliderEventChain colEv;
    public DriveCarLoop carMover;

    public List<TransitionSO> carTransition;
    public Transform car;
    public GameObject carToVanish;
    int transitionNumber = 0;
    public Transition transition;
    public Transition cameraTransition;
    public TransitionSO camSO;
    public Camera carCam, staticCam;
    public GameObject charController;
    public GameObject otherAudioListener;

    public InteractionListener interactionListener;
    public CharacterController character;
    Conversation convo;

    public GameObject mainMenu;


    // Start is called before the first frame update
    void Start()
    {
        foreach(TextAsset ta in conversations)
        {
            convos.Add(JsonUtility.FromJson<Conversation>(ta.text));
        }
        convoPlayer.callback = this;
        colEv.callback = this;
        transition.callback = this;
        interactionListener.callback = this;
        //convoPlayer.PlayConversation(convos[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mainMenu.SetActive(false);
                convoPlayer.PlayConversation(convos[0]);
                car.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        
    }


    public void DoOnTriggerEnter(Collider other)
    {
        if (other == sphere)
        {
            sphere.gameObject.SetActive(false);
            oldMats = currentGlitchMesh.materials;
            Material[] gmats = new Material[oldMats.Length];
            for (int i = 0; i < oldMats.Length; i++)
            {
                gmats[i] = glitchMat;
            }
            currentGlitchMesh.materials = gmats;
            AudioSource ass = currentGlitchMesh.gameObject.GetComponent<AudioSource>();
            ass.Play();
            currentLine.active = true;
        }
    }

    public void DoEvent(Conversation.ConversationLine line, bool start)
    {
        if(line.type == 2)
        {
            currentLine = line;
            if (start)
            {
                currentGlitchMesh = glitchMesh[line.character];
                sphere.gameObject.SetActive(true);
            }
            else
            {
                AudioSource ass = currentGlitchMesh.gameObject.GetComponent<AudioSource>();
                ass.Stop();
                currentGlitchMesh.materials = oldMats;
            }
        }
    }

    public void ConversationFinished(string id)
    {

        Debug.Log("Convo Finished");
        if(id == "intro")
        {
            carMover.enabled = false;
            Vector3 pos = car.position;
            pos.x = carTransition[0].fromPos.x;
            car.position = pos;
            transition.DoTransition(car, carTransition[0]);
            staticCam.gameObject.SetActive(true);
            carCam.gameObject.SetActive(false);
            carToVanish.SetActive(false);
            otherAudioListener.SetActive(false);
        }
        if(id == "arrived_home")
        {
            charController.SetActive(true);
            transitionNumber = 10;
            transition.DoTransition(car, carTransition[3], true);
            car.gameObject.GetComponent<AudioSource>().Play();
        }
        if(id == "front_door")
        {
            SceneManager.LoadScene(1);
        }
        character.enabled = true;
    }

    public void OnTransitionComplete()
    {
        transitionNumber++;
        if(transitionNumber == 1)
        {
            transition.DoTransition(car, carTransition[1]);
            cameraTransition.DoTransition(staticCam.gameObject.transform, camSO);
        }
        if(transitionNumber == 2)
        {
            transition.DoTransition(car, carTransition[2]);
        }
        if (transitionNumber == 3)
        {
            transition.DoTransition(car, carTransition[3]);
        }
        if (transitionNumber == 4)
        {
            car.gameObject.GetComponent<AudioSource>().Stop();
            convoPlayer.PlayConversation(convos[1]);
        }

        if(transitionNumber == 11)
        {
            transition.DoTransition(car, carTransition[2], true);
        }
        if (transitionNumber == 12)
        {
            transition.DoTransition(car, carTransition[1], true);
        }
        if (transitionNumber == 13)
        {
            transition.DoTransition(car, carTransition[0], true);
        }
        if(transitionNumber == 14)
        {
            car.gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    public void DoInteraction(InteractionPoint point)
    {
        if (point.interaction.type == InteractionSO.InteractionType.Dialogue)
        {
            convo = JsonUtility.FromJson<Conversation>(point.interaction.dialogue.text);
            character.enabled = false;
            convoPlayer.PlayConversation(convo);
        }
    }
}
