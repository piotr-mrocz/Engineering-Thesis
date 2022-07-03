using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace IntranetWebApi.Application.Extensions;

public static class WebApplicationExtensions
{
    public static void AddWebApplicationExtensions(WebApplication app)
    {
        app.MapHub<MessageHubClient>("/conversation");
    }
}
