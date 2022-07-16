using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace IntranetWebApi;

public class TaskHubClient : Hub<ITaskHubClient>
{
}
