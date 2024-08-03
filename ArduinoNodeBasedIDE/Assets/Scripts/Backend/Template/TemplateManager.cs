using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using Backend.API;
using Backend.API.DTO;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Template.Json;
using Backend.Type;
using Backend.Validator;
using Castle.Core.Internal;
using Unity.Plastic.Newtonsoft.Json;

namespace Backend
{
    public class TemplateManager : ITemplatesManager
    {
        public List<ITemplate> Templates => _templates.Values.ToList();

        private Dictionary<long, ITemplate> _templates = new();
        private Dictionary<string, FunctionsJson> _functions = new();
        private Dictionary<string, ClassJson> _classes = new();
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
                _functions.Add(json.Library, json);
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

                _classes.Add(className, json);
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
            _templates.Add(1, new BuildInTemplate(1, "If", typeof(IfNode)));
            _templates.Add(2, new BuildInTemplate(2, "While", typeof(WhileNode)));
            _templates.Add(3, new BuildInTemplate(3, "Return", typeof(ReturnNode)));
        }

        public void AddFunctionTemplate(FunctionTemplateDto functionTemplateDto)
        {
            if (!functionTemplateDto.IsDtoValid())
            {
                throw new Exception();
            }

            _highestId++;
            var library = functionTemplateDto.Library;
            if (!_functions.TryGetValue(library, out var functionsJson))
            {
                functionsJson = new FunctionsJson
                {
                    Library = library,
                };
                _functions.Add(library, functionsJson);
            }

            var function = new FunctionJson(functionTemplateDto);
            functionsJson.Functions.Add(_highestId, function);

            _templates.Add(_highestId, new FunctionTemplate(_highestId, functionTemplateDto));
            WriteJsonToFile(MakePath(FunctionsDir, library), functionsJson);
        }

        public void AddClassMethodTemplate(string className, FunctionTemplateDto functionTemplateDto)
        {
            if (!functionTemplateDto.IsDtoValid())
            {
                throw new Exception();
            }

            if (!ClassTypeValidator.Instance.IsClassType(className))
            {
                ClassTypeValidator.Instance.AddClassType(className);
            }

            _highestId++;
            if (!_classes.TryGetValue(className, out var classJson))
            {
                classJson = new ClassJson
                {
                    Library = functionTemplateDto.Library,
                };
                _classes.Add(className, classJson);
            }

            var method = new FunctionJson(functionTemplateDto);
            classJson.Methods.Add(_highestId, method);

            var classType = new ClassType(className);
            _templates.Add(_highestId, new ClassMethodTemplate(_highestId, functionTemplateDto, classType));
            WriteJsonToFile(MakePath(ClassesDir, className), classJson);
        }

        public void AddClassConstructorTemplate(string className, string library, List<string> inputs)
        {
            if (library.IsNullOrEmpty()
                || inputs.TrueForAll(x => !x.IsNullOrEmpty() && x != "void"))
            {
                throw new Exception();
            }

            if (!ClassTypeValidator.Instance.IsClassType(className))
            {
                ClassTypeValidator.Instance.AddClassType(className);
            }

            _highestId++;
            if (!_classes.TryGetValue(className, out var classJson))
            {
                classJson = new ClassJson
                {
                    Library = library,
                };
                _classes.Add(className, classJson);
            }

            classJson.Constructors.Add(_highestId, inputs);

            var classType = new ClassType(className);
            _templates.Add(_highestId, new ClassConstructorTemplate(_highestId, library, inputs, classType));
            WriteJsonToFile(MakePath(ClassesDir, className), classJson);
        }

        private void WriteJsonToFile<T>(string filePath, T json)
        {
            _fileSystem.File.WriteAllText(filePath + ".json", JsonConvert.SerializeObject(json));
        }

        public virtual ITemplate GetTemplateById(long id)
        {
            return _templates[id];
        }
    }
}
