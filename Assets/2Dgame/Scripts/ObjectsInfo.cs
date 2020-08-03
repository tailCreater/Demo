using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour
{
    public static ObjectsInfo instance;

    public TextAsset objectsInfoListText;

    private Dictionary<int,ObjectInfo> objectInfoDict = new Dictionary<int, ObjectInfo>();

    

    void Awake()
    {
        instance = this;
        ReadInfoListText();
        print(objectInfoDict.Keys.Count);
       
    }

    public ObjectInfo GetObjectInfoId(int id)
    {
        ObjectInfo info = null;
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }

    void ReadInfoListText()
    {
        string text = objectsInfoListText.text;
        string[] strArray = text.Split('\n');
        
        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            ObjectInfo info = new ObjectInfo();
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string iconName = proArray[2];
            int value = int.Parse(proArray[3]);
            info.id = id;
            info.name = name;
            info.iconName = iconName;
            info.value = value;
            objectInfoDict.Add(id,info);  //添加到字典后可以方便用id来查找到卡牌
        }
        
    }


}

public enum ObjectType
{

}

public class ObjectInfo
{
    public int id;
    public string name;
    public string iconName; //图集中的名称
    public int value;
}