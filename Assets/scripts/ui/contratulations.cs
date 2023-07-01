using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class contratulations : MonoBehaviour
{
    // Start is called before the first frame update
    private TMP_Text text; 

    [SerializeField] List<string> messages = new();
    void Awake()
    {
        text = GetComponent<TMP_Text>();
        print("start()");
    }
    private void OnEnable() {
        text.text = messages[Random.Range(0, messages.Count)];
    }
}
