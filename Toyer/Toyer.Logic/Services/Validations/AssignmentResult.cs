namespace Toyer.Logic.Services.Validations;

/// <summary>
/// This class serves as a validation indicator for database persistence during entity assignment operations.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
public class AssignmentResult<TEntity, TId> 
{
    
    public TEntity? Entity { get; }
    private readonly ICollection<TId>? _nonExistentIds;
    //ids that have already assigned TEntity member
    private readonly ICollection<TId>? _duplicationIds;

    public bool IsValid => Validate();
    public string ClientErrorMessage => GetErrorMessage();

    /// <summary>
    /// Use when TEntity instance was not found in the database.
    /// </summary>
    public AssignmentResult(){}

    /// <summary>
    /// Use when TEntity has been found, and there are no obstacles to proceed with assigning it in the relationship.
    /// </summary>
    public AssignmentResult(TEntity? entity)
    {
        Entity = entity;
    }

    /// <summary>
    /// Used when trying to assign an entity with an existing ID within a specific database relationship,
    /// or
    /// when attempting to assign a non-existing entity within a specific relationship.
    /// </summary>
    public AssignmentResult(ICollection<TId>? nonExistentIds, ICollection<TId>? duplicationIds)
    {
        _nonExistentIds = nonExistentIds;
        _duplicationIds = duplicationIds;
    }

    private string GetErrorMessage()
    {
        if (_nonExistentIds != null) return $"ID/s: [{string.Join(", ", _nonExistentIds)}] to be assigned not found";
        if (_duplicationIds != null) return $"ID/s: [{string.Join(", ", _duplicationIds!)}] has/have a member of given {typeof(TEntity).Name}";
        if (Entity == null) return $"{typeof(TEntity).Name} not found.";
        else return "No error to display";

    }

    private bool Validate()
    {
        return _nonExistentIds == null && _duplicationIds == null && Entity != null;
    }

}

