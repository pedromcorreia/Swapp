using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace Bcredi.Web.Tests.Controllers
{
    public class HttpContextManager
    {
        private static HttpContextBase m_context;
        public static HttpContextBase Current
        {
            get
            {
                if (m_context != null)
                    return m_context;

                if (HttpContext.Current == null)
                    throw new InvalidOperationException("HttpContext not available");

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public static void SetCurrentContext(HttpContextBase context)
        {
            m_context = context;
        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://stackoverflow/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }
}
