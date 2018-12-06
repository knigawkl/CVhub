using Microsoft.AspNetCore.Mvc;

namespace CVhub.Controllers
{
    public class HttpStatusCodeResult : ActionResult
    {
        private object badRequest;

        public HttpStatusCodeResult(object badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}
