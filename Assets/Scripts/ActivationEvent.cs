using System;

using StarterAssets;

using UnityEngine;

public class ActivationEvent : MonoBehaviour
{
    [SerializeField] private GameObject _messagePrefab;
    [SerializeField] private Vector3 _messageDamping;
    [SerializeField] private bool _isActive = false;

    [SerializeField] private bool _fixPos = true;

    private static readonly float _cooldown = 0.1f;
    private static DateTime _lastUsed;

    private GameObject _messageObject;
    private BackAndForthAnimation _backAndForthAnimation;

    protected ThirdPersonController Person => ThirdPersonController.Instance;

    public virtual bool TriggerEnable => true;
    public virtual string Message => "";

    private void Start()
    {
        ProtectedStart();
    }

    protected virtual void ProtectedStart()
    {
        _messageObject = Instantiate(_messagePrefab, transform.position + _messageDamping, Quaternion.identity, transform);
        _messageObject.GetComponent<Canvas>().worldCamera = Camera.main;
        _messageObject.SetActive(false);

        _backAndForthAnimation = _messageObject.GetComponent<BackAndForthAnimation>();
        ActivationVariables txt = _messageObject.GetComponent<ActivationVariables>();
        txt.Text.text = $"{Message}";
    }

    private void Update()
    {
        ProtectedUpdate();
    }

    protected virtual void ProtectedUpdate()
    {
        if (TriggerEnable is false)
            return;

        if (Person.playerInput.enabled == false)
            return;

        if ((DayAndNightControl.Now - _lastUsed).TotalSeconds < _cooldown)
            return;

        if (_isActive)
        {
            if (_fixPos == true)
                _messageObject.transform.SetPositionAndRotation(transform.position + _messageDamping, Quaternion.Inverse(transform.rotation));

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnActive();
            }
        }
    }

    protected virtual void OnActive()
    {
        _lastUsed = DayAndNightControl.Now;
        if (TriggerEnable is false)
            NotActive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerArea" && Person.playerInput.enabled == true)
        {
            TriggerArea triggerArea = other.gameObject.GetComponent<TriggerArea>();
            triggerArea.AddActivationEvent(this);
            triggerArea.Toggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TriggerArea" && Person.playerInput.enabled == true)
        {
            TriggerArea triggerArea = other.gameObject.GetComponent<TriggerArea>();
            triggerArea.RemoveActivationEvent(this);
            triggerArea.Toggle();
        }
    }

    public void NotActive()
    {
        _isActive = false;

        _messageObject.SetActive(false);
    }

    public void Active()
    {
        if (TriggerEnable is false)
            return;

        _isActive = true;
        _messageObject.SetActive(true);
        _backAndForthAnimation.Restart();
    }


}
