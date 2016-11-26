using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using AK;

public class EvalFn : MonoBehaviour {
    B83ExpressionParser.Expression zFn;
    GameObject InputField;

    string[] depVars;
    AK.ExpressionSolver solver;


	// Use this for initialization
	void setArgs (params double[] values) {
        for (int i = 0; i < depVars.Length; i++)
        {
            solver.SetGlobalVariable(depVars[i], values[i]);
        }
    }


    // parser can't understand [constant]x etc. This turns it into [constant]*x, which it does understand.
    // @TODO: Make it understand xy, yx, xx, yy, etc
    string EnsafenFnString(string fnString)
    {

        foreach (string depVar in depVars)
        {
            fnString = Regex.Replace(fnString, @"(\d+)" + depVar, "$1*" + depVar);
        }


        return fnString;
    }

    public void setZFn()
    {


        string userInputFunction = InputField.GetComponent<InputField>().text;
        userInputFunction = EnsafenFnString(userInputFunction); // make 5x -> 5*x, etc

        Expression zFn = solver.SymbolicateExpression(EnsafenFnString(userInputFunction));

        setArgs(5, 10); 

        Debug.Log("Result: " + zFn.Evaluate());
    }

    void Start()
    {
        // config
        depVars = new string[2] { "x", "y" };

        // object retrieval
        InputField = GameObject.Find("UIInputFunction");

        // init
        solver = new AK.ExpressionSolver();

        // logic
        foreach (string depVar in depVars)
        {
            solver.SetGlobalVariable(depVar, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
