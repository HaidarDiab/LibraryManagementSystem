using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace LibraryManagementSystem.Models
{
	public class ExternalLoginConfirmationViewModel
	{
		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[Display(Name = "بريدك الالكتروني")]
		public string Email { get; set; }
	}

	public class ExternalLoginListViewModel
	{
		public string ReturnUrl { get; set; }
	}

	public class SendCodeViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
		public string ReturnUrl { get; set; }
		public bool RememberMe { get; set; }
	}

	public class VerifyCodeViewModel
	{
		[Required(ErrorMessage = "اسم المزود او المخدم مطلوب")]
		public string Provider { get; set; }

		[Required(ErrorMessage = "رمز التحقق مطلوب")]
		[Display(Name = "رمز التحقق")]
		public string Code { get; set; }
		public string ReturnUrl { get; set; }

		[Display(Name = "تذكر هذا المتصفح؟")]
		public bool RememberBrowser { get; set; }

		public bool RememberMe { get; set; }
	}

	public class ForgotViewModel
	{
		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[Display(Name = "بريدك الالكتروني")]
		public string Email { get; set; }
	}

	public class LoginViewModel
	{
		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[Display(Name = "بريدك الالكتروني")]
		[EmailAddress(ErrorMessage = "البريد الالكتروني غير صالح")]
		public string Email { get; set; }

		[Required(ErrorMessage = "كلمة المرور مطلوبة")]
		[DataType(DataType.Password)]
		[Display(Name = "كلمة المرور")]
		public string Password { get; set; }

		[Display(Name = "تذكرني؟")]
		public bool RememberMe { get; set; }
	}

	public class RegisterViewModel
	{
		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[EmailAddress(ErrorMessage = "البريد الالكتروني غير صالح")]
		[Display(Name = "بريدك الالكتروني")]
		public string Email { get; set; }



		[Required(ErrorMessage = "كلمة المرور مطلوبة")]
		[StringLength(100, ErrorMessage = "كلمة المرور يجب ان تتكون من 6 محارف على الأقل ويجب أن تحتوي على الأقل رقم واحد وحرف صغير  وحرف كبير", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "كلمة المرور")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "تأكيد كلمة المرور")]
		[Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتين")]
		public string ConfirmPassword { get; set; }

	}

	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[EmailAddress]
		[Display(Name = "بريدك الالكتروني")]
		public string Email { get; set; }

		[Required(ErrorMessage = "كلمة المرور مطلوبة")]
		[StringLength(100, ErrorMessage = "كلمة المرور يجب ان تتكون من 6 محارف على الأقل ويجب أن تحتوي على الأقل رقم واحد وحرف صغير  وحرف كبير", MinimumLength = 6)]

		[DataType(DataType.Password)]
		[Display(Name = "كلمة المرور")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "تأكيد كلمة المرور")]
		[Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتين")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}

	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[EmailAddress(ErrorMessage = "البريد الالكتروني غير صالح")]
		[Display(Name = "بريدك الالكتروني")]
		public string Email { get; set; }
	}
}
