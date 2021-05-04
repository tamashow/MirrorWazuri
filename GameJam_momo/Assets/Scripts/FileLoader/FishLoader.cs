using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class FishLoader 
{
    // Start is called before the first frame update
    List<FishData> tmpFishdata = new List<FishData>();

    const string foldersParentDirectryName = "FishData";
    const string FileNameFishProperty = "property.json";
    const string FileNameFishThumbnail = "thumbnail.png";
    const string FileNameFishAppearanceImage = "appearance.png";

    /*
     FishData------Wakame(どんな名前でもok　半角英字で)---property.json
               |                                    |-thumbnail.png
               |                                    |-appearance.png
               |
               |---Wasabi--略
     */

    /*example   
      property.json

        {
            "width":1.0
            "height":0.8
           "name":"わかめ",
           "score":810,
            "explanation:"説明文"
        }

     */

    public FishLoader()
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void LoadFishData()
    {
        tmpFishdata = ReadFromStreamingAssetts();
    }

    List<FishData> ReadFromStreamingAssetts()
    {
        List<FishData> fishData = new List<FishData>();

        string assetsPath = Application.streamingAssetsPath;

        string gameDataDirectry = assetsPath + "/" + foldersParentDirectryName;

        if (Directory.Exists(gameDataDirectry) == false)
        {
            throw new Exception("FishData does not exist");
        }

    
        string[] itemFolders = System.IO.Directory.GetDirectories(path:  gameDataDirectry);


        for(int i=0; i <itemFolders.Length; i++)
        {
            FishData data;
            string folderName = itemFolders[i];

            Debug.Log(folderName);

            string folderPath = folderName;

            string propertyPath = folderPath + "/" + FileNameFishProperty;
            string thumbnailPath = folderPath + "/" + FileNameFishThumbnail;
            string imagePath = folderPath + "/" + FileNameFishAppearanceImage;


            if (File.Exists(propertyPath) == false)
            {
                throw new Exception("there is no property file {property.json}");
            }
            if (File.Exists(thumbnailPath) == false)
            {
                throw new Exception("there is no thumbnail file {thumbnail.png}");
            }

            if (File.Exists(imagePath) == false)
            {
                throw new Exception("there is no image file {appearance.png}");
            }

            JsonFishProperty fishProperty = ReadPropertyFromFile(propertyPath);
            Texture2D thumbnail = ReadTextureFromFile(thumbnailPath);
            Texture2D image = ReadTextureFromFile(imagePath);

            data.width = fishProperty.width;
            data.height = fishProperty.height;
            data.name = fishProperty.name;
            data.score = fishProperty.score;
            data.thumbnail = thumbnail;
            data.bodyImage = image;
            data.explanation = fishProperty.explanation;
            fishData.Add(data);
        }

        return fishData;
    }

    public FishDataContainer Export()
    {
        return new FishDataContainer(tmpFishdata);
    }

    /*  List<FishData> ReadFromExternalFolder(path)
      {

      }*/
    JsonFishProperty ReadPropertyFromFile(string path )
    {
        // StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Shift_JIS"));
        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
        string json = sr.ReadToEnd();

        JsonFishProperty property = JsonUtility.FromJson<JsonFishProperty>(json);
        if (property == null)
        {
            throw new Exception("property could not be loaded correctly");
        }
        return property;
    }

    Texture2D ReadTextureFromFile(string path)
    {
        /* byte[] bytes = File.ReadAllBytes(path);
         Texture2D texture = new Texture2D(100, 100);
         texture.LoadImage(bytes);
         return texture;*/

        BinaryReader bin = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read));
        byte[] rb = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();
        int pos = 16, width = 0, height = 0;
        for (int i = 0; i < 4; i++) width = width * 256 + rb[pos++];
        for (int i = 0; i < 4; i++) height = height * 256 + rb[pos++];
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(rb);
        return texture;
    }

    [Serializable]
    class JsonFishProperty
    {
        public string name;
        public string explanation;
        public float score;
        public float width;
        public float height;
    }
}
