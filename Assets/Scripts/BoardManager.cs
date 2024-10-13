using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    public List<Transform> boardSpaces; // Assign via Inspector
    public GameObject pawn; // Assign the pawn GameObject
    private int currentSpace = 0;

    void Start()
    {
        if (boardSpaces.Count == 0)
        {
            Debug.LogError("Board spaces not assigned in BoardManager.");
        }

        pawn.transform.position = boardSpaces[currentSpace].position;
    }

    public void MovePawn(int steps)
    {
        StartCoroutine(MovePawnCoroutine(steps));
    }

    IEnumerator MovePawnCoroutine(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentSpace = (currentSpace + 1) % boardSpaces.Count;
            Vector3 targetPos = boardSpaces[currentSpace].position;

            float elapsedTime = 0f;
            float duration = 0.5f;
            Vector3 startPos = pawn.transform.position;

            while (elapsedTime < duration)
            {
                pawn.transform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            pawn.transform.position = targetPos;
            yield return new WaitForSeconds(0.2f);
        }

        // Additional game logic here
    }
}
