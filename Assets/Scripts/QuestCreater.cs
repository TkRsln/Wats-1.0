using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCreater : MonoBehaviour {

    public ArrayList quests = new ArrayList();
    public Text quest;
    public GameObject answers;
    public int curnt = 0;
    public AnswerControl ac;
    public Query qu;

    private void Start()
    {
        ac = GetComponent<AnswerControl>();
        qu = GetComponent<Query>();
        ac.fix();
        qu.first();
        create(qu.mined);
        setQuest();
    }

    public void next()
    {
        curnt++;
        setQuest();
    }
    
    public bool isCorrect(string msg)
    {
        return msg.Equals(((Question)quests[curnt]).answer);
    } 

    public void create(ArrayList mined)
    {

        for (int i = 0; i < mined.Count; i++)
        {
            word w = (word)mined[i];
            int r = Random.Range(0, 10);
            string answer="";//
            string quest;
            string[] fakes = new string[3];

            #region Question & Answers
            if (r <= 7)
            {
                for (int a = 0; a < w.words.Length; a++)
                {
                    quest = w.explain+(" <color=cyan>-"+w.words[a].id).Replace("0", "Noun").Replace("1", "verb").Replace("2", "Adjective").Replace("3", "Antonym").Replace("4", "Synonym").Replace("5","Adverb")+"</color>";
                    answer = w.words[a].word;
                    fakes = getRandomAnswers(w, answer, mined);
                    quests.Add(new Question(quest, answer, fakes));
                }

            }
            else
            {
                quest = w.sentence.Replace(w.main, "_ _ _ _");
                answer = w.main;
                fakes = getRandomAnswers(w, answer, mined);
                quests.Add(new Question(quest, answer));

                // BURADAN SİLİNDİ
            }
            #endregion


        }


    } //*/


    public string[] getRandomAnswers(word w, string answer,ArrayList mined)
    {
        string[] fakes = new string[3];
        #region RANDOM ANSWERS
        int count = 0;
        if (ac.type_old == 2)
        {
            for (int b = 0; b < w.words.Length; b++)
            {
                if (count >= 3) break;
                if (!w.words[b].word.Equals(answer))
                {
                    fakes[count] = w.words[b].word;
                    count++;
                }
            }
        }
        if (count < 3)
        {
            for (; count < 3;)
            {
                int mine = Random.Range(0, mined.Count - 1);
                int word = Random.Range(0, ((word)mined[mine]).words.Length - 1);
                word mn = ((word)mined[mine]);
                if (!mn.words[word].word.Equals(answer))
                {
                    fakes[count] = mn.words[word].word;
                    count++;
                }

            }
        }
        #endregion
        return fakes;
    }

    public string fillIt(string word)
    {
        string temp = "";
        for (int i = 0; i < word.Length; i++) temp = "_ ";
        return temp;
    }

    public void setQuest()
    {
        ac.setQuestion(curnt);
    }

}
#region çöp
/*
 * 
 * 
        /*
        questions = new string[mined.Count * 2];
        for(int i = 0; i < mined.Count; i++)
        {
            word w = (word)mined[i];
            int r = Random.Range(0, 10);
            if (r <= 7) // discription //0=noun; 1=verb; 2=adj; 3=an; 4=sy;
            {
                found f = w.words[Random.Range(0, w.words.Length)];
                questions[i * 2] = w.explain+" - "+((f.id+"").Replace("0","Noun").Replace("1", "verb").Replace("2", "Adjective").Replace("3", "Antonym").Replace("4", "Synonym"));
                questions[i * 2 + 1] = f.word;
            }
            else
            {
                questions[i * 2] = w.sentence.Replace(w.main, fillIt(w.main));
                questions[i * 2+1] = w.main;
            }
            ------------------------
             /*
                int count = 0;
                if (ac.type_old == 2)
                {
                    for (int b = 0; b < w.words.Length; b++)
                    {
                        if (count >= 3) break;
                        if (!w.words[b].word.Equals(answer))
                        {
                            fakes[count] = w.words[b].word;
                            count++;
                        }
                    }
                }
                if (count < 3)
                {
                    for (; count < 3;)
                    {
                        int mine = Random.Range(0, mined.Count - 1);
                        int word = Random.Range(0, ((word)mined[mine]).words.Length - 1);
                        word mn = ((word)mined[mine]);
                        if (!mn.words[word].word.Equals(answer))
                        {
                            fakes[count] = mn.words[word].word;
                            count++;
                        }

                    }
                }*/
#endregion