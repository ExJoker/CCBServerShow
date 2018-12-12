using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ShowType
{
    All,
    HouseStyle,
    Outside,
    Transport
}
public class HandleUI : MonoBehaviour
{
    public static HandleUI Instance;
    public Canvas UI;
    public Sprite people;
    Transform houseStyleUI;
    Transform outsideUI;
    Transform transportUI;
    Transform positionUI;
    public HousesDate IsShowingHouse;
    Transform flySprite;
    Transform qiqiu;
    ShowType IsShowingType;
    bool IsStand = true;
    int waitSecond = 2;
    float playSecond = 0.02f;
    int ChangeImageSecond = 4;
    public AudioClip flyClip;
    public AudioClip StandClip;
    public AudioClip paishouClip;
    public GameObject buildingCamera;
    GameObject houseUI;
    public GameObject background;
    private void Start()
    {
        Instance = this;
        houseStyleUI = transform.Find("houseStyle");
        outsideUI = transform.Find("outside");
        transportUI = transform.Find("transport");
        positionUI = transform.Find("position");
        flySprite = transform.Find("flySprite");
        qiqiu = transform.Find("qiqiu");
        houseUI = background.transform.Find("HouseUI").gameObject;
        UIReset();
    }
    public void showUI(HousesDate house, ShowType showType)
    {

        if (house == IsShowingHouse && showType == IsShowingType) { return; }
        if (house == IsShowingHouse)
        {
            if (!IsStand) return;
            positionUI.GetComponent<RawImage>().material.SetColor("_Color", new Color(1, 1, 1, 1));
            houseStyleUI.gameObject.SetActive(false);
            outsideUI.gameObject.SetActive(false);
            transportUI.gameObject.SetActive(false);
           // buildingCamera.gameObject.GetComponent<RotateBuilding>().Reset();
        }
        else
        {
            UIReset();
            IsShowingHouse = house;
        }
        StopAllCoroutines();
        IsShowingType = showType;
        StartCoroutine(showPosition());
    }
    public IEnumerator showPosition()
    {

        //播放背景position图片
        // Debug.Log(GameManager.Instance.IsPosition);
        if (GameManager.Instance.IsPosition)
        {
            positionUI.GetComponent<RawImage>().texture = IsShowingHouse.position;
            positionUI.gameObject.SetActive(true);
            while (positionUI.GetComponent<RawImage>().material.color.a <= 0.95f)
            {

                Debug.Log(positionUI.GetComponent<RawImage>().material.color.a);
                positionUI.GetComponent<RawImage>().material.SetColor("_Color", new Color(1, 1, 1, Mathf.Lerp(positionUI.GetComponent<RawImage>().material.color.a, 1, 0.1f)));
                yield return new WaitForSeconds(playSecond);
            }
        }
        else
        {
            //展示小房子
            //houseUI.transform.SetParent(background.transform.Find("Points").Find(IsShowingHouse.Name));
            //houseUI.transform.localPosition = Vector3.zero;
            //houseUI.transform.localScale = Vector3.one;
            //houseUI.SetActive(true);
        }
        if (!flySprite.gameObject.activeSelf)
        {
            yield return StartCoroutine(ShowFlySprite());
          

        }


        switch (IsShowingType)
        {
            case ShowType.HouseStyle:
                StartCoroutine(ShowHouseStyle());
                break;
            case ShowType.Outside:
                StartCoroutine(showOutside());
                break;
            default:
                StartCoroutine(showTransport());
                break;
        }

    }

