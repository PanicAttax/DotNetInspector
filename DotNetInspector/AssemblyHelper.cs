using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetInspector
{
    // This class contains one static method for correct receipt of a Assembly instance with an assembly file path.
    class AssemblyHelper
    {
        public static Assembly GetAssembly(string path)
        {
            try
            {
                return Assembly.LoadFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loaded file doesn't contain assembly", "Wrong file");
                return null;
            }
        }


    }
}
