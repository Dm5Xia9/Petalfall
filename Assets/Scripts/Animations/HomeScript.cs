using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour
{
    [SerializeField] private GameObject _facade;
    [SerializeField] private GameObject _original;
    [SerializeField] private ThirdPersonController _persion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerArmature" && _persion._camZoom)
        {
            _original.SetActive(false);
            _facade.SetActive(true);
            _persion.originalZoom = 4;
            _persion.maxZoomOut = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerArmature" && _persion._camZoom)
        {
            _original.SetActive(true);
            _facade.SetActive(false);
            _persion.originalZoom = 7;
            _persion.maxZoomOut = 9;
        }
    }

}
