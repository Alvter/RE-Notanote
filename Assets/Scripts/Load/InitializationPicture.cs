using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;

public class InitializationPicture : MonoBehaviour
{
    public bool isStart = false;

    public GameObject Picture;

    public GameObject PictureList;

    public void ScriptStart()
    {
        for (int i = 0; i < chart.performImgList.Count; i++)
        {
            var pic = chart.performImgList[i];
            GameObject picture = Instantiate(Picture, new Vector3(pic.pos[0], pic.pos[1], pic.pos[2]), Quaternion.identity, PictureList.transform);//创建显示判定线
            SpriteRenderer sprite = picture.GetComponent<SpriteRenderer>();
            picture.transform.localScale = new Vector2(scale, scale);

            //获取贴图
            string imagePath = "Chart/" + chart.path + "/" + pic.path;
            Sprite texture = Resources.Load<Sprite>(imagePath);

            //设置属性
            sprite.color = pic.color;
            sprite.sprite = texture;
            sprite.sortingOrder = pic.sortingOrder;
            sprite.sortingLayerName = pic.name;
        }

        isStart = !isStart;
    }

    void Update()
    {
        if (!isStart) return;


    }
}
