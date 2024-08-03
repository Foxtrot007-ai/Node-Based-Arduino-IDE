using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.API;
using Backend.IO;

namespace Backend
{
    public class CodeManager
    {
        public enum VariableStatus
        {
            Unknown,
            Set,
            Global,
            Param,
        }

        public List<string> CodeLines { get; }
        public Dictionary<IVariable, VariableStatus> Variables { get; }
        public HashSet<string> Includes { get; }
        private List<string> _ignoreIncludes = new() { "system", "common" };

        public CodeManager()
        {
            CodeLines = new List<string>();
            Variables = new Dictionary<IVariable, VariableStatus>();
            Includes = new HashSet<string>();
        }

        public CodeManager(CodeManager codeManager)
        {
            CodeLines = new List<string>();
            Variables = new Dictionary<IVariable, VariableStatus>(codeManager.Variables);
            Includes = codeManager.Includes;
        }

        public virtual void AddLibrary(string library)
        {
            Includes.Add(library);
        }

        public virtual VariableStatus GetVariableStatus(IVariable variable)
        {
            Variables.TryGetValue(variable, out var status);
            return status;
        }

        public virtual void SetVariableStatus(IVariable variable, VariableStatus variableStatus)
        {
            Variables.TryAdd(variable, variableStatus);
        }

        public virtual void AddLine(string line)
        {
            CodeLines.Add(line);
        }

        public virtual void AddLines(List<string> lines, bool needBracket = false)
        {
            if (needBracket || lines.Count > 1)
            {
                AddLine("{");
            }

            lines.ForEach(line => AddLine("\t" + line));

            if (needBracket || lines.Count > 1)
            {
                AddLine("}");
            }
        }

        public string BuildParamCode(IEnumerable<IConnection> paramsList)
        {
            var codeParams = paramsList.Select(x => ((BaseIO)x.Connected).ParentNode.ToCodeParam(this));
            return string.Join(", ", codeParams);
        }

        public string BuildCode()
        {
            var builder = new StringBuilder();
            foreach (string include in Includes.Where(include => !_ignoreIncludes.Contains(include)))
            {
                builder.Append($"#include<{include}>\n");
            }
            builder.Append("\n");

            CodeLines.ForEach(s => builder.Append(s + "\n"));

            return builder.ToString();
        }
    }
}
