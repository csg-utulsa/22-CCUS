using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnProximity : MonoBehaviour
{
    private GameObject VideoScreen;
    // Start is called before the first frame update
    void Start()
    {
        VideoScreen = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            VideoScreen.GetComponent<VideoPlayer>().Play();
            Debug.Log("Player is triggering video screen!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VideoScreen.GetComponent<VideoPlayer>().Pause();
        }
    }
}
