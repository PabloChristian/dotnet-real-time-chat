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
                    .WithMessage(Properties.Resources.User_Name_Required);

                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .WithMessage(Properties.Resources.User_UserName_Required);

                RuleFor(x => x)
                    .Custom((x, context) =>
                    {
                        if (string.IsNullOrEmpty(x.Password))
                            context.AddFailure(Properties.Resources.User_Password_Required);

                        if(string.IsNullOrEmpty(x.SecondPassword))
                            context.AddFailure(Properties.Resources.User_RepeatPassword_Required);

                        if (x.Password.Length < 6)
                            context.AddFailure(Properties.Resources.User_Password_MinimumLength);

                        if(!string.IsNullOrEmpty(x.Password) && !string.IsNullOrEmpty(x.SecondPassword) 
                            && !x.Password.Equals(x.SecondPassword))
                                context.AddFailure(Properties.Resources.User_Password_NotEqual);
                    });
            }
        }
    }
}
