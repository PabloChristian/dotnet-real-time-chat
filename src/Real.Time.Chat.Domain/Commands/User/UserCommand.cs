using FluentValidation;

namespace Real.Time.Chat.Domain.Commands.User
{
    public class UserCommand<TResult> : GenericCommandResult<TResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SecondPassword { get; set; } = string.Empty;

        public UserCommand() { }
        public UserCommand(string name, string email, string password, string secondPassword)
        {
            Name = name;
            Email = email;
            Password = password;
            SecondPassword = secondPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserValidator<UserCommand<TResult>>().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class UserValidator<T> : AbstractValidator<T> where T : UserCommand<TResult>
        {
            public UserValidator() => StartRules();

            protected virtual void StartRules()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .NotNull().WithMessage("The name is required.");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("The email is required.")
                    .EmailAddress().WithMessage("A valid email address is required.");

                RuleFor(x => x)
                    .Custom((x, context) =>
                    {
                        if (string.IsNullOrEmpty(x.Password))
                            context.AddFailure("A password is required");

                        if(string.IsNullOrEmpty(x.SecondPassword))
                            context.AddFailure("Repeat the password");

                        if (x.Password.Length < 6)
                            context.AddFailure("The password must have minimum 6 characters");

                        if(!string.IsNullOrEmpty(x.Password) && !string.IsNullOrEmpty(x.SecondPassword))
                            if(!x.Password.Equals(x.SecondPassword))
                                context.AddFailure("The passwords are not equals");
                    });
            }
        }
    }
}
