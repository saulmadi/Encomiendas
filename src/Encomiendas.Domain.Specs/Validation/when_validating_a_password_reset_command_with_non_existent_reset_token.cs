using System;
using System.Collections.Generic;
using AcklenAvenue.Commands;
using Machine.Specifications;
using Moq;
using Encomiendas.Domain.Application.Commands;
using Encomiendas.Domain.Entities;
using Encomiendas.Domain.Exceptions;
using Encomiendas.Domain.Services;
using Encomiendas.Domain.Validators;
using Encomiendas.Domain.ValueObjects;

using It = Machine.Specifications.It;

namespace Encomiendas.Domain.Specs.Validation
{
    public class when_validating_a_password_reset_command_with_non_existent_reset_token
    {
        static ICommandValidator<ResetPassword> _validator;
        static readonly EncryptedPassword EncryptedPassword = new EncryptedPassword("password");
        static readonly Guid ResetPasswordToken = Guid.NewGuid();
        static List<ValidationFailure> _expectedFailures;
        static Exception _exception;
        static IReadOnlyRepository _readOnlyRepo;

        Establish context =
            () =>
            {
                _readOnlyRepo = Mock.Of<IReadOnlyRepository>();
                _validator = new PassowrdResetValidator(_readOnlyRepo, Mock.Of<ITimeProvider>());

                _expectedFailures = new List<ValidationFailure>
                                    {
                                        new ValidationFailure(
                                            "ResetPasswordToken",
                                            ValidationFailureType.DoesNotExist)
                                    };

                Mock.Get(_readOnlyRepo).Setup(x => x.GetById<PasswordResetAuthorization>(ResetPasswordToken))
                    .Throws(new ItemNotFoundException<PasswordResetAuthorization>(ResetPasswordToken));
            };

        Because of =
            () => _exception = Catch.Exception(() =>
                _validator.Validate(new VisitorSession(),
                    new ResetPassword(ResetPasswordToken, EncryptedPassword)));

        It should_return_expected_failures =
            () => ((CommandValidationException) _exception).ValidationFailures.ShouldBeLike(_expectedFailures);
    }
}