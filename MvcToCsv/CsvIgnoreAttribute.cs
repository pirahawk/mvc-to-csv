using System;

namespace MvcToCsv
{
    /// <summary>
    /// Marks the assigned field to be ignored when generating the csv file
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CsvIgnoreAttribute : Attribute
    {
        /// <summary>
        /// Flag which indicates if the property should be ignored from scaffolding within the Csv file
        /// </summary>
        public bool Ignore { get; private set; }

        public CsvIgnoreAttribute(bool ignore)
        {
            Ignore = ignore;
        }
    }
}