using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    GameObject _changeUI;
    [SerializeField] TMP_Text _collectiable;
     [SerializeField] TMP_Text _health;
     [SerializeField] GameObject _restart;
     [SerializeField]public TMP_Text _Highscore;
     [SerializeField] TMP_Text _score;

     [Header("Settings Panel")]
     [SerializeField] GameObject SettingsCanva;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject SettingsUIPanel;
    [SerializeField] GameObject SettingsOpenButton;
    // [SerializeField] GameObject SettingsCloseButton;
    // public event System.Action IsPaused;
    private bool _isPausedBool = false;
    public bool IsPausedBool => _isPausedBool;

    private void Awake() {

         _changeUI = GameObject.FindGameObjectWithTag("Player");

        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start() {
        SettingsOpenButton.SetActive(true);
        _Highscore.text =  (PlayerPrefs.GetInt("Highestcollected", 0)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnEnable() 
    {
        _changeUI.GetComponent<PlayerScript>().IsCollected += IncreaseCollectiable;
        _changeUI.GetComponent<PlayerScript>().IsHitted += DecreaseHealth;
        _changeUI.GetComponent<PlayerScript>().IsDead += SetRestart;

        
    }

    

    private void OnDisable() 
    {
        if(_changeUI == null) return;
        
        _changeUI.GetComponent<PlayerScript>().IsCollected -= IncreaseCollectiable;
        _changeUI.GetComponent<PlayerScript>().IsHitted -= DecreaseHealth;
        _changeUI.GetComponent<PlayerScript>().IsDead -= SetRestart;

    }

    private void IncreaseCollectiable(int collected)
    {
        
        _collectiable.text  = collected.ToString();
    }
    private void DecreaseHealth(int health)
    {
        _health.text = health.ToString();
    }

    private void SetRestart()
    {
       _restart.gameObject.SetActive(true);
       _score.text = _collectiable.text;
       SoundManager.Instance._mainTheme.Stop();
       
    }

    public void OpenSettingsPanel()
    {
        SettingsUIPanel.transform.localScale = Vector3.zero;
        Image panelImg = SettingsPanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(118, 41, 154, 94), 0.2f);
        SettingsUIPanel.transform.DOScale(new Vector3(0.47f, 0.47f,0.47f), 0.2f).OnComplete(() =>
    {
        
        StopGame();
    });
    }

    

    public void CloseSettingsPanel()
    {

        // 
            
        Image panelImg = SettingsPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        SettingsUIPanel.transform.DOScale(0f, 0.4f);
        ReturnGame();
        
        
    }

    private void StopGame()
    {
        SettingsPanel.SetActive(true);
        SettingsCanva.SetActive(true);
        SettingsUIPanel.SetActive(true);
        SettingsOpenButton.SetActive(false);
        // SettingsCloseButton.SetActive(true);
       Time.timeScale = 0f;
       _isPausedBool = true;


    }

    private void ReturnGame()
    {
            SettingsOpenButton.SetActive(true);
            SettingsCanva.SetActive(false);
        SettingsPanel.SetActive(false);
        SettingsUIPanel.SetActive(false);
        // SettingsCloseButton.SetActive(false);
        Time.timeScale = 1f;
        _isPausedBool = false;
    }

    public void RestartGame()
    {

        GameManager.Instance.LoadScene("Main");
        Time.timeScale = 1f;
        // if(SoundManager.Instance._mainTheme != null)
            // SoundManager.Instance._mainTheme.Play();
    }
    public void QuitToMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    

}