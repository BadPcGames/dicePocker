using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject PlayerButton;
    [SerializeField]
    private GameObject ResultMenu;

    public Rigidbody rb;

    private int move;

    private void Start()
    {
        move = -1;
        changeMove();
        ResultMenu.SetActive(false);
    }


    private void SetPlayerTurn(bool firsMove)
    {
        PlayerButton.SetActive(true);
        if (firsMove)
        {
            GameObject[] dice = GameObject.FindGameObjectsWithTag("playerDice");
            foreach (GameObject d in dice)
            {
                d.GetComponent<playerDice>().ChangeForStart();
            }
        }
        player.GetComponent<playerStats>().setCanChange(!firsMove);
    }

    private void SetEnemyTurn(bool firsMove)
    {
        player.GetComponent<playerStats>().setCanChange(false);
        if (firsMove)
        {
            enemy.GetComponent<enemyIntelect>().FirstMove();
        }
        else
        {
            enemy.GetComponent<enemyIntelect>().SecondMove();
        }
    
    }

    public void changeMove()
    {
        move++;
        if(move>3)
        {     
            move= -1;
            ResultMenu.SetActive(true);
            Transform result = ResultMenu.transform.Find("result");
            result.gameObject.GetComponent<Text>().text= findResult();
        }
        else
        {
            if (move == 0)
            {
                ResultMenu.SetActive(false);
                SetPlayerTurn(true);
            }
            else if (move == 1)
            {
                SetEnemyTurn(true);
            }
            else if (move == 2)
            {
                SetPlayerTurn(false);
            }
            else if (move == 3)
            {
                SetEnemyTurn(false);
            }
        }
    }


    private string findResult()
    {
        int enemyHandClass=enemy.GetComponent<handManager>().getHandClass();
        int playerHandClass = player.GetComponent<handManager>().getHandClass();

        if(player.GetComponent<handManager>().getValues()== enemy.GetComponent<handManager>().getValues())
        {
            return "Draw";
        }
        else
        {
            if (playerHandClass > enemyHandClass)
            {
                return "You Win";
            }
            else if (enemyHandClass > playerHandClass)
            {
                return "You Lose";
            }
            else 
            {
                int enemyHand1Combynation= enemy.GetComponent<handManager>().getHand1Combynation();
                int playerHand1Combynation = player.GetComponent<handManager>().getHand1Combynation();

                if (enemyHand1Combynation < playerHand1Combynation)
                {
                    return "You Win";
                }
                else if (enemyHand1Combynation > playerHand1Combynation)
                {
                    return "You Lose";
                }
                else
                {
                    int enemyHand2Combynation = enemy.GetComponent<handManager>().getHand2Combynation();
                    int playerHand2Combynation = player.GetComponent<handManager>().getHand2Combynation();

                    if (enemyHand2Combynation < playerHand2Combynation)
                    {
                        return "You Win";
                    }
                    else if (enemyHand2Combynation > playerHand2Combynation)
                    {
                        return "You Lose";
                    }
                    else
                    {
                        List<int> enemyMax = enemy.GetComponent<handManager>().getMaxDiceWithoutCombination();
                        List<int> playerMax = player.GetComponent<handManager>().getMaxDiceWithoutCombination();

                        for (int i = 0; i < enemyMax.Count; i++)
                        {
                            if (enemyMax[i] < playerMax[i])
                            {
                                return "You Win";
                            }
                            else if (enemyMax[i] > playerMax[i])
                            {
                                return "You Lose";
                            }
                        }
                    }
                }
            }
        }
        return "Draw";
    }


}
