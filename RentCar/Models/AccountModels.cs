using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace RentCar.Models
{

    #region Models

    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "ConfirmPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "ErrorConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(MyResources.Resource), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }


    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(MyResources.Resource), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "ErrorConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region Services
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountLoginController
    // code unit testable.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(MyResources.Resource.ValueRequired, "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException(MyResources.Resource.ValueRequired, "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(MyResources.Resource.ValueRequired, "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException(MyResources.Resource.ValueRequired, "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException(MyResources.Resource.ValueRequired, "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(MyResources.Resource.ValueRequired, "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException(MyResources.Resource.ValueRequired, "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException(MyResources.Resource.ValueRequired, "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException(MyResources.Resource.ValueRequired, "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return MyResources.Resource.DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return MyResources.Resource.DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return MyResources.Resource.InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return MyResources.Resource.InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return MyResources.Resource.InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return MyResources.Resource.InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return MyResources.Resource.InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return MyResources.Resource.ProviderError;

                case MembershipCreateStatus.UserRejected:
                    return MyResources.Resource.UserRejected;

                default:
                    return MyResources.Resource.UnknownError;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]{
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), _minCharacters, int.MaxValue)
            };
        }
    }
    #endregion

}
