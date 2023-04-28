using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirePuzzle : MonoBehaviour
{
    public static FirePuzzle Instance;

    public static Action Reset;
    public static Action NextHalf;

    [SerializeField] public GameObject Fire;
    [SerializeField] public Material FireMaterial;
    [SerializeField] public Material CompletedMaterial;
    [SerializeField] GameObject player;
    [SerializeField] public GameObject blackout;
    [SerializeField] public GameObject manaDrop;

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
        manaDrop.SetActive(false);

        for(int i = 0; i < FireBlocks.Length; i++)
        {
            FireBlocks[i] = Fire.transform.GetChild(i).GameObject();
        }

        if (GameConstants.puzzleStatus["FirePuzzle"])
        {
            Destroy(manaDrop.transform.parent.gameObject);
        }
    }

    private void Update()
    {
        if (!firstHalf && currentLine.Count == 0)
        {
            manaDrop.SetActive(true);
            GameConstants.puzzleStatus["FirePuzzle"] = true;
        }
    }

    public void ActivatePiece(int num)
    {
        if (num != currentLine.Pop())
        {
            FirePuzzle.Reset();
        }

        if (currentLine.Count == 0 && firstHalf) Halfway.Ready();
    }

    public void SecondHalf()
    {
        if (currentLine.Count == 0 && firstHalf)
        {
            currentLine = new Stack<int>(secondLine);
            firstHalf = false;
        }
    }

    private void ResetPuzzle()
    {
        StartCoroutine(this.ResetRoutine());
    }

    public IEnumerator ResetRoutine()
    {

        StartCoroutine(this.BlackOut());
        yield return new WaitForSecondsRealtime(1f);

        currentLine = new Stack<int>(firstLine);
        player.transform.position = new Vector3(-15, 4, 0) + this.transform.position;

        StartCoroutine(this.BlackOut(false));
    }

    //blackout animation taken from https://turbofuture.com/graphic-design-video/How-to-Fade-to-Black-in-Unity
    public IEnumerator BlackOut(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        UnityEngine.Color objectColor = blackout.GetComponent<RawImage>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackout.GetComponent<RawImage>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new UnityEngine.Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackout.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackout.GetComponent<RawImage>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new UnityEngine.Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackout.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }
        }
    }
}
