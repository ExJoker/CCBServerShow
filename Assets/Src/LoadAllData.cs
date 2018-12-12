using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LoadAllData
{
    private static LoadAllData instance;
    public Dictionary<string, HousesDate> houseList = new Dictionary<string, HousesDate>();
    private string spriteFolderPath;
    private string introduceFolderPath;
    public static LoadAllData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoadAllData();
            }
            return instance;
        }
    }

    public void GetFile()
    {
		#if UNITY_EDITOR
				spriteFolderPath = @"C:\Users\sf\Desktop\data1221" + @"\SpriteData";
				introduceFolderPath = @"C:\Users\sf\Desktop\data1221" + @"\TxtData";
		#else
				spriteFolderPath = Application.dataPath+@"\SpriteData";
				introduceFolderPath = Application.dataPath + @"\TxtData";
		#endif
		Debug.Log(spriteFolderPath);
        DirectoryInfo theFolder = new DirectoryInfo(spriteFolderPath);
        foreach (DirectoryInfo item in theFolder.GetDirectories())
        {
            Area area = (Area)Enum.Parse(typeof(Area), item.Name);
            foreach (DirectoryInfo subItem in item.GetDirectories())
            {
                houseList.Add(subItem.Name, searchHouses(area, subItem));
            }
        }
    }

    /// <summary>
    /// 传入区域名称以及小区信息的文件夹
    /// </summary>
    /// <param name="area"></param>
    /// <param name="houses"></param>
    HousesDate searchHouses(Area area, DirectoryInfo houses)
    {
        //新建一个小区类，储存信息，初始化小区所在的区域以及小区名字
        HousesDate houseDate = new HousesDate(houses.Name, area);
        houseDate.outsideIntroduce = getStringIntroduce(houseDate.Name);
        // DirectoryInfo[] styp = houses.GetDirectories();
        foreach (DirectoryInfo item in houses.GetDirectories())
        {
            switch (item.Name)
            {
                //户型类型图
                case "housestyle":
                    foreach (DirectoryInfo subItem in item.GetDirectories())
                    {
                        //subItem是每个户型图的文件夹
                        HouseStyle newHouse = new HouseStyle(subItem.Name);
                        //获取每个文件夹下边的JPG的路径
                        string[] dirHouseType = Directory.GetFiles(subItem.FullName, "*.PNG");
                        newHouse.Sprite.AddRange(getSprites(dirHouseType));
                        houseDate.hostStyle.Add(newHouse, getStringIntroduce(houseDate.Name + newHouse.Name));
                    }
                    break;
                case "outside":
                    string[] dirOutside = Directory.GetFiles(item.FullName, "*.PNG");
                    houseDate.outside.AddRange(getSprites(dirOutside));
                    break;
                case "transport":
                    string[] dirjpgTransport = Directory.GetFiles(item.FullName, "*.JPG");
                    houseDate.transport.AddRange(getSprites(dirjpgTransport));
                    string[] dirPNGTransport = Directory.GetFiles(item.FullName, "*.PNG");
                    houseDate.transport.AddRange(getSprites(dirPNGTransport));
                    break;
                case "position":
                    string[] dirPosition = Directory.GetFiles(item.FullName, "*.JPG");
                    Texture2D tx = new Texture2D(1920, 1200);
                    FileStream files = new FileStream(dirPosition[0], FileMode.Open);
                    byte[] imgByte = new byte[files.Length];
                    files.Read(imgByte, 0, imgByte.Length);
                    files.Close();
                    tx.LoadImage(imgByte);
                    houseDate.position = tx;
                    break;
            }
        }
        return houseDate;
    }
    /// <summary>  
    /// 根据图片路径返回Sprite
    /// </summary>  
    /// <param name="imagePath">图片路径</param>  
    /// <returns>返回的字节流</returns>  
    private List<Sprite> getSprites(string[] imagePath)
    {
        List<Sprite> sprList = new List<Sprite>();
        for (int i = 0; i < imagePath.Length; i++)
        {
            Texture2D tx = new Texture2D(778, 584);
            FileStream files = new FileStream(imagePath[i], FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            tx.LoadImage(imgByte);
            sprList.Add(Sprite.Create(tx, new Rect(0, 0, 778, 584), Vector2.zero));
        }
        return sprList;
    }
    private string getStringIntroduce(string imagepath)
    {
        string introduceString;
        try
        {
            using (StreamReader sr = new StreamReader(introduceFolderPath + @"\" + imagepath + ".txt", Encoding.UTF8))
            {
                introduceString = sr.ReadToEnd();
            }
        }
        catch
        {
            introduceString = "暂无消息";
        }
        return introduceString;
    }
    
}
