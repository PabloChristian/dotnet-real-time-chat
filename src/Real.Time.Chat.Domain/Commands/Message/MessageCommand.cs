using FluentValidation;

namespace Real.Time.Chat.Domain.Commands.Message
{
    public class MessageCommand<TResult> : GenericCommandResult<TResult>
    {
        public string Message { get; set; } = string.Empty;
        public string Consumer { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;

        public override bool IsValid()
        {
            ValidationResult = new MessageValidator<MessageCommand<TResult>>().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class MessageValidator<T> : AbstractValidator<T> where T : MessageCommand<TResult>
        {
            public MessageValidator()
            {
                StartRules();
            }

            protected virtual void StartRules()
            {
                RuleFor(x => x.Sender)
                    .NotEmpty()
                    .NotNull().WithMessage("The sender is required.");

                RuleFor(x => x.Message)
                    .NotEmpty()
                    .NotNull().WithMessage("The message is required.");
            }
        }
    }
}
