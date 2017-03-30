using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITyper : MonoBehaviour {
    public string msg = "Replace";
    private Text textComp;
    public float startDelay = 1.0f;
    public float typeDelay = 0.05f;
    
	// Use this for initialization
	void Start () {
        //StartCoroutine("TypeIn");
        //msg = "test\ntest\ntest\ntest\ncan this work?";
	}

    public void SetMessage(string st)
    {
        msg = st;
        StartCoroutine("TypeIn");
    }

    private void Awake()
    {
        textComp = GetComponent<Text>();
    }

    public IEnumerator TypeIn()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i <= msg.Length; i++)
        {
            textComp.text = msg.Substring(0, i);

            yield return new WaitForSeconds(typeDelay);
        }
    }

    public IEnumerator TypeOff()
    {
        for (int i = msg.Length; i >= 0; i--)
        {
            textComp.text = msg.Substring(0, i);
            yield return new WaitForSeconds(typeDelay);
        }
    }
}
