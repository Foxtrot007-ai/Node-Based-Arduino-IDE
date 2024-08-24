using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using Backend.API;
using Backend.Node;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Template.Json;
using Backend.Type;
using Backend.Validator;
using Newtonsoft.Json;

namespace Backend
{
    public class TemplateManager : ITemplatesManager
    {
        public List<ITemplate> Templates => _templates.Values.ToList();

        private Dictionary<PathName, ITemplate> _templates = new();
        protected string _tempalateDir;
        private const string FunctionsDir = "functions";
        private const string ClassesDir = "classes";
        private readonly IFileSystem _fileSystem;

        protected TemplateManager(IFileSystem fileSystem, string dir)
        {
            _fileSystem = fileSystem;
            _tempalateDir = dir;
            LoadBuildIn();
            LoadMethodsFromFile(); // Methods must be first, will create available class names
            LoadFunctionsFromFile();
        }
        public TemplateManager() : this(new FileSystem(), Environment.CurrentDirectory + "/Templates")
        {
        }

        private string MakePath(params string[] dirs)
        {
            var path = new StringBuilder();
            path.Append(_tempalateDir);
            foreach (var dir in dirs)
            {
                path.Append("/");
                path.Append(dir);
            }

            return path.ToString();
        }

        private void AddTemplate(BaseTemplate template)
        {
            _templates.Add(template.PathName, template);
        }
        
        private T LoadJsonFromFile<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(_fileSystem.File.ReadAllText(path));
        }

        private string[] GetFilesOrCreateDir(string dir)
        {
            var path = MakePath(dir);
            if (_fileSystem.Directory.Exists(path)) return _fileSystem.Directory.GetFiles(path);

            _fileSystem.Directory.CreateDirectory(path);
            return Array.Empty<string>();
        }

        private void LoadFunctionsFromFile()
        {
            var files = GetFilesOrCreateDir(FunctionsDir);
            foreach (var file in files)
            {
                var json = LoadJsonFromFile<FunctionsJson>(file);
                foreach (var (id, functionJson) in json.Functions)
                {
                    AddTemplate(new FunctionTemplate(id, json.Library, functionJson));
                }
            }
        }

        private void LoadMethodsFromFile()
        {
            var files = GetFilesOrCreateDir(ClassesDir);
            foreach (var file in files)
            {
                var json = LoadJsonFromFile<ClassJson>(file);
                var className = json.ClassName;

                ClassTypeValidator.Instance.AddClassType(className);
                var classType = new ClassType(className);
                var library = json.Library;

                foreach (var (id, functionJson) in json.Methods)
                {
                    AddTemplate(new ClassMethodTemplate(id, library, functionJson, classType));
                }

                foreach (var (id, list) in json.Constructors)
                {
                    AddTemplate(new ClassConstructorTemplate(id, library, list, classType));
                }
            }
        }

        private void LoadBuildIn()
        {
            List<BuildInTemplate> buildInTemplates = new()
            {
                new BuildInTemplate(1, "Input", typeof(InputNode)),
                new BuildInTemplate(2, "If", typeof(IfNode)),
                new BuildInTemplate(3, "While", typeof(WhileNode)),
                new ReturnNodeTemplate(4),
                new BuildInTemplate(5, "Break", typeof(BreakNode)),
                new BuildInTemplate(6, "Continue", typeof(ContinueNode)),
                new BuildInTemplate(7, "!", typeof(NotNode)),
                new BuildInTemplate(8, "()", typeof(BracketNode))
            };
            int id = buildInTemplates.Count + 1;
            
            foreach (ArithmeticOpTemplate.EArithmeticOp op in Enum.GetValues(typeof(ArithmeticOpTemplate.EArithmeticOp)))
            {
                buildInTemplates.Add(new ArithmeticOpTemplate(id, op));
                id++;
            }
            
            foreach (CompareOpTemplate.ECompareOp op in Enum.GetValues(typeof(CompareOpTemplate.ECompareOp)))
            {
                buildInTemplates.Add(new CompareOpTemplate(id, op));
                id++;
            }

            foreach (LogicalOpTemplate.ELogicalOp op in Enum.GetValues(typeof(LogicalOpTemplate.ELogicalOp)))
            {
                buildInTemplates.Add(new LogicalOpTemplate(id, op));
                id++;
            }
         
            buildInTemplates.ForEach(AddTemplate);
        }

        public virtual ITemplate GetTemplateByPn(PathName pathName)
        {
            return _templates[pathName];
        }
    }
}
