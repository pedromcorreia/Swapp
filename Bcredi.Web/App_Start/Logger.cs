namespace Bcredi.Web
{
    public class Logger
    {
        //readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static log4net.ILog logger = null;

        public static log4net.ILog getLogger()
        {
            if (logger == null)
            {
                logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return logger;
            }

            return logger;
        }
    }
}