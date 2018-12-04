using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum GameMode { Game, Collection, Shop }

    private Vector3 desiredMenuPosition;
    public RectTransform menuContainer;
    public static GameMode currentGameMode = GameMode.Game;
    private TrzepaczHajsu trzepacz;
    private GameObject goToCollectionButton;

    private double timer;

    void Start()
    {
        this.trzepacz = GameObject.Find("TrzepaczHajsu").GetComponent<TrzepaczHajsu>();
        this.goToCollectionButton = GameObject.Find("GoToCollectionButton");
        timer = 600.0f;
    }

    void Update()
    {
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
        timer -= Time.deltaTime;
    }

    public void OnGoToCollectionClick()
    {
        goToCollectionButton.GetComponent<HighlightableObjectScript>().setHighlightSprite(false);
        NavigateTo(GameMode.Collection);
    }

    public void OnGoToShopClick()
    {
        NavigateTo(GameMode.Shop);
    }
    public void onShowAdButtonClick()
    {
        if (trzepacz.rewarder != null && timer <= 0)
        {
            trzepacz.ShowAd();
            timer = 600.0f;
        }
    }
    public void OnBackClick()
    {
        NavigateTo(GameMode.Game);
    }

    public static bool isGameActive()
    {
        return currentGameMode == GameMode.Game;
    }

    private void NavigateTo(GameMode gameMode)
    {
        currentGameMode = gameMode;
        switch (gameMode)
        {
            case GameMode.Game:
                desiredMenuPosition = Vector3.zero;
                break;
            case GameMode.Collection:
                desiredMenuPosition = Vector3.left * 1920;
                break;
            case GameMode.Shop:
                desiredMenuPosition = Vector3.right * 1920;
                break;
        }
    }
}
