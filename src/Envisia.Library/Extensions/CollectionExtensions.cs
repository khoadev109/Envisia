using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;

namespace Envisia.Library.Extensions
{
    public static class CollectionExtensions
    {
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
        }

        public static List<T> Duplicates<T>(this IEnumerable<T> source)
        {
            return source.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();
        }

        public static string Join<T>(this IEnumerable<T> source, string seperator)
        {
            return string.Join(seperator, source);
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            foreach (var t in target)
            {
                if (source.Contains(t))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static async Task AsyncParallelForEach<T>(this IAsyncEnumerable<T> source, Func<T, Task> body, int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            if (scheduler != null)
            {
                options.TaskScheduler = scheduler;
            }

            var block = new ActionBlock<T>(body, options);

            await foreach (var item in source)
            {
                block.Post(item);
            }

            block.Complete();

            await block.Completion;
        }

    }
}
