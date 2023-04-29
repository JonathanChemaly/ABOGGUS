using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenXMLManager : MonoBehaviour
{
    [SerializeField] private string XMLpath;
    [SerializeField] private float timeBetweenPictures;

    [SerializeField] private Image pictureHolder;
    [SerializeField] private TextMeshProUGUI textHolder;
    private int pictureNum;
    private LoadingScreenXML lsXML;

    // Start is called before the first frame update
    void Start()
    {
        //creates an xml object that we can a use base on the xml path
        lsXML = XMLUtil.ImportXml<LoadingScreenXML>(XMLpath);
        //Debug.Log("length of xml: " + lsXML.imageArray.Length);

        //Set starting picture number to a random number for randomness of how we cycle through loading screen
        pictureNum = Random.Range(0, lsXML.imageArray.Length);
        StartCoroutine(LoadPhoto());
    }

    /**
     * Coroutine to load photo after timeBetweenPicture secs
     */
    IEnumerator LoadPhoto()
    {
        //Sets the two fields to the sprite determined by the xml and the
        pictureHolder.sprite = Resources.Load<Sprite>(lsXML.imageArray[pictureNum].path);
        textHolder.text = lsXML.imageArray[pictureNum].text;

        pictureHolder.color = new Color(1, 1, 1, 0); //set picture color to zero so it can fade in
        StartCoroutine(FadeInPhoto());
        //Waits for fade in to finish
        yield return new WaitForSeconds(timeBetweenPictures);
        //Resets number back to the start of the list
        pictureNum++;
        if(pictureNum >= lsXML.imageArray.Length)
        {
            pictureNum = 0;
        }


        
        StartCoroutine(FadeOutPhoto());
        //Waits for fade out to finish
        yield return new WaitForSeconds(timeBetweenPictures);
        //takes half the time necessary to fade photo out
        StartCoroutine(LoadPhoto());
    }

    //Fades in a photo by changing the color of the active picture going down from taking
    //half the time between photos
    IEnumerator FadeOutPhoto()
    {
        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            pictureHolder.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    //Fades out photo by changing the color of the active picture going down from taking
    //half the time between photos
    IEnumerator FadeInPhoto()
    {
        
        for (float i = 0; i <= 0.5f; i += Time.deltaTime)
        {
            pictureHolder.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }


}
