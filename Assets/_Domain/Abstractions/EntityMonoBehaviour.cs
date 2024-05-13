using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    private ActivationVariables _activationVariables;
    public TEntity Entity { get; private set; }
    public bool IsVisablePlaceholder { get; private set; }
    IEntity IEntityMonoBehaviour.Entity => Entity;

    public GameObject GameObject => gameObject;

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
        _activationVariables = _entityPlaceholder.GetComponent<ActivationVariables>();
  
    }
    protected abstract TEntity CreateEntity();

    private void Update()
    {
        ProtectedUpdate();
    }

    protected virtual void ProtectedUpdate()
    {
        if (string.IsNullOrEmpty(Entity.ActionMessage))
        {
            _activationVariables.Text.text = $"{Entity.Title}";
        }
        else
        {
            _activationVariables.Text.text = $"{Entity.ActionMessage}";
        }
    }

    public bool CanTargetEvent()
    {
        if (Player.Instance.IsUserInputEnable() == false)
            return false;

        bool eventEnable = false;
        if (Entity.IsItem)
        {
            if (_isDropped)
            {
                eventEnable = true;
            }
        }
        else
        {
            eventEnable = CanUseAsObject();
        }
        return eventEnable;
    }

    /// <summary>
    /// Проверяем, можно ли использовать сущность как объект
    /// </summary>
    /// <returns></returns>
    protected bool CanUseAsObject()
    {
        return Entity.IsItem == false && Entity.CanUse(Player.Instance.HandEntity);
    }

    public void HiddenPlaceholder()
    {
        IsVisablePlaceholder = false;
        if(_entityPlaceholder != null)
        {
            _entityPlaceholder.SetActive(false);
        }
    }

    public void VisablePlaceholder()
    {
        if (CanTargetEvent() is false)
            return;

        IsVisablePlaceholder = true;
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
