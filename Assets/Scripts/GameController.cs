using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
}
[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}
public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    private string playerSide;
    private int moveCount;
    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "×";
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        SetPlayerColors(playerX, playerO);
    }
    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Gridspacescript>()
              .SetGameControllerReference(this);
        }
    }
    public string GetPlayerSide()
    {
        return playerSide;
    }
    public void EndTurn()
    {
        moveCount++;

        if ( 
      (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) ||
          (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) ||
          (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        ) { GameOver(playerSide); return; }
        if ( 
      (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) ||
          (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) ||
          (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        ) { GameOver(playerSide); return; }
        if ( 
      (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) ||
          (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        ) { GameOver(playerSide); return; }
        
        if (moveCount == 9)
        { 
            GameOver(null);
        }
        else
        {
            ChangeSides();
        }
    }
    void ChangeSides()
    {
        playerSide = (playerSide == "×") ? "O" : "×";
        if (playerSide == "×")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }
    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }
    void GameOver(string winner)
    {
        SetBoardInteractable(false);
        if (winner == null) SetGameOverText("It's a Draw!");
        else SetGameOverText(winner + " Wins!");
        restartButton.SetActive(true);
    }
    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }
    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    public void RestartGame()
    {
        playerSide = "×";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerColors(playerX, playerO);
        SetBoardInteractable(true);
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }
}