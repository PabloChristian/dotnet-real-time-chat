using Real.Time.Chat.Shared.Kernel.Commands;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Real.Time.Chat.Domain.Commands
{
    public abstract class GenericCommandResult<T> : ICommandResult<T>
    {
        protected GenericCommandResult() => ValidationResult = new ValidationResult();

        protected ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();

        public virtual IList<ValidationFailure> GetErrors() => ValidationResult.Errors;
    }
}
