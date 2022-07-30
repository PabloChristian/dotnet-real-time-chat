using FluentValidation.Results;
using Real.Time.Chat.Shared.Kernel.Commands;

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
