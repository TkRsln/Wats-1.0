using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question{
    public bool fill = false;
    public string[] fakes;
    public string answer;
    public string question;

    public string[] getOptions()
    {
        string[] str = fakes;
        str[Random.Range(0, str.Length)] = answer;
        return str;
    }

    public Question(string quest,string answer)
    {
        this.question = quest;
        this.answer = answer;
        fill = false;
    }
    public Question(string quest,string answer,string[] fakes)
    {
        this.question = quest;
        this.answer = answer;
        this.fakes = fakes;
        fill = true;
    }
}
