using UnityEngine;

public abstract class EntityMonoBehaviour<TEntity, TMono> : MonoBehaviour, IEntityMonoBehaviour
    where TEntity : IEntity
    where TMono : IEntityMonoBehaviour
{
    [SerializeField] private Vector3 _messageDamping;
    [SerializeField] private Vector3 _offsetInHand;
    [SerializeField] private Quaternion _rotateInHand;

    private GameObject _entityPlaceholder;
    private BackAndForthAnimation _backAndForthAnimation;
    private bool _isDropped = true;

    private Transform _baseParent;

    public TEntity Entity { get; private set; }
    public bool IsVisiblePlaceholder { get; private set; }
    IEntity IEntityMonoBehaviour.Entity => Entity;

    public GameObject GameObject => gameObject;

    public Transform BaseParent => _baseParent;

    private void Start()
    {
        ProtectedStart();
    }

    protected virtual void ProtectedStart()
    {
        Entity = CreateEntity();

        _entityPlaceholder = Instantiate(Player.Instance.Controller.EntityPlaceholder, transform.position + _messageDamping, Quaternion.identity);
        _entityPlaceholder.GetComponent<Canvas>().worldCamera = Camera.main;
        _entityPlaceholder.transform.SetParent(transform);
        _entityPlaceholder.SetActive(false);

        _backAndForthAnimation = _entityPlaceholder.GetComponent<BackAndForthAnimation>();
        ActivationVariables txt = _entityPlaceholder.GetComponent<ActivationVariables>();
        txt.Text.text = string.IsNullOrEmpty(Entity.ActionMessage) ?
            $"{Entity.Title}" : $"{Entity.ActionMessage}";

        _baseParent = transform.parent;
    }

    protected abstract TEntity CreateEntity();

    private void Update()
    {
        ProtectedUpdate();
    }

    protected virtual void ProtectedUpdate()
    { }

    public bool CanTargetEvent()
    {
        if (Player.Instance.IsUserInputEnable() == false)
            return false;

        bool eventEnable = false;
        if (Entity.IsItem)
        {
            if (_isDropped)
                eventEnable = true;
        }
        else
        {
            eventEnable = CanUseAsObject();
        }
        return eventEnable;
    }

    protected bool CanUseAsObject()
    {
        return Entity.IsItem == false && Entity.CanUse(Player.Instance.HandEntity);
    }

    public void HiddenPlaceholder()
    {
        IsVisiblePlaceholder = false;
        if (_entityPlaceholder != null)
            _entityPlaceholder.SetActive(false);
    }

    public void VisiblePlaceholder()
    {
        if (CanTargetEvent() is false)
            return;

        IsVisiblePlaceholder = true;
        _entityPlaceholder.SetActive(true);
        _backAndForthAnimation.Restart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerArea")
        {
            TriggerArea triggerArea = other.gameObject.GetComponent<TriggerArea>();
            triggerArea.events.Add(this);
            triggerArea.Toggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TriggerArea")
        {
            TriggerArea triggerArea = other.gameObject.GetComponent<TriggerArea>();
            triggerArea.events.Remove(this);
            triggerArea.Toggle();
        }
    }

    public void ToHandPosition()
    {
        transform.SetLocalPositionAndRotation(_offsetInHand, _rotateInHand);
    }

    public bool IsDestroyed()
    {
        try
        {
            return GameObject == null;
        }
        catch
        {
            return true;
        }
    }
}
