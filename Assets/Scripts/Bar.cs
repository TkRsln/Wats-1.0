using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    public Image bar_fail;
    public Image bar_cor;
    public Image bar_back;
    public Text text;
    public int correct = 0;
    public int fail = 0;
    private string msg = "<color=#AE2222> %f </color> - <color=#229BAE> %c </color> ";

    public void cor()
    {
        correct++;
        calculate();
    }
    public void fa()
    {
        fail++;
        calculate();
    }
    public void calculate()
    {
        text.text = msg.Replace("%f",fail+"").Replace("%c",correct+"");
        //float main = bar_back.GetComponent<RectTransform>().sizeDelta[0];
        //float f = main - (main * fail / (fail + correct));
        //float c = main - (main * correct / (fail + correct));
        //bar_fail.GetComponent<RectTransform>().setS
        
    }
}
