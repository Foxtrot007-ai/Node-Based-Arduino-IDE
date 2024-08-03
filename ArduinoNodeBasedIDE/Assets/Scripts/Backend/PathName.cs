namespace Backend
{
    public class PathName
    {
        private string _path;

        public PathName(string path)
        {
            _path = path;
        }

        public PathName(PathName parent, string className, long id = 1)
        {
            _path = parent + "/" + className + "-" + id;
        }
        
        public long GetId()
        {
            return long.Parse(GetFirstPath()._path.Split("-")[1]);
        }

        public string GetClassName()
        {
            return _path.Split("-")[0];
        }
        
        public PathName GetFirstPath()
        {
            var split = _path.Split("/");
            return split.Length == 1 ? this : new PathName(split[0]);
        }

        public PathName GetNextPath()
        {
            var index = _path.IndexOf("/");
            return index == -1 ? null : new PathName(_path[(index + 1)..]);
        }
        
        public override string ToString()
        {
            return _path;
        }
    }
}
