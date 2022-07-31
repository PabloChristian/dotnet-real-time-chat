using FluentValidation;
using Real.Time.Chat.Domain.Commands;

namespace Real.Time.Chat.Domain.CommandHandlers
{
    public class LogoutCommand<TResult> : GenericCommandResult<TResult>
    {
        public string UserName { get; set; } = string.Empty;

        public override bool IsValid() => throw new NotImplementedException();

        internal class LogoutValidator<T> : AbstractValidator<T> where T : LogoutCommand<TResult>
        {
            public LogoutValidator() => StartRules();

            protected virtual void StartRules()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .WithMessage(Properties.Resources.User_UserName_Required);
            }
        }
    }
}
