using System;
using System.Collections.Generic;
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
using Unity.Plastic.Newtonsoft.Json;

namespace Backend
{
    public class TemplateManager : ITemplatesManager
    {
        public List<ITemplate> Templates => _templates.Values.ToList();

        private Dictionary<long, ITemplate> _templates = new();
        private long _highestId = 500;
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

        private void TrySetNewId(long id)
        {
            if (id > _highestId)
            {
                _highestId = id;
            }
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
        private T LoadJsonFromFile<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(_fileSystem.File.ReadAllText(path));
        }

        private void LoadFunctionsFromFile()
        {
            var files = _fileSystem.Directory.GetFiles(MakePath(FunctionsDir));
            foreach (var file in files)
            {
                var json = LoadJsonFromFile<FunctionsJson>(file);
                foreach (var (id, functionJson) in json.Functions)
                {
                    TrySetNewId(id);
                    _templates.Add(id, new FunctionTemplate(id, json.Library, functionJson));
                }
            }
        }

        private void LoadMethodsFromFile()
        {
            var files = _fileSystem.Directory.GetFiles(MakePath(ClassesDir));
            foreach (var file in files)
            {
                var json = LoadJsonFromFile<ClassJson>(file);
                var className = json.ClassName;

                ClassTypeValidator.Instance.AddClassType(className);
                var classType = new ClassType(className);
                var library = json.Library;

                foreach (var (id, functionJson) in json.Methods)
                {
                    TrySetNewId(id);
                    _templates.Add(id, new ClassMethodTemplate(id, library, functionJson, classType));
                }

                foreach (var (id, list) in json.Constructors)
                {
                    TrySetNewId(id);
                    _templates.Add(id, new ClassConstructorTemplate(id, library, list, classType));
                }
            }
        }

        private void LoadBuildIn()
        {
            _templates.Add(1, new BuildInTemplate(1, "Input", typeof(InputNode)));
            _templates.Add(2, new BuildInTemplate(2, "If", typeof(IfNode)));
            _templates.Add(3, new BuildInTemplate(3, "While", typeof(WhileNode)));
            _templates.Add(4, new ReturnNodeTemplate(4));
            _templates.Add(5, new BuildInTemplate(5, "Break", typeof(BreakNode)));
            _templates.Add(6, new BuildInTemplate(6, "Continue", typeof(ContinueNode)));
            _templates.Add(7, new BuildInTemplate(7, "!", typeof(NotNode)));
            _templates.Add(8, new BuildInTemplate(8, "()", typeof(BracketNode)));

            var id = 9;
            foreach (CompareOpTemplate.ECompareOp op in Enum.GetValues(typeof(CompareOpTemplate.ECompareOp)))
            {
                _templates.Add(id, new CompareOpTemplate(id, op));
                id++;
            }

            foreach (LogicalOpTemplate.ELogicalOp op in Enum.GetValues(typeof(LogicalOpTemplate.ELogicalOp)))
            {
                _templates.Add(id, new LogicalOpTemplate(id, op));
                id++;
            }

            foreach (ArithmeticOpTemplate.EArithmeticOp op in Enum.GetValues(typeof(ArithmeticOpTemplate.EArithmeticOp)))
            {
                _templates.Add(id, new ArithmeticOpTemplate(id, op));
                id++;
            }
        }

        public virtual ITemplate GetTemplateById(long id)
        {
            return _templates[id];
        }
    }
}
