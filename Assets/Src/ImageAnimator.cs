using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FlySpriteState
{
    First,
    Run,
    Stand
}
public class ImageAnimator : MonoBehaviour
{

    public List<Sprite> FirstImages;
    public Sprite original;

    public float time = 0.05f;
    public List<Sprite> RunImages;
    public List<Sprite> standImages;
    // bool isEnd=true;
    bool IsFirstEnd = true;

    public void play(FlySpriteState state)
    {
        switch (state)
        {
            case FlySpriteState.First:
                IsFirstEnd = false;
              StartCoroutine(PlayImage(FirstImages, true));
                break;
            case FlySpriteState.Run:
                if (IsFirstEnd)
                {
                    StopAllCoroutines();
                  StartCoroutine(  PlayImage(RunImages, false));
                }

                break;
            case FlySpriteState.Stand:
                if (IsFirstEnd)
                {
                    StopAllCoroutines();
                   StartCoroutine( PlayImage(standImages, false));
                }
                break;
        }
    }

    IEnumerator PlayImage(List<Sprite> imageFrame, bool isFirst)
    {
        Debug.Log("jin~~~~~~~~~"+IsFirstEnd);
        for (int i = 0; i < imageFrame.Count; i++)
        {
            GetComponent<Image>().sprite = imageFrame[i];

            if (i == imageFrame.Count - 1)
            {
                if (isFirst)
                {
                    IsFirstEnd = true;
                   
                }
              //  else { i = -1; }

            }
            yield return new WaitForSeconds(time);
        }
        play(FlySpriteState.Stand);

    }
    public void Reset()
    {
        StopAllCoroutines();
        GetComponent<Image>().sprite = original;
        IsFirstEnd = true;
        // GetComponent<ImageAnimator>().enabled = false;
    }

}
