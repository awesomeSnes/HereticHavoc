using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{

    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EndGame()
    {
        source.Play();
        DontDestroyOnLoad(source.gameObject);
        Application.Quit();
    }

    // Update is called once per frame
}