using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    private TextMeshProUGUI Tx;

    public static MoneyUI Instance { get; private set; }
    public int Money;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Tx = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Tx.text = Money.ToString();
    }
}
