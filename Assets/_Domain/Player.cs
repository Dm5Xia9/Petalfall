using StarterAssets;
using System;

public class Player
{
    private readonly ThirdPersonController _controller;

    public Player(ThirdPersonController controller)
    {
        _controller = controller;
        if (Instance != null)
            throw new Exception("ТЫ нахуя второго пользователя въебал дурень");

        Instance = this;
    }

    public ThirdPersonController Controller => _controller;
    public static Player Instance { get; private set; }
    public int Balance { get; set; }

    public bool EnoughBalance(int m)
    {
        return Balance >= m;
    }

    public bool HandIsEmpty()
    {
        return HandEntity == null;
    }


    public IEntity? HandEntity { get; set; }

    public void PickupHandEntity(IEntity target)
    {
        if (!HandIsEmpty()) return;

        HandEntity = target;
        HandEntity.Unity.GameObject.transform.SetParent(_controller.Hand.transform, false);
        HandEntity.Unity.ToHandPosition();

        if (HandEntity.Unity.Rigidbody != null)
            HandEntity.Unity.Rigidbody.isKinematic = true;

        HandEntity.Unity.HiddenPlaceholder();
    }

    public void DropHandEntity()
    {
        HandEntity.Unity.GameObject.transform.SetParent(Controller.ItemsParent, true);

        if (HandEntity.Unity.Rigidbody != null)
            HandEntity.Unity.Rigidbody.isKinematic = false;

        HandEntity = null;
    }

    public bool UseHand(IEntity target)
    {
        if (target.CanUse(HandEntity) == false)
            return false;

        target.Use(HandEntity);
        return true;
    }

    public void UserInputDisable()
    {
        _controller.playerInput.enabled = false;
    }

    public void UserInputEnable()
    {
        _controller.playerInput.enabled = true;
    }

    public bool IsUserInputEnable()
    {
        return _controller.playerInput.enabled == true;
    }
}
