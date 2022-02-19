using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InteractionSO", order = 1)]
public class InteractionSO : ScriptableObject
{
    public enum InteractionType {RoomTransition, Dialogue, Event}
    public InteractionType type;
    public string id;
    public TextAsset dialogue;
    public int roomEnd;
    public int spawnpoint;
}

