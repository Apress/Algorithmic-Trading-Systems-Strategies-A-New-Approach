using Microsoft.Extensions.Options;

namespace Backworker;

public class BackworkerOptions: IOptions<BackworkerOptions>
{
    public string? DbConnectionString { get; set; }
    
    public BackworkerOptions Value
    {
        get { return this; }
    }
}