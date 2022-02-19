using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationPlayer : MonoBehaviour, Dialogue.DialogueCallbacks
{

    public interface ConversationCallback
    {
        public void DoEvent(Conversation.ConversationLine line, bool start);
        public void ConversationFinished(string id);
    }

    public List<GameObject> portraitModels;
    public List<AudioClip> voices;
    public Dialogue dlg;
    public Text dialogueText;
    public Text spaceText;
    public GameObject dlgWindow;

    Conversation convo;
    Conversation.ConversationLine currentLine;

    Conversation.ConversationLine singleLine;

    public ConversationCallback callback;


    public void PlayConversation(Conversation convo)
    {
        this.convo = convo;
        this.dlg.callbacks = this;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentLine != null && currentLine == singleLine && currentLine.done)
        {
            dlgWindow.SetActive(false);
            singleLine = null;
        }

        if(convo == null)
        {
            return;
        }


        if (currentLine?.type == 1)
        {
            currentLine.time += Time.deltaTime;
            if (currentLine.time > currentLine.length)
            {
                currentLine.done = true;
                currentLine = null;
            }
        }

        if (currentLine?.type == 2 && currentLine.active)
        {
            currentLine.time += Time.deltaTime;
            if (currentLine.time > currentLine.length)
            {
                if(callback != null)
                {
                    callback.DoEvent(currentLine, false);
                }
               
                currentLine.done = true;
                currentLine = null;
            }
        }

        if (spaceText.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine.done = true;
        }


        if (currentLine == null || currentLine.done)
        {
            //get new line
            currentLine = null;
            foreach (Conversation.ConversationLine line in convo.lines)
            {
                if (!line.done)
                {
                    PlayLine(line);
                    break;
                }
            }
            if (currentLine == null && callback != null)
            {
                dlgWindow.SetActive(false);
                callback.ConversationFinished(convo.id);
                convo = null;
            }
        }



    }

    public void PlaySingleLine(int character, string text)
    {
        singleLine = new Conversation.ConversationLine();
        singleLine.text = text;
        singleLine.type = 3;
        PlayLine(singleLine);
    }

    void PlayLine(Conversation.ConversationLine line)
    {
        spaceText.gameObject.SetActive(false);
        currentLine = line;
        if (line.type == 0 || line.type == 3)
        {
            foreach (GameObject gm in portraitModels)
            {
                gm.SetActive(false);
            }
            portraitModels[line.character].SetActive(true);
            dlg.clip = voices[line.character];
            dlgWindow.SetActive(true);
            dlg.PlayLine(line.text);
        }
        if (line.type == 1)
        {
            dlgWindow.SetActive(false);
        }
        if (line.type == 2)
        {
            dlgWindow.SetActive(false);
            callback.DoEvent(line, true);
        }
    }

    public void OnFinishText()
    {
        if (currentLine.type == 3)
        {
            currentLine.done = true;
        }
        else
        {
            spaceText.gameObject.SetActive(true);
        }
    }

    public void OnTextUpdate(string currentText)
    {
        dialogueText.text = currentText;
    }

}
