using System.Collections.Generic;

using DialogueEditor;

using UnityEngine;

public class CatAbobusController : ActivationEvent
{
    public NPCConversation NPCConversation;

    public List<NPCConversation> PRs;

    public bool isFirst = true;

    public ConversationManager ConversationManager;

    private bool _abobusDialog;

    //public override bool TriggerEnable => _abobusDialog == false;

    //private void Awake()
    //{
    //    ConversationManager.OnConversationEnded += () =>
    //    {
    //        _person.enabled = true;
    //        _abobusDialog = false;
    //        //isFirst = false;
    //    };
    //}
    //protected override void OnActive()
    //{
    //    //Player.isActiveAndEnabled = false;
    //    _person.enabled = false;
    //    _abobusDialog = true;
    //    if (isFirst == true)
    //    {
    //        ConversationManager.StartConversation(NPCConversation);
    //        isFirst = false;
    //    }
    //    else
    //    {
    //        ConversationManager.StartConversation(PRs[Random.Range(0, PRs.Count)]);
    //    }
    //}
}
