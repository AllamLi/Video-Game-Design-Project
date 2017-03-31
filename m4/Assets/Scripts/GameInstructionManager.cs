using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstructionManager : MonoBehaviour {

    public GameObject instructionObj;
    public UITyper typer;
    private int m_State = 0;
    private string msg1, msg2, msg3;

	// Use this for initialization
	void Start () {
        msg1 = "Try to click the mouse left button to release an ATTRACTING BALL";
        msg2 = "Try to click the mouse right button to release an REPELLING BALL";
        msg3 = "Press ESC to call out the setting panel";
        typer.SetMessage(msg1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1"))
        {
            if (m_State == 0)
            {
                m_State++;
                typer.SetMessage(msg2);
                Debug.Log("mouse left button");
            }
        }

        if (Input.GetButton("Fire2"))
        {
            if (m_State == 1)
            {
                m_State++;
                typer.SetMessage(msg3);
                Debug.Log("mouse right button");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_State == 2)
            {
                m_State = -1;
                instructionObj.SetActive(false);
                Debug.Log("esc");
            }
        }
	}
}
