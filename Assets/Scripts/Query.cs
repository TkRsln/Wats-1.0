using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Query : MonoBehaviour {

    private string path_gen =       "/storage/emulated/0/";
    private string path_folder =    "/storage/emulated/0/WAT";
    public string[] msgs = {

        "bilingual	able to speak two languages equally well	bilingual:adj bilingualism:n	pascal is bilingual in French and English"
        
        //"m:destination exp:the place that someone or something is going to. word:destination:n sen:We arrived at out destination",
        //"m:broaden exp:to become larger in distance from side to side / to increase word:broaden:v broad:adj sen:Living abroad makes you learn and experience new things and it broadens your horizons",
        //"m:experience exp:to encounter or undergo an event or occurence word:experience:n sen:Childeren need to experience things for themselves in order to learn from them",
        //"m:explore exp:to travel around an area in order to find out about it word:explore:v explorer:n exploration:n sen:test1 aa bb cc",
        //"m:vast exp:The vast majority of students attend state schools. word:vast:adj vastness:n sen:test1 aa bb cc",
        //"m:diverse exp:very different from each other. word:diverse:adj varied:sy similar:an diversity:n sen:the school offers courses as dicerse as history of jazz music and archaelogy.",
        //"m:iniyiate exp:to cause / to begin word:initiate:verb launch:sy end:an initiator:n initiative:n sen:initiate a conversation",
        //"m:reveal exp:to make known something that was previously secret or unknown word:reveal:v show:sy hide:an sen:After the police asked man questions for three hours, he revealed where he had hidden the stolen cars."

    };
    [SerializeField]
    public ArrayList mined = new ArrayList();

    // Use this for initialization
    void Start ()
    {
        
        //foreach(string msg in msgs)
        //mined.Add(startQuary(msg));
    }
    public void first()
    {
        if (!Directory.Exists(path_folder))
        {
            Directory.CreateDirectory(path_folder);
        }
        if (Directory.GetFiles(path_folder).Length > 0)
        {
            string[] str = File.ReadAllLines(Directory.GetFiles(path_folder)[0]);
            msgs = str;
        }

        foreach (string msg in msgs) mined.Add(startQuary(msg));
         
    }

    public word startQuary(string msg)
    {


        word w = new word();
        int start = 0;
        w.main = msg.Substring(0, msg.IndexOf("	", start));
        Debug.Log(w.main+" | "+start);
        start = msg.IndexOf("	", start)+1;
        w.explain = msg.Substring(start, msg.IndexOf("	", start)-start);
        Debug.Log(w.explain + " | " + start);
        start = msg.IndexOf("	", start)+1;
        w.words = separate(msg.Substring(start, msg.IndexOf("	", start)-start));
        Debug.Log(msg.Substring(start, msg.IndexOf("	", start) - start) + " | " + start);
        start = msg.IndexOf("	", start)+1;
        w.sentence = msg.Substring(start, msg.Length - start);
        Debug.Log(w.sentence + " | " + start);

        return w;


        /*  //Reader 1.0
        int mode = 0; //0:main / 1:exp / 2:words / 3:sentence 
        word w = new word();
        w.main = msg.Substring(msg.IndexOf("m:")+2, msg.IndexOf(" ", msg.IndexOf("m:"))- msg.IndexOf("m:")-2);
        w.explain = msg.Substring(msg.IndexOf("exp:")+4, msg.IndexOf("word:")- msg.IndexOf("exp:")-4);
        w.words = separate(msg.Substring(msg.IndexOf("word:") + 5, msg.IndexOf("sen:") - 5 - msg.IndexOf("word:")));
        w.sentence = msg.Substring(msg.IndexOf("sen:")+4,msg.Length- msg.IndexOf("sen:")-4);
        return w;
        */
    }

    public int mode(string word,int curnt)
    {
        if (word.Equals("m:")) return 0;
        else if (word.Equals("exp:")) return 1;
        else if (word.Equals("word:")) return 2;
        else if (word.Equals("sen:")) return 3;
        else return curnt;
    }

    public found[] separate(string msg)
    {
        ArrayList list = new ArrayList();
        string temp = "";
        for(int i = 0; i < msg.Length; i++)
        {
            if(msg[i]!=' ')
            {
                temp += msg[i];
            }
            else
            {
                list.Add(temp);
                temp = "";
            }
        }
        if (temp.Length != 0) list.Add(temp);
        found[] all = new found[list.Count];
        for(int i = 0; i < list.Count; i++)
        {
            string word =(string) list[i];
            all[i] = new found();
            all[i].word = word.Substring(0, word.IndexOf(":"));
            string id = word.Substring(word.IndexOf(":") + 1, word.Length-(word.IndexOf(":") + 1));
            if (id.Equals("n")) all[i].id = 0;
            else if (id.Equals("v")) all[i].id = 1;
            else if (id.Equals("adj")) all[i].id = 2;
            else if (id.Equals("an")) all[i].id = 3;
            else if (id.Equals("sy")) all[i].id = 4;
            else if (id.Equals("adv")) all[i].id = 5;
        }
        return all;
    }
}
