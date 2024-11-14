using UnityEngine;

public class gameLogic : MonoBehaviour
{
    private int currentState;

    void Start()
    {
        currentState = 0;  // Начинаем с хода игрока
        StartPlayerTurn();
    }

    void Update()
    {
        if (currentState == 0)
        {
            // Проверяем завершение хода игрока
            if (Input.GetKeyDown(KeyCode.Space)) // Например, пробел завершает ход
            {
                EndPlayerTurn();
            }
        }
        else if (currentState == TurnState.EnemyTurn)
        {
            // Пример простого AI, выполняющего ход
            Debug.Log("Enemy is taking their turn...");
            EndEnemyTurn();
        }
        else if (currentState == TurnState.Result)
        {
            // Обрабатываем результат после каждого цикла ходов
            CheckGameResult();
        }
    }

    private void StartPlayerTurn()
    {
        Debug.Log("Player's Turn");
        currentState = TurnState.PlayerTurn;
        // Активируйте необходимые элементы управления для игрока
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
        // Вы можете добавить паузу или анимацию перед ходом противника
    }

    private void EndEnemyTurn()
    {
        Debug.Log("Enemy's Turn Ended");
        currentState = TurnState.Result;
    }

    private void CheckGameResult()
    {
        // Проверьте условие победы или поражения
        Debug.Log("Checking Game Result...");

        // Если игра продолжается:
        currentState = TurnState.PlayerTurn;
        StartPlayerTurn();

        // Если игра окончена, можно перезапустить или завершить:
        // Debug.Log("Game Over");
    }
}
