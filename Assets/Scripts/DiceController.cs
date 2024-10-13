using UnityEngine;
using System.Collections;
using SysRandom = System.Random;
using System;
public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isRolling = false;
    private int diceValue = 1;

    public float throwForce = 5f;
    public float throwTorque = 10f;

    public event Action<int> OnDiceRollComplete;

    public bool IsRolling => isRolling;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ThrowDice()
    {
        if (isRolling) return;

        isRolling = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = new Vector3(
            transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f),
            transform.position.y,
            transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f)
        );

        rb.AddForce(new Vector3(
            UnityEngine.Random.Range(-throwForce, throwForce),
            throwForce,
            UnityEngine.Random.Range(-throwForce, throwForce)
        ), ForceMode.Impulse);

        rb.AddTorque(new Vector3(
            UnityEngine.Random.Range(-throwTorque, throwTorque),
            UnityEngine.Random.Range(-throwTorque, throwTorque),
            UnityEngine.Random.Range(-throwTorque, throwTorque)
        ), ForceMode.Impulse);

        StartCoroutine(DetermineDiceValue());
    }

    IEnumerator DetermineDiceValue()
    {
        yield return new WaitUntil(() => rb.IsSleeping());

        diceValue = GetTopFaceValue();

        Debug.Log("Dice Value: " + diceValue);

        isRolling = false;

        OnDiceRollComplete?.Invoke(diceValue);
    }

    int GetTopFaceValue()
    {
        Vector3 up = transform.up;
        float maxDot = -Mathf.Infinity;
        int topFace = 1;

        Vector3[] faceNormals = new Vector3[]
        {
            Vector3.up,      // Face 1
            Vector3.down,    // Face 2
            Vector3.forward, // Face 3
            Vector3.back,    // Face 4
            Vector3.left,    // Face 5
            Vector3.right    // Face 6
        };

        for (int i = 0; i < faceNormals.Length; i++)
        {
            float dot = Vector3.Dot(up, faceNormals[i]);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFace = i + 1;
            }
        }

        return topFace;
    }

    public int GetDiceValue()
    {
        return diceValue;
    }
}
