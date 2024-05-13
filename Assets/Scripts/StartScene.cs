using Cinemachine;
using DialogueEditor;
using StarterAssets;
using System.Collections;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _personController;
    [SerializeField] private GameObject _act1Point;
    [SerializeField] private Vector2 _act1Move;
    [SerializeField] private GameObject _act1Position;

    [SerializeField] private NPCConversation _act2Dialog;
    [SerializeField] private GameObject _act2CarObject;
    [SerializeField] private GameObject _act2CarPos;
    [SerializeField] private float _act2CarSpeed;

    [SerializeField] private Vector2 _act2Move;
    [SerializeField] private GameObject _act2Pos;


    [SerializeField] private Vector2 _act3Move;
    [SerializeField] private GameObject _act3Pos;
    [SerializeField] private float _act3CamDistance = 15f; // Максимальное отдаление
    [SerializeField] private float _act3CamSpeed = 5f; // Максимальное отдаление

    [SerializeField] private Vector2 _act4Move;
    [SerializeField] private GameObject _act4Pos;

    [SerializeField] private Vector2 _act5Move;
    [SerializeField] private GameObject _act5Pos;
    [SerializeField] private ShovelScript _act5Shovel;

    [SerializeField] private Vector2 _act6Move;
    [SerializeField] private GameObject _act6Pos;

    [SerializeField] private Vector3 _act7Rotation;
    [SerializeField] private GameObject _act7Pos;

    private StarterAssetsInputs _inputs;
    [SerializeField] private int _actNumber = 0;

    [SerializeField] private GameObject _atrtree;
    [SerializeField] private Vector3 _offsetInHand;
    [SerializeField] private Quaternion _rotateInHand;

    public ConversationManager ConversationManager;
    public float FadeDuration = 1f; // Продолжительность анимации фейда в секундах Private CanvasGroup CanvasGroup;
    public CanvasGroup CanvasGroup;
    public CinemachineVirtualCamera Cinemachine;
    private CinemachineFramingTransposer _transposer;
    private GameObject ff;
    public Animator _animator;

    public static bool IsScenePlay = true;

    private void Start()
    {
        _inputs = _personController.GetComponent<StarterAssetsInputs>();
        _transposer = Cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();

    }
    private void Update()
    {
        if (_actNumber == 0 && !_personController.HasMovePoint)
        {
            IsScenePlay = true;
            Player.Instance.UserInputDisable();
            //_personController.StopPerson();
            StartCoroutine(Act2Car());
            _personController.Teleport(_act1Position.transform.position);
            _inputs.MoveInput(_act1Move);
            _personController.MoveToPoint(_act1Point.transform.position, p =>
            {
                return p.z >= 0;
            });
            StartCoroutine(Act1Corr(1f, 0f));
            _actNumber++;
        }
        else if (_actNumber == 1 && !_personController.HasMovePoint)
        {
            _inputs.MoveInput(_act2Move);
            _personController.MoveToPoint(_act2Pos.transform.position, p =>
            {
                return p.x <= 0;
            });

            _actNumber++;
            ////Player.isActiveAndEnabled = false;
            //_personController.StopPerson();
            //ConversationManager.StartConversation(_act2Dialog);
        }
        else if (_actNumber == 3)
        {
            _personController._camZoom = false;
            StartCoroutine(Act3CamDistance());
            _inputs.MoveInput(_act3Move);
            _personController.MoveToPoint(_act3Pos.transform.position, p =>
            {
                Debug.Log(p.x);
                return p.x >= 0;
            });
            _actNumber++;
        }
        else if (_actNumber == 4 && !_personController.HasMovePoint)
        {
            Cinemachine.enabled = false;
            _inputs.MoveInput(_act4Move);
            _personController.MoveToPoint(_act4Pos.transform.position, p =>
            {
                return p.z >= 0;
            });

            _actNumber++;
            ////Player.isActiveAndEnabled = false;
            //_personController.StopPerson();
            //ConversationManager.StartConversation(_act2Dialog);
        }
        else if (_actNumber == 5 && !_personController.HasMovePoint)
        {
            _inputs.MoveInput(_act5Move);
            _personController.MoveToPoint(_act5Pos.transform.position, p =>
            {
                return p.x <= 0;
            });

            _actNumber++;
            ////Player.isActiveAndEnabled = false;
            //_personController.StopPerson();
            //ConversationManager.StartConversation(_act2Dialog);
        }
        else if (_actNumber == 6 && !_personController.HasMovePoint)
        {
            Player.Instance.PickupHandEntity(_act5Shovel.Entity);

            _inputs.MoveInput(-_act5Move);
            _personController.MoveToPoint(_act4Pos.transform.position, p =>
            {
                return p.x >= 0;
            });

            _actNumber++;
        }
        else if (_actNumber == 7 && !_personController.HasMovePoint)
        {
            _inputs.MoveInput(-_act4Move);
            _personController.MoveToPoint(_act3Pos.transform.position, p =>
            {
                return p.z <= 0;
            });

            _actNumber++;
        }
        else if (_actNumber == 8 && !_personController.HasMovePoint)
        {
            _inputs.MoveInput(-_act3Move);
            _personController.MoveToPoint(_act2Pos.transform.position, p =>
            {
                return p.x <= 0;
            });

            _actNumber++;
        }
        else if (_actNumber == 9 && !_personController.HasMovePoint)
        {
            Player.Instance.DropHandEntity();
            _act5Shovel.enabled = false;
            _inputs.MoveInput(_act6Move);
            ff = Instantiate(_atrtree);
            ff.transform.SetParent(_personController.Hand.transform, false);
            ff.transform.SetLocalPositionAndRotation(_offsetInHand, _rotateInHand);
            //-0.608 -0.097 -0.327 -13.544 -33.74 -142
            _personController.MoveToPoint(_act6Pos.transform.position, p =>
            {
                Debug.Log(p.x);
                return p.x >= 0;
            });

            _actNumber++;
        }
        else if (_actNumber == 10 && !_personController.HasMovePoint)
        {
            ff.SetActive(false);
            _personController._camZoom = true;
            Cinemachine.enabled = true;
            _personController.Teleport(_act7Pos.transform.position);
            _act1Position.transform.Rotate(_act7Rotation);
            StartCoroutine(Act7Povorot());
            _act5Shovel.enabled = true;
            _act5Shovel.transform.position = _act7Pos.transform.position;
            Player.Instance.UserInputEnable();

            _actNumber++;
        }
        else if (_actNumber == 12)
        {
            _animator.SetBool(Animator.StringToHash("StartAnim"), true);
            IsScenePlay = false;
        }
    }

    private void Awake()
    {

        ConversationManager.OnConversationEnded += () =>
        {
            if (_actNumber == 2)
            {
                _actNumber++;
            }
        };
    }

    private IEnumerator Act7Povorot()
    {
        _inputs.MoveInput(new Vector2(-1, 1));
        yield return new WaitForSeconds(1f);
        _inputs.MoveInput(new Vector2(0, 0));
        _actNumber = 12;

    }

    private IEnumerator Act1Corr(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < FadeDuration)
        {
            CanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / FadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        CanvasGroup.alpha = endAlpha;
    }

    private IEnumerator Act2Car()
    {
        while (_act2CarObject.transform.position.z >= _act2CarPos.transform.position.z)
        {
            _act2CarObject.transform.Translate(new Vector3(0, 0, -Time.deltaTime * _act2CarSpeed));
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        ConversationManager.StartConversation(_act2Dialog);
        //_actNumber = 3;
    }


    private IEnumerator Act3CamDistance()
    {

        while (_transposer.m_CameraDistance < _act3CamDistance)
        {
            _transposer.m_CameraDistance += _act3CamSpeed * Time.deltaTime;
            yield return null;
        }
    }

}
