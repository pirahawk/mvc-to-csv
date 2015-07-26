#mvc-to-csv
An MVC extension library that can be used to serialize a list of POCO's (your model) to a CSV file which can be returned via an action result

##How To Import
The library is available via [MvcToCsv on nuget](https://www.nuget.org/packages/MvcToCsv/). To import the library, simply install the following nuget package:

```
Install-Package MvcToCsv
```

##Usage
To serialize a collection of `POCO` using `MvcToCsv`

```
public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
```

Assume we have the following _Mvc Controller_

```
 public class HomeController : Controller
    {
        public ActionResult CreateCsv()
        {
            var models = Enumerable.Range(1, 3).Select(i => new Person
            {
                Id = i,
                FirstName = "Foo",
                LastName = "Bar",
            });

            return new CsvResult<Person>("People.csv", models);
        }
    }
```

##Specifying Csv Column Names
Certain fields can be decorated with the _CsvColumnNameAttribute_ which will ensure a field gets serialized with the custom column name

```
public class Person
    {
        public int Id { get; set; }
        
        [CsvColumnName("First Name")]
        public string FirstName { get; set; }

        [CsvColumnName("Last Name")]
        public string LastName { get; set; }
    }
```

##Omitting fields from serialization
You can omit a field from being serialized in the output by decorating it with the _CsvIgnoreAttribute_

```
public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [CsvIgnore(true)]
        public string Password { get; set; }
    }
```

##Formatting the serialized value
If a Type of a field within your model supports custom formatting, you can use the _CsvFormatAttribute_ to specify a format for the serialized value

```
 public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [CsvFormat("dd-MM-yyyy")]
        public DateTimeOffset? WhenCreated { get; set; }
    }
```