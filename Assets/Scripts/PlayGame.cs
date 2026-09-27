using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{

    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        source.Play();
        DontDestroyOnLoad(source.gameObject);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
}