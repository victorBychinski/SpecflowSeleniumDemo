using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class WebDriverPRovider
    {
        private IConfiguration _configuration;

        public WebDriverPRovider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
