using Matri.Business;

namespace Matri.Services
{
    public class CloseApplication: ICloseApplication
    {
        [Obsolete]
        public void closeApplication()
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}