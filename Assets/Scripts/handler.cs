using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class handler : MonoBehaviour
{
    GameObject prevVideo;
    GameObject curtain;
    GameObject player;
    // Start is called before the first frame update

    void OnEnable()
    {
        player = GameObject.Find("XR Origin");

        AlphaAdjust(false);
        if (transform.name == "Cantina")
        {
            GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
            player.transform.position = new Vector3(3.5f, 0f, 0f);
        }
        else if (transform.name == "Cube")
        {
            GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
            player.transform.position = new Vector3(7f, 0f, 0f);
        }
        else if (transform.name == "Mezzanine")
        {
            GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
            player.transform.position = new Vector3(10.5f, 0f, 0f);
        }
        else if (transform.name == "LivingRoom")
        {
            GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
            player.transform.position = new Vector3(0f, 0f, 0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void ButtonClicked()
    {

        prevVideo = transform.root.gameObject;
        prevVideo.GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
        //transform.parent.gameObject.GetComponent<Canvas>().enabled = false; - made the button disappear on click. breaks things when going back to a visited hotspot.
        if (transform.parent.name == "CantinaCanvas")
            StartCoroutine(sceneTransition(transform.root.name, "Cantina"));
        else if (transform.parent.name == "CubeCanvas")
            StartCoroutine(sceneTransition(transform.root.name, "Cube"));
        else if (transform.parent.name == "MezzCanvas")
            StartCoroutine(sceneTransition(transform.root.name, "Mezzanine"));
        else if (transform.parent.name == "LivingCanvas")
            StartCoroutine(sceneTransition(transform.root.name, "LivingRoom"));
    }
    public void AlphaAdjust(bool transistion)
    {
        curtain = GameObject.Find("Alpha");
        if (transistion)
            StartCoroutine(FadeIn(curtain.GetComponent<Image>()));
        else if (transistion == false)
            StartCoroutine(FadeOut(curtain.GetComponent<Image>()));
    }
    private YieldInstruction fadeInstruction = new YieldInstruction();
    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < 1.0f)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / 1.0f);
            image.color = c;
        }
    }
    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < 1.0f)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / 1.0f);
            image.color = c;
        }
    }
    IEnumerator sceneTransition(string sceneName, string newScene)
    {
        GameObject temp = GameObject.Find(sceneName);
        yield return new WaitForSecondsRealtime(1f);
        FindInactive(newScene).SetActive(true);
        temp.SetActive(false);
    }
    GameObject FindInactive(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
