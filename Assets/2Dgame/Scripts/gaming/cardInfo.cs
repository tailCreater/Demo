using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardInfo : MonoBehaviour
{
    private UISprite sprite;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }

    void Update()
    {
        
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoId(id);
        sprite.spriteName = info.iconName;
    }

    public void SetIcoName(string iconName)
    {
        sprite.spriteName = iconName;
    }


}
