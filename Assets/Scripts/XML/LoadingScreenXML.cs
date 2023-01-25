using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName = "LoadingScreen")]
public class LoadingScreenXML
{
    [XmlArray("images")]
    [XmlArrayItem("image", Type = typeof(ImageXML))]
    public ImageXML [] imageArray;
}

[XmlRoot(ElementName = "image")]
public class ImageXML
{
    [XmlElement(ElementName = "path")]
    public string path;

    [XmlElement(ElementName = "text")]
    public string text;
}

