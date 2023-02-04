using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSnapToLeftOf : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("What you want this text object to snap to.")]
    private TextMeshProUGUI whatToSnapTo;
    [SerializeField]
    [Tooltip("How much you want you want this to be the left of whatToSnapTo by")]
    private float offset = 10;

    // Start is called before the first frame update
    void Start()
    {
        //Get rectangles of our text object and the serialize
        RectTransform snapRectangle = whatToSnapTo.rectTransform;
        RectTransform thisRectTransform = gameObject.GetComponent<RectTransform>();

        Debug.Log(whatToSnapTo.preferredWidth);
        /*
         * we take half the width and add the offset then negate itto put our gameobject 
         * at the offset to the left of the serizable field.
         */
        float x = -(whatToSnapTo.preferredWidth/2 + offset); 
        //we what our new object at sime y postion as our field
        float y = snapRectangle.anchoredPosition.y;
        //set our game objects transform to our new values
        thisRectTransform.anchoredPosition = new Vector2(x,y);
    }

}
