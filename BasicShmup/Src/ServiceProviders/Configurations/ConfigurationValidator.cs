using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BasicShmup.ServiceProviders.Configurations;

public class ConfigurationValidator
{
    public IEnumerable<ConfiguraitonValidationError> ValidateConfiguration(IConfiguration configuration)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(configuration);
        Validator.TryValidateObject(configuration, validationContext, validationResults, validateAllProperties: true);

        return validationResults.Select(validationResult =>
            new ConfiguraitonValidationError(
                $"Invalid configuration of type {configuration.GetType()} - {validationResult.ErrorMessage}"
            )
        );
    }
}
