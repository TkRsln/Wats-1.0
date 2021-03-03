using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour {

    public GameObject last;
    public GameObject child;
    public GameObject first;
    public Transform parent;
    private Text txt;
    public string msg = "W.A.T. \n Practice \n1.0";
    public bool onStart = false;

    private float created;

	// Use this for initialization
	void Start () {
        if (onStart)
        {
            start(last, msg);
        }
	}
	
    public void start(GameObject toCreate,string msg)
    {
        if (msg != null) this.msg = msg;
        if (toCreate != null) last = toCreate;
        child = transform.GetChild(0).gameObject;
        txt = child.GetComponentInChildren<Text>();
        parent = transform.parent;
        //child.GetComponent<Animator>().Play("start");
        created = Time.time;
        onStart = true;
    }

	void Update () {
        if (onStart) { 
            int i = (int)((Time.time - created)*6f);

            if (i<msg.Length)
            {
                setText(msg.Substring(0, i));
            }
            else {
                setText(msg + " <color=red>...</color>");
                if(i>msg.Length*4/3)
                {
                    if(last!=null)Instantiate(last, parent);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void setText(string msg)
    {
        txt.text = msg;
    }
}
