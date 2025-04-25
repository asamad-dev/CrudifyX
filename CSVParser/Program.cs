using System.Reflection;

namespace CSVParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var people = ParseCsv<Person>("people.csv");
            foreach (var person in people)
            {
                Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
            }

            //Test with a CSV file that has a different header order
            //var people = ParseCsv<Person>("people_header_error.csv");

            // Testfor invalid data types
            //var people = ParseCsv<Person>("people_invalid_data_types.csv");


            


            Console.ReadKey();
        }
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }


        public static List<T> ParseCsv<T>(string filePath) where T : new()
        {
            var result = new List<T>();
            using (var reader = new StreamReader(filePath))
            {
                var headerLine = reader.ReadLine();
                if (headerLine == null)
                    return result;

                var headers = headerLine.Split(',').Select(h => h.Trim().ToLower()).ToList();
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                           .ToDictionary(p => p.Name.ToLower(), p => p);

                var headerToProperty = headers
                    .Where(h => properties.ContainsKey(h))
                    .ToDictionary(h => h, h => properties[h]);

                if (headerToProperty.Count != properties.Count)
                    throw new Exception("CSV headers do not match all required properties.");


                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length != headers.Count)
                        continue;

                    var obj = new T();
                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (properties.TryGetValue(headers[i], out var property))
                        {
                            try
                            {
                                var value = Convert.ChangeType(values[i], property.PropertyType);
                                property.SetValue(obj, value);
                            }
                            catch
                            {
                                throw new Exception("Invalid data types.");
                            }
                        }
                    }
                    result.Add(obj);
                }
            }
            return result;
        }

    }
}
