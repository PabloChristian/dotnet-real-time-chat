using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Domain.CommandHandlers
{
    public class LogoutUserCommand : LogoutCommand<TokenJWT>
    {
        public override bool IsValid()
        {
            ValidationResult = new LogoutUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class LogoutUserCommandValidator : LogoutValidator<LogoutUserCommand>
        {
            protected override void StartRules() => base.StartRules();
        }
    }
}
