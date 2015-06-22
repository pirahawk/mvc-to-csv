using System;

namespace MvcToCsv
{
    /// <summary>
    /// Marks the assigned field to be ignored when generating the csv file
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CsvIgnoreAttribute : Attribute
    {
        public bool Ignore { get; private set; }

        public CsvIgnoreAttribute(bool ignore)
        {
            Ignore = ignore;
        }
    }
}