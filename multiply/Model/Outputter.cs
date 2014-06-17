using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiply.Model
{
    public interface IOutput
    {
        void OutputGrid();
    }

    public class HtmlOutputter : IOutput
    {
        public void OutputGrid()
        {
            throw new NotImplementedException();
        }
    }

    public class CvsOutputter : IOutput
    {

        public void OutputGrid()
        {
            throw new NotImplementedException();
        }
    }

    public class ConsoleOutputter : IOutput
    {

        public void OutputGrid()
        {
            throw new NotImplementedException();
        }
    }
}
