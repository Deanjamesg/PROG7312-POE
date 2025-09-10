namespace PROG7312_Web_App.Data
{
    public class ReportList
    {
        public ReportNode Head;
        public ReportNode Tail;

        public ReportList()
        {
            Head = null;
            Tail = null;
        }

        public void Add(ReportNode newReport)
        {
            if (Head == null)
            {
                Head = newReport;
                Tail = newReport;
            }
            else
            {
                newReport.Previous = Tail;
                Tail.Next = newReport;
                Tail = newReport;
            }
        }

    }
}
