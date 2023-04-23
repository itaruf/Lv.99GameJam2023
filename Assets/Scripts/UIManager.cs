using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private float _flickerInterval = 0.25f;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(FlickerText(_startText, _flickerInterval));
    }

    IEnumerator FlickerText(TextMeshProUGUI textToFlicker, float interval)
    {
        while (true)
        {
            textToFlicker.color = new Color(1f, 1f, 1f, 0.25f);
            yield return new WaitForSeconds(interval);
            textToFlicker.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "TitleScreen")
        {
            SceneManager.LoadScene("DebugScene");
        }
    }
}
