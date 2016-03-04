using System;
using UnityEditor;
using SyntaxTree.VisualStudio.Unity.Bridge;
using UnityEngine;

[InitializeOnLoad]
public class LangRemover
{

    static LangRemover()
    {
        ProjectFilesGenerator.ProjectFileGeneration += (string name, string content) =>
        {
            // This will remove the language restriction to C# 4
            return content.Replace("<LangVersion Condition=\" '$(VisualStudioVersion)' != '10.0' \">4</LangVersion>", string.Empty);
        };
    }
}
    