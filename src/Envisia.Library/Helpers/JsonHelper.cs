using Newtonsoft.Json;

namespace Envisia.Library.Helpers
{
    public class JsonHelper
    {
        private readonly string _path;

        private static readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public JsonHelper(string path)
        {
            _path = PathHelper.Get(path);

            PathHelper.Ensure(_path);
        }
        
        public T Read<T>() where T: class
        {
            T result = null;

            using StreamReader streamReader = new StreamReader(_path);
            using (JsonReader reader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = serializer.Deserialize<T>(reader);
            }

            return result;
        }

        public void AddToList<T>(T item) where T: class
        {
            var list = Read<List<T>>() ?? new List<T>();
            
            if (list.Contains(item))
            {
                list.Remove(item);
            }

            list.Add(item);

            UpdateAll<List<T>>(list);
        }

        public void RemoveFromList<T>(Predicate<T> match) where T: class
        {
            var list = Read<List<T>>() ?? new List<T>();
            list.RemoveAll(match);

            UpdateAll<List<T>>(list);
        }

        public void UpdateAll<T>(T content) where T: class
        {
            _readWriteLock.EnterWriteLock();

            try
            {
                using StreamWriter sw = new StreamWriter(_path);
                using JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                var serializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                serializer.Serialize(writer, content);
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }
    }
}
