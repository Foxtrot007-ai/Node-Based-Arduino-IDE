using System.Collections.Generic;
using System.Linq;

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

        public virtual void AddLines(List<string> lines)
        {
            if (lines.Count > 1)
            {
                AddLine("{");    
            }

            lines.ForEach(AddLine);
            
            if (lines.Count > 1)
            {
                AddLine("}");    
            }
        }
    }
}
