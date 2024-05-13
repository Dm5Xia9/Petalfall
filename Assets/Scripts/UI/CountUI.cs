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
        if (Player.Instance != null && Player.Instance.HandIsEmpty() == false && Player.Instance.HandEntity is ICountableEntity countableEntity)
        {
            Tx.text = countableEntity.Count.ToString();
            Image.enabled = true;
            return;
        }

        Tx.text = "";

        Image.enabled = false;
    }
}
