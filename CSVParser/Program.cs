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

            var products = ParseCsv<Product>("products.csv");
            foreach (var p in products)
                Console.WriteLine($"Product: {p.ProductName}, Price: {p.Price}, Stock: {p.Stock}");

            var employees = ParseCsv<Employee>("employees.csv");
            foreach (var e in employees)
                Console.WriteLine($"Employee: {e.FirstName} {e.LastName}, Salary: {e.Salary}");

            var students = ParseCsv<Student>("students.csv");
            foreach (var s in students)
                Console.WriteLine($"Student: {s.Name}, Grade: {s.Grade}, GPA: {s.GPA}");

            Console.ReadKey();
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

                // Check if all required properties are present
                var missingProperties = properties.Keys.Except(headerToProperty.Keys).ToList();
                if (missingProperties.Any())
                {
                    throw new Exception($"CSV missing required columns: {string.Join(", ", missingProperties)}");
                }

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length != headers.Count)
                        // Ignoring rows with missing values. 
                        continue;

                    var obj = new T();
                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (headerToProperty.TryGetValue(headers[i], out var property))
                        {
                            try
                            {
                                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                if (string.IsNullOrWhiteSpace(values[i]))
                                {
                                    property.SetValue(obj, GetDefault(targetType));
                                }
                                else
                                {
                                    var convertedValue = Convert.ChangeType(values[i], targetType);
                                    property.SetValue(obj, convertedValue);
                                }
                            }
                            catch
                            {
                                property.SetValue(obj, GetDefault(property.PropertyType));
                            }
                        }
                    }
                    result.Add(obj);
                }
            }
            return result;
        }

        private static object? GetDefault(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Product
        {
            public string ProductName { get; set; }
            public double Price { get; set; }
            public int Stock { get; set; }
        }

        public class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Salary { get; set; }
        }

        public class Student
        {
            public string Name { get; set; }
            public int Grade { get; set; }
            public double GPA { get; set; }
        }

    }
}
