namespace MvcToCsv
{
    /// <summary>
    /// Responsible for persisting a row to the csv report
    /// </summary>
    public interface IReportWriter
    {
        void WriteLine(string row);
    }
}