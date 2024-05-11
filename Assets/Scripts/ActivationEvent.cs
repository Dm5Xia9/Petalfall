using StarterAssets;
using TMPro;
using UnityEngine;

public class ActivationEvent : MonoBehaviour
{
    public GameObject MessagePrefab;
    public Vector3 MessageDamping;
    public float Radius;
    public float Distance;
    private GameObject MessageObject;
    private BackAndForthAnimation BackAndForthAnimation;
    public bool IsActive = false;
    public virtual bool TriggerEnable => true;
    protected ThirdPersonController _person => ThirdPersonController.Instance;
    public virtual string Message => "";

    private void Start()
    {
        ProtectedStart();
    }

    protected virtual void ProtectedStart()
    {
        MessageObject = Instantiate(MessagePrefab, transform.position + MessageDamping, Quaternion.identity);
        MessageObject.GetComponent<Canvas>().worldCamera = Camera.main;
        MessageObject.transform.SetParent(transform);
        MessageObject.SetActive(false);
        BackAndForthAnimation = MessageObject.GetComponent<BackAndForthAnimation>();
        var txt = MessageObject.GetComponent<ActivationVariables>();
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

        if (_person.playerInput.enabled == false)
            return;

        if (IsActive)
        {
            // ���������, ���� �� ������ �� ������� "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnActive();
            }
        }
    }

    protected virtual void OnActive()
    {
        //DayAndNightControl.Instance.SkipMinutes(15);
        //Debug.Log("����� �����");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TriggerArea" && _person.playerInput.enabled == true)
        {
            TriggerArea triggerArea = other.gameObject.GetComponent<TriggerArea>();
            triggerArea.events.Add(this);
            triggerArea.Toggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TriggerArea" && _person.playerInput.enabled == true)
        {
            TriggerArea triggerArea = other.gameObject.GetComponent<TriggerArea>();
            triggerArea.events.Remove(this);
            triggerArea.Toggle();
        }
    }

    public void NotActive()
    {
        IsActive = false;

        MessageObject.SetActive(false);
    }

    public void Active()
    {
        if (TriggerEnable is false)
            return;

        IsActive = true;
        MessageObject.SetActive(true);
        BackAndForthAnimation.Restart();
    }


}
