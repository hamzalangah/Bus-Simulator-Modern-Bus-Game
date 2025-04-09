using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoTypeText : MonoBehaviour
{

    public float letterPause = 0.2f;
    public AudioClip sound;
    private string word;
    public bool music = true;

    private Text uiText;
    private AudioSource audioSource;

    public void OnEnable()
    {
        uiText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();

        if (uiText != null)
        {
            word = uiText.text;
            uiText.text = "";  // Start with an empty text
            StartCoroutine(TypeText());
        }
        else
        {
            Debug.LogError("Text component not found!");
        }
    }

    public void OnDisable()
    {
        if (uiText != null)
        {
            uiText.text = "";  // Clear text on disable
        }
    }

    public IEnumerator TypeText()
    {
        if (music)
        {
            foreach (char letter in word.ToCharArray())
            {
                uiText.text += letter;

                if (PlayerPrefs.GetInt("Music") == 0 && sound && audioSource)
                {
                    audioSource.PlayOneShot(sound);
                }

                yield return new WaitForSeconds(letterPause);
            }
        }
    }
}
