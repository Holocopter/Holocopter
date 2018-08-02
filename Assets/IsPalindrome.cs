using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsPalindrome : MonoBehaviour {
    List<char> char1 = new List<char>();

    public string startString = "mam";
    // Use this for initialization
    void Start () {

        isPalindrome(startString);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetValidString(string s)
    {
        string ValidString = "";

        foreach (char currentChar in s.Trim().ToCharArray())
        {
            if (!(currentChar < 'a' || currentChar > 'z'))
            {
                char1.Add(currentChar);
            }
        }
        ValidString = char1.ToString();

        return ValidString;
    }


    public string Reverse(string text)
    {
        char[] cArray = text.ToCharArray();
        string reverse = "";
        for (int i = cArray.Length - 1; i > -1; i--)
        {
            reverse += cArray[i];
        }
        return reverse;
    }

    public bool isPalindrome(string s)
    {

       

        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            return true;
        else

        {   //.Where(character => char.IsLetterOrDigit(character)


            GetValidString(s);
        }



        var inputString = GetValidString(s);
        //.Where(character => char.IsLetterOrDigit(character)
        var reverseString = Reverse(inputString);

        Debug.Log("true or false" + string.Equals(inputString, reverseString));

        Debug.Log("inputString" + inputString.ToString());
        Debug.Log("reverseString" + reverseString.ToString());
        return string.Equals(inputString.ToString(), reverseString.ToString());

    
    }
}
