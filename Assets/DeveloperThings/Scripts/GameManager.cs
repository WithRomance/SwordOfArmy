using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class GameManager : UnitySingleton<GameManager>
{
    public enum Winner { Player, Enemy }
    public static GameData gameData;
    public static event Action SetGameData;
    public static event Action LoadGameData;
    public Winner gameWinner;
    private float playerMoney = 0;
    private int playerLevel = 0;
    private int currentScene;
    private int playerGoblet = 0;
    private bool isGameOver = false;
    private bool isGameGoing = true;
    [SerializeField] private int soldierEquipped = 0;
    private int mergedEquipment = 0;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text gobletText;
    [SerializeField] private TMP_Text winPanelGobletText;
    [SerializeField] private Transform moneyIconTransform;
    [SerializeField] private Transform gobletIconTransform;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject playerPathObject;
    [SerializeField] private GameObject enemyPathObject;
    [SerializeField] private GameObject playerFortObject;
    [SerializeField] private GameObject enemyFortObject;
    private void OnEnable()
    {
        LoadGameData += LoadGameDatas;
        SetGameData += SetGameDatas;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        gameData = new GameData();
        //gameData = SaveSystem.Load(gameData);
        LoadGameData?.Invoke();
        // SceneManager.LoadScene(currentScene);
        isGameOver = false;
        isGameGoing = true;
    }

    private void SetGameDatas()
    {
        gameData.playerMoney = playerMoney;
        gameData.playerLevel = playerLevel;
        gameData.playerGoblet = playerGoblet;
        gameData.currentScene = currentScene;
    }
    private void LoadGameDatas()
    {
        playerMoney = gameData.playerMoney;
        playerLevel = gameData.playerLevel;
        playerGoblet = gameData.playerGoblet;
        currentScene = gameData.currentScene;
    }

    private void Update()
    {
        SetGameData?.Invoke();
        SaveSystem.Save(gameData);
        moneyText.text = Mathf.RoundToInt(playerMoney).ToString();
        gobletText.text = playerGoblet.ToString();
        currentScene = SceneManager.GetActiveScene().buildIndex;

    }


    public Transform GetMoneyIconTransform() => moneyIconTransform;
    public Transform GetGobletIconTransform() => gobletIconTransform;
    public bool IsGameOver() => isGameOver;
    public bool IsGameGoing() => isGameGoing;
    public Winner GetGameWinner() => gameWinner;
    public void SetGameState(bool value) => isGameGoing = value;


    public void FinishGame()
    {
        isGameOver = true;
        if (gameWinner == Winner.Player)
        {
            winPanel.SetActive(true);
            playerGoblet += 90;
            winPanelGobletText.text = 90.ToString();
            WinRewardManager.Instance.StartRewardingGoblet(90);

        }
        else failPanel.SetActive(true);
    }
    public void IncreaseLevel() => playerLevel++;
    public void SpendMoney(float value) => playerMoney -= value;
    public float GetMoneyValue() => playerMoney;
    public void EarnMoney(float value) => playerMoney += value;
    public int GetPlayerLevel() => playerLevel;
    public void IncreaseMergedEquipment() => mergedEquipment++;
    public void IncreaseEquippedSoldier() => soldierEquipped++;
    public int GetMergedEquipment() => mergedEquipment;
    public int GetEquippedSoldier() => soldierEquipped;
    public GameObject GetPlayerPathObject() => playerPathObject;
    public GameObject GetEnemyPathObject() => enemyPathObject;
    public GameObject GetPlayerFortObject() => playerFortObject;
    public GameObject GetEnemyFortObject() => enemyFortObject;
    public void unlimitedmoney()
    {
        playerMoney = 999999999;
    }
    public void limitedmoney()
    {
        playerMoney = 500;
    }


}
