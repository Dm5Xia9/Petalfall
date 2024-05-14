using DialogueEditor;

public class Jellyfish : Entity<Jellyfish, JellyfishScript>
{
    public bool _isFirstDialog = true;
    public bool _isActiveDialog = false;

    public Jellyfish(JellyfishScript gameObject) : base(gameObject)
    {
        ConversationManager.OnConversationEnded += () =>
        {
            Player.Instance.UserInputEnable();
            _isActiveDialog = false;
        };
    }

    public override string Title => "Котомедузка";

    public override bool IsItem => false;

    public override bool CanCleaned => false;

    public override bool CanUse(IEntity target)
    {
        return Unity.InTimeline() && _isActiveDialog == false;
    }

    public override void Use(IEntity target)
    {
        Player.Instance.UserInputDisable();
        if (_isFirstDialog == true)
        {
            Unity.StartFirstDialog();
            _isFirstDialog = false;
            _isActiveDialog = true;
        }
        else
        {
            Unity.StartRandomHints();
            _isActiveDialog = true;
        }

        base.Use(target);
    }
}
