using ChristianJodi.Business;

namespace ChristianJodi.Services
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