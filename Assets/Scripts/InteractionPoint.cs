using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    public InteractionSO interaction;
    public GameObject affectedObject;
    public List<InteractionSO> stages;
    int currentStage = 0;


    private void Start()
    {
        if(stages != null && stages.Count > 0)
        {
            interaction = stages[0];
        }
    }


    public void NextStage()
    {
        currentStage++;
        interaction = stages[currentStage];
    }
}
