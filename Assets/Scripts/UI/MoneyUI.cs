using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    private TextMeshProUGUI Tx;

    // Start is called before the first frame update
    void Start()
    {
        Tx = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Tx.text = Player.Instance.Balance.ToString();
    }
}
