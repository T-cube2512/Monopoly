using UnityEngine;

public class GameController : MonoBehaviour
{
    public DiceController dice;
    public BoardManager boardManager;

    void Start()
    {
        dice.OnDiceRollComplete += OnDiceRollComplete;
    }

    void OnDestroy()
    {
        dice.OnDiceRollComplete -= OnDiceRollComplete;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!dice.IsRolling)
            {
                dice.ThrowDice();
            }
        }
    }

    void OnDiceRollComplete(int value)
    {
        boardManager.MovePawn(value);
    }
}
