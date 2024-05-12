using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountUI : MonoBehaviour
{
    private TextMeshProUGUI Tx;
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        Tx = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(ThirdPersonController.Instance != null && ThirdPersonController.Instance.InHandObject != null)
        //{
        //    //TODO
        //    //if(ThirdPersonController.Instance.InHandObject.Counter != null)
        //    //{
        //    //    Tx.text = ThirdPersonController.Instance.InHandObject.Counter.ToString();
        //    //    Image.enabled = true;
        //    //    return;
        //    //}
        //}

        Tx.text = "";

        Image.enabled = false;
    }
}
