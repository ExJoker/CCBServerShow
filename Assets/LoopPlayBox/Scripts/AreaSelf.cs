using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaSelf : Singleton<AreaSelf>
{
    Image thisImage, lightImage;
    Vector2 panelHidePos2,panelShowPos2;

    public int id;

    DelegateColor delegatecolor = new DelegateColor();
    DelegateScale delegatesize = new DelegateScale();
    DelegatePosition delegatepos = new DelegatePosition();

    Color hideColor = new Color(1, 1, 1, 0.5f), showColor = new Color(1, 1, 1, 1f);
    public Texture texInPanel;
    public string texName;
    void OnEnable()
    {
        if (id != 0)
        {
            StartCoroutine(LoadTexture());                
        }
        thisImage = transform.GetComponent<Image>();
        lightImage = transform.Find("Light").GetComponent<Image>();
        panelHidePos2 = lightImage.GetComponent<RectTransform>().localPosition;
        panelShowPos2 = lightImage.GetComponent<RectTransform>().localPosition + new Vector3(0, 300);

        thisImage.enabled = false;
        lightImage.enabled = false;

        delegatecolor._Speed = 0.05f;
        delegatepos._Speed = 0.1f;
        delegatesize._Speed = 0.1f;
    }

    IEnumerator LoadTexture()
    { 
        string sql = "SELECT * FROM `biulding` WHERE id = " + id + ";";
        List<string[]> result = new List<string[]>();
        result = DataDBContorller.Select(sql);
        texName = result[0][2];
        string TempURL = "file://" + Application.streamingAssetsPath + @result[0][4];
        WWW www = new WWW(TempURL);
        yield return www;
        texInPanel= www.texture;
        Resources.UnloadUnusedAssets();
    }
    void Update()
    {
        delegatecolor.Update();
        delegatesize.Update();
        delegatepos.Update();
    }

    public void DisplayThis(RectTransform rect)
    {
        thisImage.enabled = true;
        thisImage.color = new Color(1, 1, 1, 0);

        rect.localScale = Vector2.zero;
        rect.localPosition = panelHidePos2;
        rect.SetAsLastSibling();
        AreaManager.Instance.showRect.transform.Find("Image").GetComponent<RawImage>().texture = texInPanel;
        AreaManager.Instance.showRect.transform.Find("Text").GetComponent<Text>().text = texName;

        //出现选区
        delegatecolor.Start(thisImage, showColor, delegate {
            lightImage.enabled = true;
            //闪烁
            delegatecolor.Start(lightImage, showColor, delegate {
                delegatecolor.Start(lightImage, hideColor, delegate {
                    delegatecolor.Start(lightImage, showColor, delegate {
                        delegatecolor.Start(lightImage, hideColor, delegate {
                            delegatecolor.Start(lightImage, showColor, delegate {

                                //面板出现
                                delegatesize.Start(rect, new Vector3(2.5f, 2.5f, 2.5f));
                                delegatepos.Start(rect, panelShowPos2);
                            });
                        });
                    });
                });
            });
        });
    }

    public void HideThis()
    {
        lightImage.enabled = false;
        delegatecolor.Start(thisImage, new Color(1, 1, 1, 0), delegate
        {
            thisImage.enabled = false;
        });
    }
}
