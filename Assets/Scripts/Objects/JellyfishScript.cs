using DialogueEditor;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishScript : ObjectMonoBehaviour<Jellyfish, JellyfishScript>
{
    [SerializeField] private NPCConversation _firstDialog;

    [SerializeField] private List<NPCConversation> _hints;
    [SerializeField] private ConversationManager _conversationManager;
    protected override Jellyfish CreateEntity()
    {
        return new Jellyfish(this);
    }

    public void StartFirstDialog()
    {
        _conversationManager.StartConversation(_firstDialog);
    }

    public void StartRandomHints()
    {
        _conversationManager.StartConversation(_hints[UnityEngine.Random.Range(0, _hints.Count)]);
    }
}
