using FluentValidation;

namespace Real.Time.Chat.Domain.Commands.User
{
    public class LoginCommand<TResult> : GenericCommandResult<TResult>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        internal class LoginValidator<T> : AbstractValidator<T> where T : LoginCommand<TResult>
        {
            public LoginValidator()
            {
                StartRules();
            }

            protected virtual void StartRules()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .WithMessage("The username is required.");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("The password is required");
            }
        }
    }
}