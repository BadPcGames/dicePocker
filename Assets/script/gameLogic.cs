using UnityEngine;

public class gameLogic : MonoBehaviour
{
    private int currentState;

    void Start()
    {
        currentState = 0;  // �������� � ���� ������
        StartPlayerTurn();
    }

    void Update()
    {
        if (currentState == 0)
        {
            // ��������� ���������� ���� ������
            if (Input.GetKeyDown(KeyCode.Space)) // ��������, ������ ��������� ���
            {
                EndPlayerTurn();
            }
        }
        else if (currentState == TurnState.EnemyTurn)
        {
            // ������ �������� AI, ������������ ���
            Debug.Log("Enemy is taking their turn...");
            EndEnemyTurn();
        }
        else if (currentState == TurnState.Result)
        {
            // ������������ ��������� ����� ������� ����� �����
            CheckGameResult();
        }
    }

    private void StartPlayerTurn()
    {
        Debug.Log("Player's Turn");
        currentState = TurnState.PlayerTurn;
        // ����������� ����������� �������� ���������� ��� ������
    }

    private void EndPlayerTurn()
    {
        Debug.Log("Player's Turn Ended");
        currentState = TurnState.EnemyTurn;
        StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        Debug.Log("Enemy's Turn");
        // �� ������ �������� ����� ��� �������� ����� ����� ����������
    }

    private void EndEnemyTurn()
    {
        Debug.Log("Enemy's Turn Ended");
        currentState = TurnState.Result;
    }

    private void CheckGameResult()
    {
        // ��������� ������� ������ ��� ���������
        Debug.Log("Checking Game Result...");

        // ���� ���� ������������:
        currentState = TurnState.PlayerTurn;
        StartPlayerTurn();

        // ���� ���� ��������, ����� ������������� ��� ���������:
        // Debug.Log("Game Over");
    }
}
