using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirePuzzle : MonoBehaviour
{
    public static FirePuzzle Instance;

    public static Action Reset;
    public static Action NextHalf;

    [SerializeField] public GameObject Fire;
    [SerializeField] public Material FireMaterial;
    [SerializeField] public Material CompletedMaterial;

    public bool puzzleActive = true;
    public bool fireActive = false;
    public bool firstHalf = true;

    private GameObject[] FireBlocks = new GameObject[19];

    public static List<int> firstLine = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    public static List<int> secondLine = new List<int> { 0, 1, 2, 3, 4, 5, 6, 12, 13, 14, 15, 16, 17, 18 };

    private Stack<int> currentLine;

    // Start is called before the first frame update
    void Start()
    {
        FirePuzzle.Instance = this;

        firstLine.Reverse();
        secondLine.Reverse();

        Reset += ResetPuzzle;
        NextHalf += SecondHalf;

        currentLine = new Stack<int>(firstLine);

        for(int i = 0; i < FireBlocks.Length; i++)
        {
            FireBlocks[i] = Fire.transform.GetChild(i).GameObject();
        }
    }

    public void ActivatePiece(int num)
    {
        if (num != currentLine.Pop())
        {
            FirePuzzle.Reset();
        }

        if (currentLine.Count == 0) Halfway.Ready();
    }

    public void SecondHalf()
    {
        if (currentLine.Count == 0 && firstHalf)
        {
            currentLine = new Stack<int>(secondLine);
        }
    }

    private void ResetPuzzle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
