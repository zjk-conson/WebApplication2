using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class MyExceptionFilter:IExceptionFilter
    {
        private TestService testService;

        public MyExceptionFilter(TestService testService) {
            this.testService = testService;
        }
        public void OnException(ExceptionContext context)
        {
            string ddd =  testService.Hello();
        }
    }
}
