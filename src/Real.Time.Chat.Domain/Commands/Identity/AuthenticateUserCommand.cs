using Real.Time.Chat.Domain.Commands.User;
using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Domain.CommandHandlers
{
    public class AuthenticateUserCommand : LoginCommand<TokenJWT>
    {
        public override bool IsValid()
        {
            ValidationResult = new AuthenticateUserValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class AuthenticateUserValidator : LoginValidator<AuthenticateUserCommand>
        {
            protected override void StartRules()
            {
                base.StartRules();
            }
        }
    }
}
