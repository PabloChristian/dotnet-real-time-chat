using FluentValidation;

namespace Real.Time.Chat.Domain.Commands.User
{
    public class LoginCommand<TResult> : GenericCommandResult<TResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }

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
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("The email is required.")
                    .EmailAddress().WithMessage("A valid email address is required.");

                RuleFor(x => x.Password).NotEmpty()
                    .WithMessage("The password is required");

                //RuleFor(x => x.Password)
                //    .Custom((x, context) =>
                //    {
                //        if (x.Length < 6)
                //            context.AddFailure("Password must have minimum 6 characters");
                //    });
            }
        }
    }
}