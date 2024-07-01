using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using UnityEngine;
using UnityEngine.UI;
using OpenRSR;
using OpenRSR.Modding;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CSScriptLib;

public class ScriptLoader : MonoBehaviour
{
    string modFolderPath;
    public List<string> installedModPaths = new List<string>();
    //private CSharpScriptCompiler scriptCompiler = new CSharpScriptCompiler();
    // Start is called before the first frame update
    void Start()
    {
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Mods"))) {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Mods"));
        }
        modFolderPath = Path.Combine(Application.persistentDataPath, "Mods");
        installedModPaths = Directory.GetDirectories(modFolderPath).ToList();

        foreach (string modPath in installedModPaths) {
            string ScriptsFolderPath = Path.Combine(modPath, "Scripts");
            if (Directory.Exists(ScriptsFolderPath)) {
                string[] files = Directory.GetFiles(ScriptsFolderPath);
                foreach (string file in files) {
                    var result = CSScript.Evaluator.LoadFile(file);
                    gameObject.AddComponent(result.GetType());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
