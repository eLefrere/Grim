using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    // SETTING VARIABLES HERE

    [SerializeField] private MovementType movementType = MovementType.Walking;
    public static MovementType _movementType => Instance.movementType;


    [SerializeField] private AIType aIType = AIType.Walking;
    public static AIType AItype => Instance.aIType;

    //


    public static GameSettings Instance { get; private set; }

    public delegate void SettingChangeDelegate();
    public event SettingChangeDelegate onSettingChange;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void OnEnable()
    {
        //GameSettings.Instance.onSettingChange += SaveSettings;
    }

    private void OnDisable()
    {
        //GameSettings.Instance.onSettingChange -= SaveSettings;
    }

    private void Start()
    {
        //LoadSettings();
    }

    void LoadSettings()
    {
        aIType = (AIType)PlayerPrefs.GetInt("AIType");
        movementType = (MovementType)PlayerPrefs.GetInt("MovementType");
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt("AIType", (int)aIType);
        PlayerPrefs.SetInt("MovementType", (int)movementType);
    }
}
