using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BasicShmup.Extensions;
using Godot;

namespace BasicShmup.Validations;

public class ResourceValidator
{
    private static readonly List<string> IgnoredSuffixesList = [
        ".godot/",
        "Src/",
        ".svg",
        ".png",
        ".cs"
    ];

    private Stack<ResourcePath> _resourcePaths = new();
    private List<ResourceValidationError> _errors = [];

    public void Run()
    {
        _resourcePaths = new Stack<ResourcePath>(["res://"]);
        _errors = [];

        GD.Print("Validating resource files...");

        while (_resourcePaths.Count > 0)
        {
            var resourcePath = _resourcePaths.Pop();
            if (ShouldIgnore(resourcePath))
                continue;

            if (IsDirectory(resourcePath))
                AddDirectory(resourcePath);
            else
                ValidateResourceAt(resourcePath);
        }

        if (_errors.Count > 0)
            throw new ValidationException(AggregateErrors());

        GD.Print("Validation successful!");
    }

    private string AggregateErrors()
    {
        var errorStringBuilder = new StringBuilder();

        errorStringBuilder.AppendLine("Encountered validation error(s)");

        for (var i = 0; i < _errors.Count; i++)
        {
            var error = _errors[i];

            errorStringBuilder.Append('\t');
            errorStringBuilder.Append(i + 1);
            errorStringBuilder.Append(") ");
            errorStringBuilder.Append(error.ResourcePath);
            errorStringBuilder.Append(" - ");
            errorStringBuilder.AppendLine(error.ErrorMessage);
        }

        return errorStringBuilder.ToString();
    }

    private static bool ShouldIgnore(ResourcePath resourcePath)
    {
        return IgnoredSuffixesList.Any(resourcePath.Path.EndsWith);
    }

    private static bool IsDirectory(ResourcePath resourcePath)
    {
        return resourcePath.Path.EndsWith('/');
    }

    private void AddDirectory(ResourcePath directoryPath)
    {
        foreach (var resourcePath in ResourceLoader.ListDirectory(directoryPath))
            _resourcePaths.Push(directoryPath.Join(resourcePath));
    }

    private void ValidateResourceAt(ResourcePath resourcePath)
    {
        var resource = ResourceLoader.Load(resourcePath);

        if (resource is PackedScene packedScene)
            ValidateScene(resourcePath, packedScene);

        Validate(resourcePath, resource);
    }

    private void ValidateScene(ResourcePath resourcePath, PackedScene packedScene)
    {
        var sceneRoot = packedScene.Instantiate();
        foreach (var sceneNode in sceneRoot.GetNodesInSubTree())
        {
            Validate(resourcePath, sceneNode);
        }

        sceneRoot.Free();
    }

    private void Validate(ResourcePath resourcePath, object o)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(o);
        Validator.TryValidateObject(o, validationContext, validationResults, validateAllProperties: true);

        var validationErrors = validationResults.Select(validationResult =>
            new ResourceValidationError(
                resourcePath,
                $"Invalid object of type {o.GetType()} - {validationResult.ErrorMessage}"
            )
        );

        _errors.AddRange(validationErrors);
    }
}
