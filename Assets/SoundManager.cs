using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] Slider _volume;
    [SerializeField]  public AudioSource _coinTheme;
    [SerializeField]  public AudioSource _mainTheme;
    [SerializeField]  public AudioSource _damageTheme;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    private void Update() {
        Debug.Log(_coinTheme.volume);
        Debug.Log(_volume.value);
    }


    public void ChangeVolume()
    {
        AudioListener.volume = _volume.value;
        Save();
    }

    private void Load()
    {
        _volume.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", _volume.value);
    }
}
