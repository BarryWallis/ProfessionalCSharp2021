namespace WhatsNewAttributes;

/// <summary>
/// Attribute marking when an item was last modified.
/// </summary>
[AttributeUsage(AttributeTargets.Class
                | AttributeTargets.Method
                | AttributeTargets.Constructor
                | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public class LastModifiedAttribute : Attribute
{
    /// <summary>
    /// The date and time the item was modified.
    /// </summary>
    public DateTime DateModified { get; }

    /// <summary>
    /// A description of the changes.
    /// </summary>
    public string Changes { get; }

    /// <summary>
    /// Any outstanding issues. 
    /// </summary>
    public string Issues { get; set; } = string.Empty;

    /// <summary>
    /// Create a new LastModifiedAttribute.
    /// </summary>
    /// <param name="dateModified">The date the item was modified.</param>
    /// <param name="changes">A description of the changes.</param>
    public LastModifiedAttribute(string dateModified, string changes)
    {
        DateModified = DateTime.Parse(dateModified);
        Changes = changes;
    }
}

/// <summary>
/// Attribute to mark an assembly that contains change documentation.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class SupportsWhatsNewAttribute : Attribute
{

}
