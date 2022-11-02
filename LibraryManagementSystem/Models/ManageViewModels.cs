using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LibraryManagementSystem.Models
{
	public class IndexViewModel
	{
		public bool HasPassword { get; set; }
		public IList<UserLoginInfo> Logins { get; set; }
		public string PhoneNumber { get; set; }
		public bool TwoFactor { get; set; }
		public bool BrowserRemembered { get; set; }
	}

	public class ManageLoginsViewModel
	{
		public IList<UserLoginInfo> CurrentLogins { get; set; }
		public IList<AuthenticationDescription> OtherLogins { get; set; }
	}

	public class FactorViewModel
	{
		public string Purpose { get; set; }
	}

	public class SetPasswordViewModel
	{
		[Required]
		[StringLength(100, ErrorMessage = "كلمة المرور يجب ان تتكون من 6 محارف على الأقل.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "كلمة المرور الجديدة")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "تأكيد كلمة المرور")]
		[Compare("NewPassword", ErrorMessage = "كلمتا المرور غير متطابقتين")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangePasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "كلمة المرور الحالية")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "كلمة المرور يجب ان تتكون من 6 محارف على الأقل.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "كلمة المرور الجديدة")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "تأكيد كلمة المرور")]
		[Compare("NewPassword", ErrorMessage = "كلمتا المرور غير متطابقتين")]
		public string ConfirmPassword { get; set; }
	}

	public class AddPhoneNumberViewModel
	{
		[Required]
		[Phone]
		[Display(Name = "رقم الهاتف")]
		public string Number { get; set; }
	}

	public class VerifyPhoneNumberViewModel
	{
		[Required]
		[Display(Name = "رمز التأكيد")]
		public string Code { get; set; }

		[Required]
		[Phone]
		[Display(Name = "رقم التأكيد")]
		public string PhoneNumber { get; set; }
	}

	public class ConfigureTwoFactorViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
	}
}