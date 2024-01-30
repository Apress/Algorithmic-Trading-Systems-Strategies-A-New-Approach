using System.Reflection;
using Microsoft.Extensions.Options;

namespace ProcessBot;

public class ProcessBotOptions: IOptions<ProcessBotOptions>
{
    public string? DbConnectionString { get; set; }
    
    public ProcessBotOptions Value
    {
        get { return this; }
    }
}