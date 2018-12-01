using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum GameMode { Game, Collection }

    private Vector3 desiredMenuPosition;
    public RectTransform menuContainer;
    public static GameMode currentGameMode = GameMode.Game;

    void Start()
    {

    }

    void Update()
    {
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
    }

    public void OnGoToCollectionClick()
    {
        NavigateTo(GameMode.Collection);
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
        }
    }
}
