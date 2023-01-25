using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

public static class XMLUtil
{
    public static T ImportXml<T>(string path)
    {
        try
        {
            XmlSerializer serializer = new(typeof(T));
            using var stream = new FileStream(path, FileMode.Open);
            Debug.Log("Successfully imported: " + path);
            return (T)serializer.Deserialize(stream);
        }
        catch (Exception e)
        {
            Debug.LogError("Exception importing xml file: " + e);
            return default;
        }
    }
}
