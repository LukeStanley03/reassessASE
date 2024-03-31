using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    /// <summary>
    /// contains the parameter list and method body
    /// </summary>
    public class MethodDefinition
    {
        public string Name { get; }
        public List<string> Parameters { get; }
        public List<string> Body { get; }

        public MethodDefinition(string name, List<string> parameters, List<string> body)
        {
            Name = name;
            Parameters = parameters ?? new List<string>();
            Body = body;
        }
    }
}
