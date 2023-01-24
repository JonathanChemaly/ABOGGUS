using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuXMLManager : MonoBehaviour
{
    [SerializeField] private string XMLpath;
    [SerializeField] private float timeBetweenPictures;

    [SerializeField] private Image pictureHolder;
    [SerializeField] private TextMeshProUGUI textHolder;
    private int pictureNum = 0;
    private PauseMenuXML pmXML;

    // Start is called before the first frame update
    void Start()
    {
        //creates an xml object that we can a use base on the xml path
        pmXML = XMLUtil.ImportXml<PauseMenuXML>(XMLpath);
        Debug.Log("length of xml: " + pmXML.imageArray.Length);
        StartCoroutine(LoadPhoto());
    }

    /**
     * Coroutine to load photo after timeBetweenPicture secs
     */
    IEnumerator LoadPhoto()
    {
        //Sets the two fields to the sprite determined by the xml and the
        Debug.Log(pmXML.imageArray[pictureNum].path);
        pictureHolder.sprite = Resources.Load<Sprite>(pmXML.imageArray[pictureNum].path);
        textHolder.text = pmXML.imageArray[pictureNum].text;

        //Resets number back to the start of the list
        pictureNum++;
        if(pictureNum >= pmXML.imageArray.Length)
        {
            pictureNum = 0;
        }

        //waits deteremined amount of seconds before loading the next photo.
        yield return new WaitForSeconds(timeBetweenPictures);
        StartCoroutine(LoadPhoto());
    }
}
