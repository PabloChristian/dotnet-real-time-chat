using FluentValidation;

namespace Real.Time.Chat.Domain.Commands.User
{
    public class UserCommand<TResult> : GenericCommandResult<TResult>
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SecondPassword { get; set; } = string.Empty;

        public UserCommand() { }
        public UserCommand(string name, string username, string password, string secondPassword)
        {
            Name = name;
            UserName = username;
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
                    .NotNull()
                    .WithMessage("The name is required.");

                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .WithMessage("The username is required.");

                RuleFor(x => x)
                    .Custom((x, context) =>
                    {
                        if (string.IsNullOrEmpty(x.Password))
                            context.AddFailure("A password is required");

                        if(string.IsNullOrEmpty(x.SecondPassword))
                            context.AddFailure("Repeat the password");

                        if (x.Password.Length < 6)
                            context.AddFailure("The password must have minimum of 6 characters");

                        if(!string.IsNullOrEmpty(x.Password) && !string.IsNullOrEmpty(x.SecondPassword) 
                            && !x.Password.Equals(x.SecondPassword))
                                context.AddFailure("The passwords are not equal");
                    });
            }
        }
    }
}