    public IEnumerator showTransport()
    {
        yield return StartCoroutine(runToTarget(transportUI.Find("Point")));
        transportUI.gameObject.SetActive(true);
        List<Sprite> transport = IsShowingHouse.transport;
        Transform ShowImage = transportUI.Find("Image");
        GameManager.Instance.SendMSG(IsShowingHouse.Name + "*" + ShowType.Transport);
        for (int i = 0; i < transport.Count; i++)
        {
            ShowImage.GetComponent<Image>().sprite = transport[i];
            ShowImage.GetComponent<AlphaShake>().Shake();

            yield return new WaitForSeconds(ChangeImageSecond);
        }
        yield return new WaitForSeconds(waitSecond);
        GameManager.Instance.SendMSG("End");
        if (IsShowingType == ShowType.All)
        {
            StartCoroutine(showOutside());
            transportUI.gameObject.SetActive(false);
        }
        else
        {
            // yield return new WaitForSeconds(waitSecond);
            UIReset();
        }

    }
    public IEnumerator showOutside()
    {
        yield return StartCoroutine(runToTarget(outsideUI.Find("Point")));
        outsideUI.gameObject.SetActive(true);
        List<Sprite> outsideList = IsShowingHouse.outside;
        Transform ShowImage = outsideUI.Find("Image");
        GameManager.Instance.SendMSG(IsShowingHouse.Name + "*" + ShowType.Outside);
        outsideUI.Find("TextBackground").Find("Text").GetComponent<Text>().text = IsShowingHouse.outsideIntroduce;
        for (int i = 0; i < outsideList.Count; i++)
        {
            ShowImage.GetComponent<Image>().sprite = outsideList[i];
            ShowImage.GetComponent<AlphaShake>().Shake();
            yield return new WaitForSeconds(ChangeImageSecond);

        }
        yield return new WaitForSeconds(waitSecond);
        GameManager.Instance.SendMSG("End");
        if (IsShowingType == ShowType.All)
        {
            StartCoroutine(ShowHouseStyle());
            outsideUI.gameObject.SetActive(false);
        }
        else
        {
            UIReset();
        }

    }
    public IEnumerator ShowHouseStyle()
    {
        yield return StartCoroutine(runToTarget(houseStyleUI.Find("Point")));
        houseStyleUI.gameObject.SetActive(true);
        Dictionary<HouseStyle, string> housestyleList = IsShowingHouse.hostStyle;
        Transform ShowImage = houseStyleUI.Find("Image");
        GameManager.Instance.SendMSG(IsShowingHouse.Name + "*" + ShowType.HouseStyle);
        foreach (var item in housestyleList)
        {
            houseStyleUI.Find("title").GetComponent<Text>().text = item.Key.Name;
            houseStyleUI.Find("TextBackground").Find("Text").GetComponent<Text>().text = item.Value;
            for (int j = 0; j < item.Key.Sprite.Count; j++)
            {
                ShowImage.GetComponent<AlphaShake>().Shake();
                ShowImage.GetComponent<Image>().sprite = item.Key.Sprite[j];

                if (IsShowingHouse.Name == "rundayuanting" || IsShowingHouse.Name == "chengshichuntian")
                {
                   // buildingCamera.gameObject.GetComponent<RotateBuilding>().rotateChengshi(IsShowingHouse.Name);
                    yield return new WaitForSeconds(15);
                }
                else
                {
                    yield return new WaitForSeconds(ChangeImageSecond);
                }

            }
        }
        yield return new WaitForSeconds(waitSecond);
        GameManager.Instance.SendMSG("End");
        UIReset();

    }
    IEnumerator ShowFlySprite()
    {
        IsStand = false;
        flySprite.gameObject.SetActive(false);
        flySprite.GetComponent<RectTransform>().localPosition = new Vector3(22, -850, 0);
        flySprite.GetComponent<AudioSource>().clip = flyClip;
        flySprite.GetComponent<AudioSource>().Play();
        while (flySprite.GetComponent<RectTransform>().localPosition.y < 900)
        {
            flySprite.GetComponent<RectTransform>().localPosition = Vector3.Lerp(flySprite.GetComponent<RectTransform>().localPosition, new Vector3(22, 1000, 0), 0.1f);
            flySprite.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(flySprite.GetComponent<RectTransform>().sizeDelta, new Vector2(700, 700), 0.1f);
            yield return new WaitForSeconds(playSecond);
        }
        qiqiu.position = flySprite.position;
        StartCoroutine(ShowQiqiu());
        // qiqiu.position = flySprite.position;
        flySprite.gameObject.SetActive(true);
        flySprite.GetComponent<Image>().sprite = people;
        while (flySprite.GetComponent<RectTransform>().localPosition.y > 100)
        {
            flySprite.GetComponent<RectTransform>().localPosition = Vector3.Lerp(flySprite.GetComponent<RectTransform>().localPosition, new Vector3(22, 80, 0), 0.1f);
            flySprite.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(flySprite.GetComponent<RectTransform>().sizeDelta, new Vector2(800, 800), 0.1f);
            yield return new WaitForSeconds(playSecond);
        }
        flySprite.GetComponent<ImageAnimator>().play(FlySpriteState.First);
        if (GameManager.Instance.clipList.ContainsKey(IsShowingHouse.Name))
        {
            flySprite.GetComponent<AudioSource>().clip = GameManager.Instance.clipList[IsShowingHouse.Name];
            flySprite.GetComponent<AudioSource>().Play();
        }
        yield return null;
    }

    IEnumerator ShowQiqiu()
    {
        qiqiu.gameObject.SetActive(true);
        Vector3 endPosition = new Vector3(qiqiu.GetComponent<RectTransform>().position.x, 1300, qiqiu.GetComponent<RectTransform>().position.x);
        Vector3 controlerPosition = qiqiu.GetComponent<RectTransform>().position + (endPosition - qiqiu.GetComponent<RectTransform>().position) * 0.5f + new Vector3(500, 0, 0);
        Vector3[] positionArr = BezierUtils.GetBeizerList(qiqiu.GetComponent<RectTransform>().position, controlerPosition, endPosition, 80);
        for (int i = 0; i < positionArr.Length; i++)
        {
            qiqiu.GetComponent<RectTransform>().position = positionArr[i] - new Vector3(0, 0, positionArr[i].z);
            yield return new WaitForSeconds(playSecond);
        }
        qiqiu.gameObject.SetActive(false);
        IsStand = true;
        yield return null;
    }
    IEnumerator runToTarget(Transform target)
    {

        flySprite.GetComponent<ImageAnimator>().play(FlySpriteState.Run);
        if (!flySprite.GetComponent<AudioSource>().isPlaying)
        {
            flySprite.GetComponent<AudioSource>().clip = paishouClip;
            flySprite.GetComponent<AudioSource>().Play();
        }
        while (Vector3.Distance(flySprite.position, target.position) > 2f)
        {
            flySprite.position = Vector3.Lerp(flySprite.position, target.position, 0.05f);
            yield return new WaitForSeconds(playSecond);
        }
       // flySprite.GetComponent<ImageAnimator>().play(FlySpriteState.Stand);

    }
    public void UIReset()
    {
        positionUI.GetComponent<RawImage>().material.SetColor("_Color", new Color(1, 1, 1, 0));
        houseStyleUI.gameObject.SetActive(false);
        outsideUI.gameObject.SetActive(false);
        transportUI.gameObject.SetActive(false);
        positionUI.gameObject.SetActive(false);
        qiqiu.gameObject.SetActive(false);
        flySprite.GetComponent<ImageAnimator>().Reset();
        flySprite.gameObject.SetActive(false);
        flySprite.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 1300);
       // buildingCamera.gameObject.GetComponent<RotateBuilding>().Reset();
        GameManager.Instance.SendMSG("End");
        houseUI.SetActive(false);
        StopAllCoroutines();
        IsShowingHouse = null;
        IsStand = true;

    }

}
