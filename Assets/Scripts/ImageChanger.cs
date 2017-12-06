using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour {

    public Sprite hr;
    public Sprite ch;
    public Sprite eq;
    public Sprite jy;
    public Sprite mj;
    public Sprite sh;
    public Sprite yj;
    public Sprite yy;

    Image image;

    public void Haram()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = hr;
    }

    public void Choong()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = ch;
    }

    public void Eqwon()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = eq;
    }

    public void Jooyil()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = jy;
    }

    public void Mz()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = mj;
    }

    public void Sungho()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = sh;
    }

    public void Yongjae()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = yj;
    }

    public void Yeolyum()
    {
        image = gameObject.GetComponentInChildren<Image>();
        image.sprite = yy;
    }


}

