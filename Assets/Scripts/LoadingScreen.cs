using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject[] Tips;
    public Text LoadingText;

    private int count = 0;

    private void Start()
    {
        Tips[Random.Range(0, Tips.Length)].SetActive(true);
        StartCoroutine("LoadingScreenEvent");
    }
    IEnumerator LoadingScreenEvent()
    {
        count++;
        if(count < 3)
        {
            LoadingText.text = "Loading";
            yield return new WaitForSeconds(1f);
            LoadingText.text = ". Loading .";
            yield return new WaitForSeconds(1f);
            LoadingText.text = ".. Loading ..";
            yield return new WaitForSeconds(1f);
            LoadingText.text = "... Loading ...";
            yield return new WaitForSeconds(1f);
            StartCoroutine("LoadingScreenEvent");
        }
        else
        {
            GameObject.FindObjectOfType<MainCamera>().GetComponent<Animator>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
