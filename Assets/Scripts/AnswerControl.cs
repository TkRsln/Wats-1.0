using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerControl : MonoBehaviour {


    public Button btn_answer;
    public int type = 1; //
    public int type_old = 0;
    public string[] type_name = { "Only Type", "Only Option", "Option With Similar" };
    public GameObject pre_button;
    public GameObject pre_fill;
    public GameObject pre_cor;
    public GameObject pre_fail;
    public Transform parent;
    public QuestCreater qc;
    private string question;
    private string answer;
    private int questNo = 0;
    public Text tx_quest;
    public Bar bar;

    private void Start()
    {
        qc = GetComponent<QuestCreater>();
    }

    public void check(string msg)
    {

        next();
    }

    public void onType()
    {
        type=(type+1)%type_name.Length;
        btn_answer.GetComponent<Text>().text = type_name[type];
    }

    public void onAButtons(int i)
    {
        string msg = parent.GetChild(0).GetChild(i).GetComponentInChildren<Text>().text;
        if (msg.Equals(answer))
        {
            Instantiate(pre_cor, transform);
            bar.cor();
        }
        else
        {
            GameObject o = Instantiate(pre_fail, transform);
            o.GetComponentInChildren<Text>().text = answer.Replace("_", " ");
            bar.fa();
        }
        next();

    }

    public void onTField(string str)
    {
        Debug.Log("TestT>" + str);
    }

    public void next()
    {
        fix();
        qc.next();
    }
    public void fix()
    {
        if (type_old != type)
        {
            if(parent.transform.childCount>0)Destroy(parent.transform.GetChild(0).gameObject);
            if (type != 0)
            {
                GameObject btn = Instantiate(pre_button, parent);
                btn.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { onAButtons(0); });
                btn.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { onAButtons(1); });
                btn.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { onAButtons(2); });
            }
            else
            {
                GameObject btn = Instantiate(pre_fill, parent);
                btn.transform.GetChild(0).GetComponent<InputField>().onValueChanged.AddListener(delegate { onTField(btn.transform.GetChild(0).GetComponent<InputField>().text); });
            }
            type_old = type;
        }
    }

    public void setQuestion(int questNo)
    {
        this.questNo = questNo;
        question = ((Question)qc.quests[questNo ]).question;
        answer = ((Question)qc.quests[questNo  ]).answer.Replace("_", " ");
        tx_quest.text = question;
        string[] options = ((Question)qc.quests[questNo]).getOptions();
        if (type != 0)
        {
            // Silindi
            for (int i = 0; i < 3; i++) parent.GetChild(0).GetChild(i).GetComponentInChildren<Text>().text = options[i].Replace("_"," ");
        }
    }

}

/*
 * 
            int total = 0;
            string[] answer =new string[3];
            if (type == 2)
            {
                word w = ((word)qc.qu.mined[questNo]);
                for(int i = 0; i < w.words.Length; i++)
                {
                    if (this.answer != w.words[i].word)
                    {
                        total++;
                        answer[i] = w.words[i].word;
                    }
                }
            }
            for (; total < 3; total++)
            {
                answer[total] = ((word)qc.qu.mined[Random.Range(0, qc.qu.mined.Count-1)]).words[Random.Range(0, ((word)qc.qu.mined[Random.Range(0, qc.qu.mined.Count-1)]).words.Length-1)].word;
            }
            int r = Random.Range(0, 2);
            answer[r] = this.answer;
 * 
 */
