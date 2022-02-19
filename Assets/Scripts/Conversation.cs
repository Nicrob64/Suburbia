using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class Conversation 
{
    public List<ConversationLine> lines;
    public string id;

    [Serializable]
    public class ConversationLine
    {
        public int type;
        public int character;
        public string text;
        public float length;
        public bool done = false;
        public float time = 0;
        public bool active = false;
    }
}

