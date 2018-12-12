using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Area
{
    NanShan,
    LongGang,
    BaoAn,
    PingShanXinQu,
    FuTian,
    LongHua
}
public class HousesDate
{
    //小区名字
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }
    //小区所属行政区
    private Area _area;
    public Area Area
    {
        get
        {
            return _area;
        }
    }
    public Dictionary<HouseStyle, string> hostStyle = new Dictionary<HouseStyle, string>();
    public List<Sprite> outside=new List<Sprite>();
    public string outsideIntroduce = "";
    public List<Sprite> transport=new List<Sprite>();
    public Texture2D position = new Texture2D(2048,1024);
   // public AudioClip clip=new AudioClip();
    public HousesDate(string name, Area area)
    {
        _name = name;
        _area = area;
    }
}
public class HouseStyle
{
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public List<Sprite> Sprite=new List<Sprite>();
    public HouseStyle(string name) {
        _name = name;
    }
}
