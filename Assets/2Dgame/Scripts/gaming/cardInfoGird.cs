using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardInfoGird : MonoBehaviour
{
    public int id = 0;

    private ObjectInfo info = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetId(int id)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoId(id);
        cardInfo card = this.GetComponentInChildren<cardInfo>();
        card.SetIcoName(info.iconName);
    }

    public void ClearCard()
    {
        this.id = 0;
        this.info = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
