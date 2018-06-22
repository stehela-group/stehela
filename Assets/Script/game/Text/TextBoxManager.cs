using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TextBoxManager {

    public static GameObject textBox ;
    public static TextMeshProUGUI theText;
    


    public static TextAsset textFile;
    public static string[] textLines;
    public static  bool dialogActive;

    public static int currentLine;
    public static int endAtLine;

    protected CText Mensaje = new CText("");




    void Start()
    {

       

        if (textFile != null)
        {
            //Paso de Linea 
            textLines = (textFile.text.Split('\n'));


        }

        if (endAtLine  == 0)
        {
            //Me fijo el final de la linia menos 1 
            endAtLine = textLines.Length - 1;

        }

    }





    void Update()
    {

       
       

        if (currentLine < endAtLine)
            {

                theText.text = textLines[currentLine];
            }


            // Le doy espacio para que siga pasando para la otra linea 
            if (Input.GetKeyDown(KeyCode.Return))
            {
                currentLine += 1;

            } 

        if (currentLine > endAtLine)
        {

            textBox.SetActive(false);

        }


    }


    public static void setActive(bool aActive)
    {

         textBox.SetActive(aActive);

    }



  



}

 